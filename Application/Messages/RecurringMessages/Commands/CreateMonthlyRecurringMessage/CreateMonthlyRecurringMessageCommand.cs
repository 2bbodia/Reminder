namespace Application.Messages.RecurringMessages.Commands.CreateMonthlyRecurringMessage;

using Application.Common.Interfaces;
using MediatR;

public record CreateMonthlyRecurringMessageCommand(string Text, long ReceiverId, int? Day, int? Hour, int? Minute) : IRequest<Guid>;

public class CreateMonthlyRecurringMessageCommandHandler : IRequestHandler<CreateMonthlyRecurringMessageCommand,Guid>
{
    private readonly IRecurringMessageService _recurringMessageService;

    public CreateMonthlyRecurringMessageCommandHandler(IRecurringMessageService recurringMessageService)
    {
        _recurringMessageService = recurringMessageService;
    }

    public async Task<Guid> Handle(CreateMonthlyRecurringMessageCommand request, CancellationToken cancellationToken)
    {
        return await _recurringMessageService.CreateMonthlyRecurringMessageAsync(request.Text, request.ReceiverId, request.Day, request.Hour, request.Minute);
    }
}
