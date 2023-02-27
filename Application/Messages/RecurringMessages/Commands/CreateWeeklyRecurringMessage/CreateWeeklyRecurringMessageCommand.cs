namespace Application.Messages.RecurringMessages.Commands.CreateWeeklyRecurringMessage;

using Application.Common.Interfaces;
using MediatR;

public  record CreateWeeklyRecurringMessageCommand(string Text, long ReceiverId, DayOfWeek? DayOfWeek, int? Hour, int? Minute) : IRequest<Guid>;

public class CreateWeeklyRecurringMessageCommandHandler : IRequestHandler<CreateWeeklyRecurringMessageCommand, Guid>
{
    private readonly IRecurringMessageService _recurringMessageService;

    public CreateWeeklyRecurringMessageCommandHandler(IRecurringMessageService recurringMessageService)
    {
        _recurringMessageService = recurringMessageService;
    }

    public async Task<Guid> Handle(CreateWeeklyRecurringMessageCommand request, CancellationToken cancellationToken)
    {
        return await _recurringMessageService.CreateWeeklyRecurringMessageAsync(request.Text, request.ReceiverId, request.DayOfWeek, request.Hour, request.Minute);
    }
}
