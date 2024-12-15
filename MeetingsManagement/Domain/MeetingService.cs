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
            ResponseDto response = new ResponseDto();

            ValidateMeetingTimings(meeting, response);

            ValidateEmail(meeting, response);

            _meetingRepository.Add(meeting);

            return new ResponseDto
            {
                IsSuccess = true,
                Id = meeting.Id,
                Response = "Meeting created successfully."
            };
        }

        private static void ValidateEmail(Meeting meeting, ResponseDto response)
        {
            string validEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            foreach (var attendee in meeting.Attendees)
            {
                if (string.IsNullOrWhiteSpace(attendee) || !Regex.IsMatch(attendee, validEmail))
                {
                    response = new ResponseDto
                    {
                        Response = "Invalid Attendee email."
                    };
                }
            }
        }

        private static void ValidateMeetingTimings(Meeting meeting, ResponseDto response)
        {
            if (meeting.StartTime >= meeting.EndTime)
            {
                response = new ResponseDto
                {
                    Response = "Start time must be earlier than end time."
                };
            }
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

        public ResponseDto UpdateMeeting(Guid id, Meeting meeting)
        {
            
            Guid? meetingId = _meetingRepository.Update(id, meeting);
            return meetingId != null
                ? new ResponseDto { IsSuccess = true, Response = "Meeting Updated Successfully!" }
                : new ResponseDto { Response = "Meeting not found." };
        }
    }
}
