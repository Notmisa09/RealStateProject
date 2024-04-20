using RealStateApp.Infrastructure.Shared;
using RealStateApp.Infrastructure.Identity;
using RealStateApp.Presentation.API.Extensions;
using Microsoft.AspNetCore.Identity;
using RealStateApp.Infrastructure.Identity.Entities;
using RealStateApp.Infrastructure.Identity.Seeds;
using RealStateApp.Infrastructure.Persistence;
using RealStateApp.Core.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddSharedLayer(builder.Configuration);
builder.Services.AddIdentityLayer(builder.Configuration);
builder.Services.AddIdentityApiLayer(builder.Configuration);
builder.Services.InfraStructureLayer(builder.Configuration);
builder.Services.AddAPiVersioningExtension();
builder.Services.AddSwaggerExtension();
builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await AdminSeed.SeedAsync(userManager, roleManager);
        await DeveloperSeed.SeedAsync(userManager, roleManager);

    }
    catch (Exception ex)
    {
        
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UserSwaggerExtensions();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/health");
app.UseSession();
app.MapControllers();

app.Run();
