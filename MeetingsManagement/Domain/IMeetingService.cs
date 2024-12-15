using Meetings.Dtos;

namespace Meetings.Domain
{
    public interface IMeetingService
    {
        ResponseDto CreateMeeting(Meeting meeting);
        ResponseDto DeleteMeeting(Guid id);
        Meeting GetMeeting(Guid id);
        IEnumerable<Meeting> GetMeetings();
        ResponseDto UpdateMeeting(Guid id, Meeting meeting);
    }
}