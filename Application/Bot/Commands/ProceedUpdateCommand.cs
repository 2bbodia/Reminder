namespace Application.Bot.Commands;
using MediatR;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public record ProceedUpdateCommand(Update Update) : IRequest;

public class ProceedUpdateCommandHandler : IRequestHandler<ProceedUpdateCommand>
{
    private readonly ISender _mediatr;

    public ProceedUpdateCommandHandler(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    public async Task Handle(ProceedUpdateCommand request, CancellationToken cancellationToken)
    {
        switch (request.Update.Type)
        {
            case UpdateType.Message:
                Message? msg = request?.Update?.Message;
                if (msg != null)
                    await _mediatr.Send(new ProceedMessageCommand(msg), cancellationToken);
                break;
        }

    }
}