using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace RealStateApp.Presentation.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                List<string> xmFiles = Directory.GetFiles
                (AppContext.BaseDirectory, "*.xml", searchOption: SearchOption.TopDirectoryOnly).ToList();
                xmFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RealStateApp",
                    Description = "This Api is fit for a property selling company",
                    Contact = new OpenApiContact
                    {
                        Name = "Felix Misae Mora",
                        Email = "misamora03@gmail.com",
                        Url = new Uri("https://www.instagram.com/misamorasuero/")
                    }
                });
                options.EnableAnnotations();
                options.DescribeAllParametersInCamelCase();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = ""
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    }
                });
            });
            
        }

        #region APIVERSIONING

        public static void AddAPiVersioningExtension(this IServiceCollection service)
        {
            service.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        #endregion
    }
}
