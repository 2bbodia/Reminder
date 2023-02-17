namespace API.Controllers;

using Application.Common.Models;
using Application.Messages.DelayedMessages.Commands.CancelDelayedMessage;
using Application.Messages.DelayedMessages.Commands.CreateDelayedMessage;
using Application.Messages.DelayedMessages.Queries.GetDelayedMessagesByUserId;
using Application.Messages.DelayedMessages.Queries.GetDelayedMessageById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DelayedMessageController : ControllerBase
{
    private readonly ISender _mediatr;

    public DelayedMessageController(ISender mediatr)
    {
        _mediatr = mediatr;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMessagesByUserId([FromQuery] GetDelayedMessagesByUserIdQuery query)
    {
        return Ok(await _mediatr.Send(query));
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var message = await _mediatr.Send(new GetDelayedMessageByIdQuery(id));
        return message == null ? BadRequest() : Ok(message);
    }

    [HttpPost]
    public async Task<CreatedMessageDto> CreateDelayedMessage([FromBody] CreateDelayedMessageCommand command)
    {
       return await _mediatr.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelDelayedMessage(string id)
    {
        var result = await _mediatr.Send(new CancelDelayedMessageCommand(id));
        return result ? Ok() : BadRequest();
    }
    
}