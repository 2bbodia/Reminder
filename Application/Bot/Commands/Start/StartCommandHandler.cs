namespace Application.Bot.Commands.Start;
using MediatR;

public class StartCommandHandler : IRequestHandler<StartCommand>
{
    public Task<Unit> Handle(StartCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}