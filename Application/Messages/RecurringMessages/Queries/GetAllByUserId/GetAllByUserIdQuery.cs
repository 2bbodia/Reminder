namespace Application.Messages.RecurringMessages.Queries.GetAllByUserId;

using MediatR;

public record GetAllByUserIdQuery(long Id) : IRequest;