using Swashbuckle.AspNetCore.SwaggerUI;

namespace RealStateApp.Presentation.API.Extensions
{
    public static class AppExtensions
    {
        public static void UserSwaggerExtensions(this IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant");
                options.DefaultModelRendering(ModelRendering.Model);
            });
        }
    }
}
