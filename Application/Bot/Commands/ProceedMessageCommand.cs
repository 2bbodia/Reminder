namespace Application.Bot.Commands;

using MediatR;
using Telegram.Bot.Types;

public record ProceedMessageCommand(Message Message) : IRequest;

public class ProceedMessageCommandHandler : IRequestHandler<ProceedMessageCommand>
{
    private readonly ISender _mediatr;

    public ProceedMessageCommandHandler(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    public async Task<Unit> Handle(ProceedMessageCommand request, CancellationToken cancellationToken)
    {
        var command = GetBotCommand(request.Message);
         await  _mediatr.Send(command);
        return Unit.Value ;
    }

    private static IRequest GetBotCommand(Message message) => message.Text switch
    {
        "/start" => new StartCommand(message),

        _ => new NotFoundCommand(message.From!.Id)
    };
}