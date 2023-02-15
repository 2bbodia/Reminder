namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Application.Bot.Commands.ProceedUpdate;
using MediatR;



public class BotWebhookController : ControllerBase
{
    private readonly ISender _mediatr;

    public BotWebhookController(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Update update)
    {
        _mediatr.Send(new ProceedUpdateCommand(update));
        return Ok();
    }
}