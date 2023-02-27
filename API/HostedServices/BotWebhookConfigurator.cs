namespace API.HostedServices;

using Microsoft.Extensions.Options;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using API.ConfigurationModels;


public class BotWebhookConfigurator : IHostedService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<BotWebhookConfigurator> logger;
    private readonly BotConfiguration _botConfigs;

    public BotWebhookConfigurator(IServiceProvider serviceProvider, ILogger<BotWebhookConfigurator> logger, IOptions<BotConfiguration> botConfigs)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
        this._botConfigs = botConfigs.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = this.serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        var webhookUrl = $"{_botConfigs.WebHookUrl}/BotWebhook/{_botConfigs.Token}";
        this.logger.LogInformation($"Configuring webhook : {webhookUrl}");
        await botClient.SetWebhookAsync(
            url: webhookUrl,
            allowedUpdates: Array.Empty<UpdateType>(),
            cancellationToken: cancellationToken
            );
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = this.serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
        this.logger.LogInformation("Removing webhook");
        await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}
