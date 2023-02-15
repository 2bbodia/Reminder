namespace Application.Bot.Commands.ProceedMessage;
using MediatR;
using Telegram.Bot.Types;

public record ProceedMessageCommand(Message Message) : IRequest;