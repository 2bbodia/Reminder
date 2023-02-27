namespace Application.Messages.RecurringMessages.Queries.GetRecurringMessagesByUserId;

using Common.Interfaces;
using MediatR;

public record GetRecurringMessagesByUserIdQuery(
    long Id, 
    string? OrderBy,
    int Page = 1,
    int PageSize = 10) : IRequest;


public class GetRecurringMessagesByUserIdQueryHandler : IRequestHandler<GetRecurringMessagesByUserIdQuery>
{
    private readonly IRecurringMessageService _messageService;

    public GetRecurringMessagesByUserIdQueryHandler(IRecurringMessageService messageService)
    {
        _messageService = messageService;
    }
    
    public async Task<Unit> Handle(GetRecurringMessagesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _messageService.GetAllByUserIdAsync(request.Id);
        return Unit.Value ;
    }
}