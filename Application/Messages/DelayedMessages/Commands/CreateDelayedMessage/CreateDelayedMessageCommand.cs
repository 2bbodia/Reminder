namespace Application.Messages.DelayedMessages.Commands.CreateDelayedMessage;

using Common.Interfaces;
using MediatR;
using Common.Models;

public record CreateDelayedMessageCommand(string Text, long ReceiverId, DateTime TimeToSend) : IRequest<CreatedMessageDto>;

public class CreateDelayedMessageCommandHandler : IRequestHandler<CreateDelayedMessageCommand,CreatedMessageDto>
{
    private readonly IDelayedMessageService _messageService;

    public CreateDelayedMessageCommandHandler(IDelayedMessageService messageService)
    {
        _messageService = messageService;
    }

    public async  Task<CreatedMessageDto> Handle(CreateDelayedMessageCommand request, CancellationToken cancellationToken)
    { 
       var jobId =  await _messageService.CreateDelayedMessageAsync(request.Text, request.ReceiverId, request.TimeToSend);

       return new CreatedMessageDto(jobId);
    }
}