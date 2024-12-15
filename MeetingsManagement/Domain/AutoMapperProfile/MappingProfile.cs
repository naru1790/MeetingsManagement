namespace Meetings.Domain.AutoMapperProfile
{
    using AutoMapper;

    using Meetings.Dtos;
    using Meetings.Requests;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateMeetingRequest, Meeting>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateMeetingRequest, Meeting>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

        }
    }
}
