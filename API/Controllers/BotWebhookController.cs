namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Application.Bot.Commands;
using MediatR;
using System.Diagnostics;

public class BotWebhookController : ControllerBase
{
    private readonly ISender _mediatr;

    public BotWebhookController(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update)
    {

        await _mediatr.Send(new ProceedUpdateCommand(update));

        return Ok();
    }
}