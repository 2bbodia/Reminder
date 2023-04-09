using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Events.Queries.GetEventsByUserId;
using Application.Events.Queries.GetEventIfExistInParticularTime;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediatr;

    public EventsController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody]CreateEventCommand command)
    {
        var id = await _mediatr.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetEventsByUserId([FromQuery] GetEventsByUserIdQuery query)
    {
        var events = await _mediatr.Send(query);
        return Ok(events);
    }

    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        await _mediatr.Send(new DeleteEventCommand(id)) ;

        return Ok();
    }

    [HttpGet("isExist")]
    public async Task<IActionResult> IsExist([FromQuery] GetEventIfExistInParticularTimeQuery query)
    {
        return Ok(await _mediatr.Send(query));
    }


}

