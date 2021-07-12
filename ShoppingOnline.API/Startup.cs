using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShoppingOnline.API.Extensions;
using ShoppingOnline.API.Services;
using ShoppingOnline.API.Services.Implementation;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.MapperConfiguration;
using ShoppingOnline.Domain.Services;
using ShoppingOnline.Domain.Services.Implementation;

namespace ShoppingOnline.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add service and create Policy with options
            services.AddCors();
            services.AddDbContext<ShoppingOnlineDBContext>(db => db.UseSqlite(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ShoppingOnline.API")));
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IProductDashboardService, ProductDashboardService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddControllers();
            services.AddIdentityServices(_configuration);
            services.AddControllersWithViews().AddNewtonsoftJson();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingOnline.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //allow cors origin
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingOnline.API v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
