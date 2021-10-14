using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using eComSolution.Service.System.Token;
using eComSolution.Service.System.Users;
using eComSolution.Service.Catalog.Products;

namespace eComSolution.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eComSolution.API", Version = "v1" });
            });
            services.AddDbContext<EComDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //         .AddJwtBearer(options => {
            //             options.TokenValidationParameters = new TokenValidationParameters
            //             {
            //                 ValidateIssuer = false,
            //                 ValidateAudience = false,
            //                 ValidateLifetime = false, // nếu true thì có valid cái thời gian hết hạn
            //                 ValidateIssuerSigningKey = true, // có valid cái key của JWT
            //                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"))
            //             };
            //         });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eComSolution.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
