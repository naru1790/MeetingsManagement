using Meetings.Data;
using Meetings.Dtos;

using System.Text.RegularExpressions;

namespace Meetings.Domain
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingService(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }
        public ResponseDto CreateMeeting(Meeting meeting)
        {
            if (meeting.StartTime >= meeting.EndTime)
            {
                return new ResponseDto
                {
                    Response = "Start time must be earlier than end time."
                };
            }
            string validEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            foreach (var attendee in meeting.Attendees)
            {
                if (string.IsNullOrWhiteSpace(attendee) || !Regex.IsMatch(attendee, validEmail))
                {
                    return new ResponseDto
                    {
                        Response = "Invalid Attendee email."
                    };
                }
            }            

            _meetingRepository.Add(meeting);

            return new ResponseDto
            {
                IsSuccess = true,
                Id = meeting.Id,
                Response = "Meeting created successfully."
            };
        }

        public ResponseDto DeleteMeeting(Guid id)
        {
            var meetingId = _meetingRepository.Delete(id);
            return meetingId != null
                ? new ResponseDto { IsSuccess = true, Response = "Meeting deleted Successfully!" }
                : new ResponseDto { Response = "Meeting not found." };
        }

        public Meeting? GetMeeting(Guid id)
        {
            var meeting = _meetingRepository.Get(id);
            return meeting == null || !meeting.IsActive ? null : meeting;
        }

        public IEnumerable<Meeting> GetMeetings()
        {
            return _meetingRepository.GetAll().Where(m => m.IsActive);
        }
    }
}
