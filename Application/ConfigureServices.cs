

using Microsoft.Extensions.Configuration;

namespace Application;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;


public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(options =>
        {
            options.AddMaps(Assembly.GetExecutingAssembly());
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services
            .AddHttpClient("telegramBot")
            .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(configuration["BotConfiguration:Token"]!, httpClient));
        return services;
    }
}
