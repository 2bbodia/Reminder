using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Application.Events.Queries.GetEventsByUserId;

public record GetEventsByUserIdQuery(long UserId) : IRequest<IReadOnlyList<EventDto>>;

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
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user is null) return Array.Empty<EventDto>();


        return _mapper.Map<IReadOnlyList<EventDto>>(user.Events);
    }
}
