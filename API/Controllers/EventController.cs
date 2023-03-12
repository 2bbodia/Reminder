using Application.Events.Commands.CreateEvent;
using Application.Events.Queries.GetEventsByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IMediator _mediatr;

    public EventController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody]CreateEventCommand command)
    {
        var id = await _mediatr.Send(command);
        return Ok(id);
    }

    public async Task<IActionResult> GetEventsByUserId([FromQuery] GetEventsByUserIdQuery query)
    {
        var events = await _mediatr.Send(query);
        return Ok(events);
    }


}

