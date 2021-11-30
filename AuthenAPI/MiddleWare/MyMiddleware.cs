using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using AuthenAPI.Services.Authen;
using Microsoft.AspNetCore.Http.Extensions;

namespace AuthenAPI.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
 
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
 
        public async Task Invoke(HttpContext httpContext, IAuthenService authenService)
        {
            bool check = false;
            // check httpContext.GetEndpoint(): 
            // - nó sẽ null nếu như request không khớp với endpoint nào
            // - VD: url lấy ảnh hoặc url 404 not found
            // check httpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>(): 
            // - nó sẽ null nếu như method của request bị sai - lỗi 405
            // - VD: POST api/Products/7 (GET getProductById)
            // những trường hợp như trên thì sẽ không cần check quyền, nên cho qua
            
            if(httpContext.GetEndpoint() != null 
            && httpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>() != null )
            {
                var controllerActionDescriptor = httpContext
                    .GetEndpoint()
                    .Metadata
                    .GetMetadata<ControllerActionDescriptor>();

                var controllerName = controllerActionDescriptor.ControllerName;
                var actionName = controllerName + "." + controllerActionDescriptor.ActionName;
                
                // get id user
                var claimsPrincipal = httpContext.User;

                if(claimsPrincipal.FindFirst("id") == null){
                    await _next(httpContext);
                    return;
                }

                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
                // get permission of user 
                var list_function = await authenService.GetPermissions(userId);

                // check permission of user
                foreach (var function in list_function){
                    if(function.ActionName == actionName){
                        check = true; // nếu có quyền thực hiện thì gán check = true và dừng vòng lặp
                        break;
                    }
                }

                if(!check)
                {
                    httpContext.Response.StatusCode = 403;
                    return;
                }
            }
            await _next(httpContext);
        }
    }
}
