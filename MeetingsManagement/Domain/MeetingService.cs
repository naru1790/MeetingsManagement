using Meetings.Data;
using Meetings.Dtos;
using Meetings.Requests;

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
            // use fluent validation instead of manual code.
            if (IsInvalidTimings(meeting))
            {
                return new ResponseDto
                {
                    Response = "Start time must be earlier than end time."
                };
            }

            foreach (var attendee in meeting.Attendees)
            {
                if (IsInvalidEmail(attendee))
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

        private static bool IsInvalidEmail(string attendee)
        {
            string validEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return string.IsNullOrWhiteSpace(attendee) || !Regex.IsMatch(attendee, validEmail);
        }

        private static bool IsInvalidTimings(Meeting meeting)
        {
            return meeting.StartTime >= meeting.EndTime;
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

        public IEnumerable<Meeting> GetMeetings(PaginatedRequest request)
        {
            if (--request.PageIndex < 0)
            {
                request.PageIndex = 0;
            }

            return _meetingRepository
                .GetAll()
                .Where(m => m.IsActive)
                .Skip(request.PageIndex * request.PageCount)
                .Take(request.PageCount);
        }

        public ResponseDto UpdateMeeting(Guid id, Meeting meeting)
        {
            if (IsInvalidTimings(meeting))
            {
                return new ResponseDto
                {
                    Response = "Start time must be earlier than end time."
                };
            }

            foreach (var attendee in meeting.Attendees)
            {
                if (IsInvalidEmail(attendee))
                {
                    return new ResponseDto
                    {
                        Response = "Invalid Attendee email."
                    };
                }
            }

            Guid? meetingId = _meetingRepository.Update(id, meeting);
            return meetingId != null
                ? new ResponseDto { IsSuccess = true, Response = "Meeting Updated Successfully!" }
                : new ResponseDto { Response = "Meeting not found." };
        }
    }
}
