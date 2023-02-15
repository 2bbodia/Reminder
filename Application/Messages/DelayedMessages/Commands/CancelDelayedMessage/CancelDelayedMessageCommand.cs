namespace Application.Messages.DelayedMessages.Commands.CancelDelayedMessage;

using MediatR;
using Common.Interfaces;

public record CancelDelayedMessageCommand(string Id) : IRequest<bool>;

public class CancelDelayedMessageCommandHandler : IRequestHandler<CancelDelayedMessageCommand, bool>
{
    private readonly IDelayedMessageService _messageService;

    public CancelDelayedMessageCommandHandler(IDelayedMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task<bool> Handle(CancelDelayedMessageCommand request, CancellationToken cancellationToken)
    {
        return await _messageService.CancelDelayedMessageAsync(request.Id);
    }
}