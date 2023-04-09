using Application.Common.Models;
using Application.Events.Commands.CreateEvent;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<CreateEventCommand, Event>()
            .ForMember(m => m.Id, cf => cf.Ignore())
            .ForMember(m => m.User, cf => cf.Ignore())
            .ForMember(m => m.Importance, cf => cf.Ignore());

        CreateMap<Event, EventDto>()
            .ForMember(m => m.Importance, cf => cf.MapFrom(m => m.Importance.Name));

    }
}
