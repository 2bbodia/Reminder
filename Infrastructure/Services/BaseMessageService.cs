namespace Infrastructure.Services;

using Telegram.Bot;

public abstract class BaseMessageService
{
    protected readonly ITelegramBotClient BotClient;

    protected BaseMessageService(ITelegramBotClient botClient)
    {
        BotClient = botClient;
    }

    public async Task SendTextMessage(long to, string message, CancellationToken cancellationToken)
    {
        await BotClient.SendTextMessageAsync(to, message, cancellationToken: cancellationToken);
    }
}