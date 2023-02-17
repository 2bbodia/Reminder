namespace Infrastructure.Mapping;

using AutoMapper;
using Hangfire.Storage.Monitoring;
using Application.Common.Models;


public class JobMapperProfile : Profile
{
    public JobMapperProfile()
    {
        CreateMap<KeyValuePair<string, ScheduledJobDto>, DelayedMessageDto>()
            .ForMember(m => m.Id, cf =>
                cf.MapFrom(f => f.Key))
            .ForMember(m => m.EnqueueAt, cf =>
                cf.MapFrom(f => f.Value.EnqueueAt))
            .ForMember(m => m.CreatedAt, cf =>
                cf.MapFrom(f => f.Value.ScheduledAt))
            .ForMember(m => m.Text , cf => 
                cf.Ignore());

    }
}