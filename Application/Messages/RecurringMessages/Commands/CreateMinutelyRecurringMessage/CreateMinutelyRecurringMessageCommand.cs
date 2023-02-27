namespace Application.Messages.RecurringMessages.Commands.CreateMinutelyRecurringMessage;

using Application.Common.Interfaces;
using MediatR;

public record CreateMinutelyRecurringMessageCommand(string Text, long ReceiverId) : IRequest<Guid>;

public class CreateMinutelyRecurringMessageCommandHandler : IRequestHandler<CreateMinutelyRecurringMessageCommand, Guid>
{
    private readonly IRecurringMessageService _recurringMessageService;

    public CreateMinutelyRecurringMessageCommandHandler(IRecurringMessageService recurringMessageService)
    {
        _recurringMessageService = recurringMessageService;
    }

    public async Task<Guid> Handle(CreateMinutelyRecurringMessageCommand request, CancellationToken cancellationToken)
    {
        return await _recurringMessageService.CreateMinutelyRecurringMessageAsync(request.Text, request.ReceiverId);
    }
}

