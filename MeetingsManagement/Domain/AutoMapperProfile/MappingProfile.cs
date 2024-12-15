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
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
          
        }
    }
}
