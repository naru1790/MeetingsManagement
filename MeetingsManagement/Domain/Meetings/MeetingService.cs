using MeetingsManagement.Data.Meetings;
using MeetingsManagement.Data.Tasks;
using MeetingsManagement.Dtos;
using MeetingsManagement.Dtos.Meetings;
using MeetingsManagement.Dtos.Tasks;
using MeetingsManagement.Requests.Meetings;

using System.Text.RegularExpressions;

namespace MeetingsManagement.Domain.Meetings
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly ITaskRepository _taskRepository;

        public MeetingService(IMeetingRepository meetingRepository, ITaskRepository taskRepository)
        {
            _meetingRepository = meetingRepository;
            _taskRepository = taskRepository;
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
            return _meetingRepository.Get(id);
        }

        public IEnumerable<Meeting> GetMeetings(PaginatedRequest request)
        {
            if (--request.PageIndex < 0)
            {
                request.PageIndex = 0;
            }

            return _meetingRepository
                .GetAll()
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

        public IEnumerable<MeetingSummary> GetMeetingReport(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                return Enumerable.Empty<MeetingSummary>();
            }

            return from meeting in _meetingRepository.GetAll()
                   join task in _taskRepository.GetAll()
                       on meeting.Id equals task.MeetingId into taskGroup
                   where meeting.IsActive
                       && meeting.StartTime >= start && meeting.StartTime <= end // assuming we are only looking for the starting within a time period.
                   select new MeetingSummary
                   {
                       Title = meeting.Title,
                       Description = meeting.Description,
                       Tasks = taskGroup?.Where(t => t.IsActive)
                                           .Select(t => new TaskSummary
                                           {
                                               Title = t.Title,
                                               Description = t.Description,
                                               Status = t.Status
                                           })
                                           .ToList()
                   };

        }
    }
}
