namespace Application.Messages.RecurringMessages.Commands.CreateDailyRecurringMessage;

using Application.Common.Interfaces;
using MediatR;

public record CreateDailyRecurringMessageCommand(string Text, long ReceiverId, int? Hour, int? Minute) :IRequest<Guid>;

public class CreateDailyRecurringMessageCommandHandler : IRequestHandler<CreateDailyRecurringMessageCommand, Guid>
{
    private readonly IRecurringMessageService _recurringMessageService;

    public CreateDailyRecurringMessageCommandHandler(IRecurringMessageService recurringMessageService)
    {
        _recurringMessageService = recurringMessageService;
    }

    public async Task<Guid> Handle(CreateDailyRecurringMessageCommand request, CancellationToken cancellationToken)
    {
        return await _recurringMessageService.CreateDailyRecurringMessageAsync(request.Text, request.ReceiverId, request.Hour, request.Minute);
    }
}
