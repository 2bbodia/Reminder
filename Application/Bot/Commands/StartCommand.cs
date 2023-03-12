namespace Application.Bot.Commands;

using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;
public record StartCommand(Message Message) : IRequest;

public class StartCommandHandler : IRequestHandler<StartCommand>
{
    private readonly ITelegramBotClient _bot;
    private readonly IApplicationDbContext _db;

    public StartCommandHandler(ITelegramBotClient bot, IApplicationDbContext db)
    {
        _bot = bot;
        _db = db;
    }

    public async Task Handle(StartCommand request, CancellationToken cancellationToken)
    {
        Message msg = request.Message;
        var telegramUser = msg.From!;
        var chatId = msg.Chat.Id;
        var telegramUserId = telegramUser.Id;

        var user = await _db.Users.FindAsync(telegramUserId);
        if (user is null)
        {
            user = new Domain.Entities.User
            {
                Id = telegramUserId
            };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync(cancellationToken);
            
        }
           


        await _bot.SendTextMessageAsync(chatId,
            $"Привіт, {telegramUser.FirstName}!" +
            $"{Environment.NewLine}" +
            $"Переглянь меню!");

    }
}