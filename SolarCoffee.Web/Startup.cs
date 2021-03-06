using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolarCoffee.Data;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Order;
using SolarCoffee.Services.Product;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace SolarCoffee.Web
{
    public class Startup
    {
        private IConfiguration _configuration { get; set; }
        public Startup(IWebHostEnvironment environment)
        {
            _configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", false, true)
                 .AddJsonFile("appsettings.json", false, true).Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            services.AddDbContext<SolarDbContext>(options =>
            {
                options.EnableDetailedErrors();
                options.UseSqlServer(_configuration.GetConnectionString("solar.dev"));
            });

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<ICustomerService, CustomerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(buider =>
            buider
                .WithOrigins("http://localhost:8080")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
