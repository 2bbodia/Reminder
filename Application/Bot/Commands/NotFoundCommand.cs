namespace Application.Bot.Commands;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

public record NotFoundCommand(long ChatId) : IRequest;

public class NotFoundCommandHandler : IRequestHandler<NotFoundCommand>
{
    private readonly ITelegramBotClient bot;

    public NotFoundCommandHandler(ITelegramBotClient bot)
    {
        this.bot = bot;
    }

    
    public async Task Handle(NotFoundCommand request, CancellationToken cancellationToken)
    {
        await bot.SendTextMessageAsync(
            chatId: request.ChatId,
            text: "Command not found",
            cancellationToken: cancellationToken
        );
    }

}

