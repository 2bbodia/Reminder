namespace Application.Messages.RecurringMessages.Commands.CancelRecurringMessage;

using Application.Common.Interfaces;
using MediatR;

public record CancelRecurringMessageCommand(string Id) : IRequest;

public class CancelRecurringMessageCommandHandler : IRequestHandler<CancelRecurringMessageCommand>
{
    private readonly IRecurringMessageService _recurringMessageService;

    public CancelRecurringMessageCommandHandler(IRecurringMessageService recurringMessageService)
    {
        _recurringMessageService = recurringMessageService;
    }

    public async Task Handle(CancelRecurringMessageCommand request, CancellationToken cancellationToken)
    {
        await _recurringMessageService.CancelRecurringMessageAsync(request.Id);
    }
}

