using Application.Messages.RecurringMessages.Queries.GetAllByUserId;

namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Messages.DelayedMessages.Commands;
using Application.Messages.RecurringMessages.Commands;
using MediatR;

[Route("api/[controller]")]
[ApiController]
public class RecurringMessageController : ControllerBase
{
    private readonly ISender _mediatr;

    public RecurringMessageController(ISender mediatr)
    {
        this._mediatr = mediatr;
    }

    [HttpGet]
    [Route("[action]/{id:long}")]
    public async Task<IActionResult> GetAllByUserId(long id)
    {
        await _mediatr.Send(new GetAllByUserIdQuery(id));
        return Ok();
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public IActionResult GetById(string id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task Create([FromBody] CreateRecurringMessageCommand command)
    {
        await _mediatr.Send(command);
    }
}