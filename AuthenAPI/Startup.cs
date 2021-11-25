using AuthenAPI.Services.Authen;
using AuthenAPI.Services.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Database;
using AuthenAPI.Middleware;

namespace AuthenAPI
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

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(option 
                => option.AddPolicy("MyCorsPolicy", builder => 
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthenAPI", Version = "v1" }); 
            });
            
            services.AddDbContext<EComDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));
            // add jwt bearer authentication
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //         .AddJwtBearer(options => {
            //             options.TokenValidationParameters = new TokenValidationParameters
            //             {
            //                 ValidateIssuer = false,
            //                 ValidateAudience = false,
            //                 ValidateLifetime = true, // nếu true thì có valid cái thời gian hết hạn
            //                 ValidateIssuerSigningKey = true, // có valid cái key của JWT
            //                 ClockSkew = TimeSpan.Zero,
            //                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"))
            //             };
            //         });
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenService, AuthenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthenAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyCorsPolicy");

            app.UseAuthorization();

            app.UseMiddleware<MyMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
