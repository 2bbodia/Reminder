using Application.Common.Helpers;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Messages.DelayedMessages.Queries.GetDelayedMessagesByUserId;

using Common.Models;
using MediatR;
using Common.Interfaces;
using static Common.Constants.MessagesOrdering;

public record GetDelayedMessagesByUserIdQuery(
    long UserId,
    string? OrderBy,
    int Page = 1,
    int PageSize = 10) : IRequest<Pagination<DelayedMessageDto>>;


public class GetDelayedMessagesByUserIdQueryHandler : IRequestHandler<GetDelayedMessagesByUserIdQuery, Pagination<DelayedMessageDto>>
{
    private readonly IDelayedMessageService _messageService;

    public GetDelayedMessagesByUserIdQueryHandler(IDelayedMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task<Pagination<DelayedMessageDto>> Handle(GetDelayedMessagesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var messages = await _messageService.GetAllByUserIdAsync(request.UserId);

        List<DelayedMessageDto> orderedMessages;
        switch (request.OrderBy)
        {
            case CreatedAtTimeDescending:
                orderedMessages = messages.OrderByDescending(m => m.CreatedAt).ToList();
                break;
            case EnqueueAtTimeAscending:
                orderedMessages = messages.OrderBy(m => m.EnqueueAt).ToList();
                break;
            case EnqueueAtTimeDescending:
                orderedMessages = messages.OrderByDescending(m => m.EnqueueAt).ToList();
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

        return new Pagination<DelayedMessageDto>(request.Page, request.PageSize, totalCount, paginatedMessages);

    }
}