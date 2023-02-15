namespace Application.Messages.DelayedMessages.Queries.GetAllDelayedMessagesByUserId;

using Common.Models;
using MediatR;
using Common.Interfaces;

public record GetAllDelayedMessagesByUserIdQuery(long Id) : IRequest<IReadOnlyList<DelayedMessageDto>>;


public class GetAllDelayedMessagesByUserIdQueryHandler : IRequestHandler<GetAllDelayedMessagesByUserIdQuery,IReadOnlyList<DelayedMessageDto>>
{
    private readonly IDelayedMessageService _messageService;

    public GetAllDelayedMessagesByUserIdQueryHandler(IDelayedMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task<IReadOnlyList<DelayedMessageDto>> Handle(GetAllDelayedMessagesByUserIdQuery request, CancellationToken cancellationToken)
    {
        return  await _messageService.GetAllByUserIdAsync(request.Id);
    }
}