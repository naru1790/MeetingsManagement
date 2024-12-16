namespace MeetingsManagement.Domain.AutoMapperProfile
{
    using AutoMapper;

    using MeetingsManagement.Dtos.Meetings;
    using MeetingsManagement.Dtos.Tasks;
    using MeetingsManagement.Requests.Meetings;
    using MeetingsManagement.Requests.Tasks;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateMeetingRequest, Meeting>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateMeetingRequest, Meeting>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<CreateTaskRequest, TaskItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateTaskRequest, TaskItem>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

        }
    }
}
