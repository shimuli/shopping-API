using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Shopping
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;
        public void Configure(SwaggerGenOptions options)
        {
            foreach(var desc in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = $"Shopping API {desc.ApiVersion}",
                        Version = desc.ApiVersion.ToString(),
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "shimulicedric@gmail.com",
                            Name = "Shimuli Cedric",
                            Url = new Uri("https://github.com/shimuli/shopping-API")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT Licence",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                "JWT Authorization header using Bearer scheme. \r\n\r\n " +
                "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
                "Example: \"Bearer 123874657865gjhfgh\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id= "Bearer"
                        },
                        Scheme = "oauth2",
                        Name= "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var cmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            options.IncludeXmlComments(cmlCommentFullPath);
        }
    }
}
