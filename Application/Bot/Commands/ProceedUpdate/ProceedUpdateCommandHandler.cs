namespace Application.Bot.Commands.ProceedUpdate;
using MediatR;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using ProceedMessage;


public class ProceedUpdateCommandHandler:IRequestHandler<ProceedUpdateCommand>
{
    private readonly ISender _mediatr;

    public ProceedUpdateCommandHandler(ISender mediatr)
    {
        this._mediatr = mediatr;
    }

    public Task<Unit> Handle(ProceedUpdateCommand request, CancellationToken cancellationToken)
    {
        switch (request.Update.Type)
        {
          case  UpdateType.Message:
              Message? msg = request?.Update?.Message;
              if (msg != null)
                  this._mediatr.Send(new ProceedMessageCommand(msg), cancellationToken);
              break;
        }

        return Unit.Task;
    }
}