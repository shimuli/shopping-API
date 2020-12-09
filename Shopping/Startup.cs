using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shopping.Repo;
using Shopping.Repo.IRepo;
using AutoMapper;
using Shopping.Mapper;
using Shopping.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;

namespace Shopping
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
            services.AddDbContext<MyShoppingDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MyShoppingDB")));

            services.AddScoped<IInventoryRepo, InventoryRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IShoppingRepo, ShopingRepo>();
            services.AddAutoMapper(typeof(ShoppingMappings));
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("ShoppingAPI",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Shopping API",
                        Version = "1",
                        Description = "Shopping App Web API",
                        Contact= new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "shimulicedric@gmail.com",
                            Name= "Shimuli Cedric",
                            Url = new Uri("https://github.com/shimuli/shopping-API")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT Licence",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                      
                    });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentFullPath);
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options=> {
                options.SwaggerEndpoint("/swagger/ShoppingAPI/swagger.json", "Shopping API");
                options.RoutePrefix = "";
                });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
