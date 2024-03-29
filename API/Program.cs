using API.ConfigurationModels;
using API.Filters;
using API.HostedServices;
using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.Configure<BotConfiguration>(builder.Configuration.GetSection("BotConfiguration"));
builder.Services.AddHostedService<BotWebhookConfigurator>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews(options =>
        options.Filters.Add<ApiExceptionFilterAttribute>());
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(); 
app.UseDefaultFiles();
app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

var token = builder.Configuration.GetSection("BotConfiguration").Get<BotConfiguration>()!.Token;
app.MapControllerRoute(
            name: "telegramBot",
            pattern: $"BotWebhook/{token}",
            new { controller = "BotWebhook", action = "Post" });


app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var scopeServices = scope.ServiceProvider;
    try
    {
        var dbContext = scopeServices.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scopeServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the DB.");
    }
}

app.Run();