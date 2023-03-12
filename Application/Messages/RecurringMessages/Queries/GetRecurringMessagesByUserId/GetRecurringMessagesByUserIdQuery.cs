namespace Application.Messages.RecurringMessages.Queries.GetRecurringMessagesByUserId;

using Application.Common.Helpers;
using Application.Common.Models;
using Common.Interfaces;
using MediatR;
using static Common.Constants.MessagesOrdering;

public record GetRecurringMessagesByUserIdQuery(
    long UserId, 
    string? OrderBy,
    int Page = 1,
    int PageSize = 10) : IRequest<Pagination<RecurringMessageDto>>;


public class GetRecurringMessagesByUserIdQueryHandler : IRequestHandler<GetRecurringMessagesByUserIdQuery, Pagination<RecurringMessageDto>>
{
    private readonly IRecurringMessageService _messageService;

    public GetRecurringMessagesByUserIdQueryHandler(IRecurringMessageService messageService)
    {
        _messageService = messageService;
    }
    
    public async Task<Pagination<RecurringMessageDto>> Handle(GetRecurringMessagesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var messages = await _messageService.GetAllByUserIdAsync(request.UserId);
        List<RecurringMessageDto> orderedMessages;
        switch (request.OrderBy)
        {
            case CreatedAtTimeDescending:
                orderedMessages = messages.OrderByDescending(m => m.CreatedAt).ToList();
                break;
            case CreatedAtTimeAscending:
            default:
                orderedMessages = messages.OrderBy(m => m.CreatedAt).ToList();
                break;
        }

        var paginatedMessages = orderedMessages
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var totalCount = messages.Count;

        return new Pagination<RecurringMessageDto>(request.Page, request.PageSize, totalCount, paginatedMessages);
    }
}