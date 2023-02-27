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

    public StartCommandHandler(ITelegramBotClient bot)
    {
        _bot = bot;
    }

    public async Task<Unit> Handle(StartCommand request, CancellationToken cancellationToken)
    {
        Message msg = request.Message;
        var telegramUser = msg.From!;
        var chatId = msg.Chat.Id;
        var telegramUserId = telegramUser.Id;

        await _bot.SendTextMessageAsync(chatId,
            $"Привіт, {telegramUser.FirstName}!" +
            $"{Environment.NewLine}" +
            $"Переглянь меню!");

        return Unit.Value;

    }
}