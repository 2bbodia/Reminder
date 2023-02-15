namespace Application.Bot.Commands.ProceedUpdate;
using MediatR;
using Telegram.Bot.Types;

public record ProceedUpdateCommand(Update Update) : IRequest;