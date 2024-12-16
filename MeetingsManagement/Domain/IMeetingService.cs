using Meetings.Dtos;
using Meetings.Requests;

namespace Meetings.Domain
{
    public interface IMeetingService
    {
        ResponseDto CreateMeeting(Meeting meeting);
        ResponseDto DeleteMeeting(Guid id);
        Meeting GetMeeting(Guid id);
        IEnumerable<MeetingSummary> GetMeetingReport(DateTime start, DateTime end);
        IEnumerable<Meeting> GetMeetings(PaginatedRequest request);
        ResponseDto UpdateMeeting(Guid id, Meeting meeting);
    }
}