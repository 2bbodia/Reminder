namespace Infrastructure.Services;

using Application.Common.Interfaces;
using Hangfire;
using Hangfire.Storage;
using Telegram.Bot;

public class RecurringMessageService : BaseMessageService, IRecurringMessageService
{

    public RecurringMessageService(ITelegramBotClient botClient) : base(botClient)
    {
    }
    
    public Task CreateRecurringMessage()
    {
        throw new NotImplementedException();
    }

    public Task GetAllByUserId(long id)
    {
        throw new NotImplementedException();
    }

    public Task GetById(string id)
    {
        throw new NotImplementedException();
    }
}