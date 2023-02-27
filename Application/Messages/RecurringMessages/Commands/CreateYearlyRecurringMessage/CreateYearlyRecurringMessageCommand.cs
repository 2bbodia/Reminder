namespace Application.Messages.RecurringMessages.Commands.CreateYearlyRecurringMessage;

using Application.Common.Interfaces;
using MediatR;
public record CreateYearlyRecurringMessageCommand(string Text, long ReceiverId, int? Month, int? Day, int? Hour, int? Minute) : IRequest<Guid>;


public class CreateYearlyRecurringMessageCommandHandler : IRequestHandler<CreateYearlyRecurringMessageCommand, Guid>
{
    private readonly IRecurringMessageService _recurringMessageService;

    public CreateYearlyRecurringMessageCommandHandler(IRecurringMessageService recurringMessageService)
    {
        _recurringMessageService = recurringMessageService;
    }

    public async Task<Guid> Handle(CreateYearlyRecurringMessageCommand request, CancellationToken cancellationToken)
    {
        return await _recurringMessageService.CreateYearlyRecurringMessageAsync(request.Text, request.ReceiverId, request.Month, request.Day, request.Hour, request.Minute);
    }
}
