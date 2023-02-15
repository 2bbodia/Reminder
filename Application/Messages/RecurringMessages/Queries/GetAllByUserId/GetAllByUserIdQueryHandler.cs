

namespace Application.Messages.RecurringMessages.Queries.GetAllByUserId;

using MediatR;
using Common.Interfaces;

public class GetAllByUserIdQueryHandler : IRequestHandler<GetAllByUserIdQuery>
{
    private readonly IRecurringMessageService _messageService;

    public GetAllByUserIdQueryHandler(IRecurringMessageService messageService)
    {
        _messageService = messageService;
    }

    public Task<Unit> Handle(GetAllByUserIdQuery request, CancellationToken cancellationToken)
    {
        _messageService.GetAllByUserId(request.Id);
        return Unit.Task;
    }
}