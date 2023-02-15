using MediatR;

namespace Application.Bot.Commands.ProceedMessage;

public class ProceedMessageCommandHandler : IRequestHandler<ProceedMessageCommand>
{
    public Task<Unit> Handle(ProceedMessageCommand request, CancellationToken cancellationToken)
    {
        string? text = request.Message.Text;
        return Unit.Task;
    }
}