namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Messages.DelayedMessages.Commands.CancelDelayedMessage;
using Application.Messages.RecurringMessages.Queries.GetRecurringMessagesByUserId;
using Application.Messages.RecurringMessages.Commands.CreateMinutelyRecurringMessage;
using Application.Messages.RecurringMessages.Commands.CreateHourlyRecurringMessage;
using Application.Messages.RecurringMessages.Commands.CreateDailyRecurringMessage;
using Application.Messages.RecurringMessages.Commands.CreateWeeklyRecurringMessage;
using Application.Messages.RecurringMessages.Commands.CreateMonthlyRecurringMessage;
using Application.Messages.RecurringMessages.Commands.CreateYearlyRecurringMessage;
using Application.Messages.RecurringMessages.Commands.CancelRecurringMessage;

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
    public async Task<IActionResult> GetMessagesByUserId([FromQuery] GetRecurringMessagesByUserIdQuery query)
    {
        await _mediatr.Send(query);
        return Ok();
    }

    [HttpPost]
    [Route("Minutely")]
    public async Task<IActionResult> CreateMinutelyRecurringMessage([FromBody] CreateMinutelyRecurringMessageCommand command)
    {
        return Ok(await _mediatr.Send(command));
    }

    [HttpPost]
    [Route("Hourly")]
    public async Task<IActionResult> CreateHourlyRecurringMessage([FromBody] CreateHourlyRecurringMessageCommand command)
    {
        return Ok(await _mediatr.Send(command));
    }

    [HttpPost]
    [Route("Daily")]
    public async Task<IActionResult> CreateDailyRecurringMessage([FromBody] CreateDailyRecurringMessageCommand command)
    {
        return Ok(await _mediatr.Send(command));
    }

    [HttpPost]
    [Route("Weekly")]
    public async Task<IActionResult> CreateWeeklyRecurringMessage([FromBody] CreateWeeklyRecurringMessageCommand command)
    {
        return Ok(await _mediatr.Send(command));
    }

    [HttpPost]
    [Route("Monthly")]
    public async Task<IActionResult> CreateMonthlyRecurringMessage([FromBody] CreateMonthlyRecurringMessageCommand command)
    {
        return Ok(await _mediatr.Send(command));
    }

    [HttpPost]
    [Route("Yearly")]
    public async Task<IActionResult> CreateYearlyRecurringMessage([FromBody] CreateYearlyRecurringMessageCommand command)
    {
        return Ok(await _mediatr.Send(command));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelRecurringMessage(string id)
    {
        return Ok(await _mediatr.Send(new CancelRecurringMessageCommand(id)));
    }

}