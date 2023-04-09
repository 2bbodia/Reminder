namespace Application.Events.Queries.GetEventIfExistInParticularTime;

using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public record GetEventIfExistInParticularTimeQuery(
    long UserId, 
    DateTime StartDate, 
    DateTime EndDate) : IRequest<EventDto>;


public class GetEventIfExistInParticularTimeQueryHandler : IRequestHandler<GetEventIfExistInParticularTimeQuery,EventDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEventIfExistInParticularTimeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EventDto> Handle(GetEventIfExistInParticularTimeQuery request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Users
            .Include(u => u.Events)
            .FirstOrDefaultAsync(u => u.Id == request.UserId) ?? throw new Exception();

        var existingEventsInParticularTime = user.Events.FirstOrDefault(e =>
        {
            var isStartDateIncluded = e.StartDate <= request.StartDate && e.EndDate >= request.EndDate;
            var isEndDateIncluded = e.StartDate <= request.EndDate && e.EndDate >= request.EndDate;
            return isStartDateIncluded || isEndDateIncluded;
        });

        return _mapper.Map<EventDto>(existingEventsInParticularTime);



    }
}
