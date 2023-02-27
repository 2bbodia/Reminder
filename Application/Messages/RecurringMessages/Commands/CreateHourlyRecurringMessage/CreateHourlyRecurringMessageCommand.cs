namespace Application.Messages.RecurringMessages.Commands.CreateHourlyRecurringMessage;

using Common.Interfaces;
using MediatR;

public record CreateHourlyRecurringMessageCommand(string Text, long ReceiverId, int? Minute) : IRequest<Guid>;

public class CreateHourlyRecurringMessageCommandHandler : IRequestHandler<CreateHourlyRecurringMessageCommand, Guid>
{
    private readonly IRecurringMessageService _recurringMessageService;

    public CreateHourlyRecurringMessageCommandHandler(IRecurringMessageService recurringMessageService)
    {
        _recurringMessageService = recurringMessageService;
    }

    public async Task<Guid> Handle(CreateHourlyRecurringMessageCommand request, CancellationToken cancellationToken)
    {
        return await _recurringMessageService.CreateHourlyRecurringMessageAsync(request.Text, request.ReceiverId, request.Minute);
    }
}
