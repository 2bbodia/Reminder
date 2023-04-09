using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Application.Events.Queries.GetEventsByUserId;

public record GetEventsByUserIdQuery(long UserId, DateOnly Date) : IRequest<IReadOnlyList<EventDto>>;

public class GetEventsByUserIdQueryHandler : IRequestHandler<GetEventsByUserIdQuery, IReadOnlyList<EventDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetEventsByUserIdQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<EventDto>> Handle(GetEventsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _db.Users
            .Include(u => u.Events)
            .ThenInclude(e => e.Importance)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user is null) return Array.Empty<EventDto>();

        var events = user.Events
            .Where(e =>
                    (e.StartDate.Year == request.Date.Year && e.StartDate.Day == request.Date.Day) ||
                    (e.EndDate.Year == request.Date.Year &&  e.EndDate.Day == request.Date.Day))
            .ToList();

        return _mapper.Map<IReadOnlyList<EventDto>>(events);
    }
}
