namespace Application.Messages.DelayedMessages.Queries.GetDelayedMessageById;

using Common.Interfaces;
using Common.Models;
using MediatR;

public record GetDelayedMessageByIdQuery(string Id) : IRequest<DelayedMessageDto?>;



public class GetDelayedMessageByIdQueryHandler : IRequestHandler<GetDelayedMessageByIdQuery, DelayedMessageDto?>
{
    private readonly IDelayedMessageService _messageService;

    public GetDelayedMessageByIdQueryHandler(IDelayedMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task<DelayedMessageDto?> Handle(GetDelayedMessageByIdQuery request, CancellationToken cancellationToken)
    {
       return await  _messageService.GetByIdAsync(request.Id);
    }
}