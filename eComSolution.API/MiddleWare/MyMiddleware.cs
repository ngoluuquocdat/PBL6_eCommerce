using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using eComSolution.Service.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace eComSolution.API.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
 
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext, IUserService userService)
        {
            bool check = false;

            if(httpContext.GetEndpoint() != null)
            {
                // lấy action name
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
                var list_function = await userService.GetPermissions(userId);

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
