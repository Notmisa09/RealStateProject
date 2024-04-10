using Swashbuckle.AspNetCore.SwaggerUI;
using RealStateApp.Presentation.API.Middlewares;

namespace RealStateApp.Presentation.API.Extensions
{
    public static class AppExtensions
    {
        public static void UserSwaggerExtensions(this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant"); //Modificar nombre segun Misa
                options.DefaultModelRendering(ModelRendering.Model);
            });
        }
        
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddlewares>();
        }
    }
}
