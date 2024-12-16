using MeetingsManagement.Dtos;
using MeetingsManagement.Dtos.Meetings;

namespace MeetingsManagement.Data.Meetings
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly Dictionary<Guid, Meeting> _meetings = new();
        private readonly Guid[] _guids =
        [
            new("28cd5bb3-f90e-4217-ab73-678e5160347c"),
            new("cc2b5b17-2c03-4561-aea4-7da59e4d010b"),
            new("5133a319-46a0-40fa-97c5-9ed3e236c5d6"),
            new("f1f0cc3b-de0a-4e95-bfb5-b04e52831a7b"),
            new("01734672-9278-4ae6-b234-2144f50f684a"),

        ];

        public MeetingRepository()
        {
            for (int i = 0; i < 5; i++)
            {
                var meeting = new Meeting
                {
                    Id = _guids[i],
                    Description = $"Meeting Description {i}",
                    Title = $"Meeting Title {i}",
                    IsActive = true,
                    Attendees = new List<string>() { "testAttendee1@gmai.com", "testAttendee2@gmail.com" },
                    StartTime = DateTime.Today.AddHours(1),
                    EndTime = DateTime.Today.AddHours(2),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _meetings.Add(meeting.Id, meeting);
            }
        }

        public Guid Add(Meeting meeting)
        {
            _meetings.Add(meeting.Id, meeting);

            return meeting.Id;
        }

        public Guid? Delete(Guid id)
        {
            _meetings.TryGetValue(id, out var meeting);

            if (meeting != null)
            {
                meeting.IsActive = false;
            }

            return meeting?.Id;
        }

        public Meeting Get(Guid id)
        {
            _meetings.TryGetValue(id, out var meeting);

            if (meeting != null && meeting.IsActive)
            {
                return meeting;
            }

            return null;
        }

        public IEnumerable<Meeting> GetAll()
        {
            return _meetings.Values.Where(m => m.IsActive);
        }

        public Guid? Update(Guid id, Meeting meeting)
        {
            _meetings.TryGetValue(id, out var existingMeeting);

            if (existingMeeting != null)
            {
                existingMeeting.Title = meeting.Title;
                existingMeeting.Description = meeting.Description;
                existingMeeting.StartTime = meeting.StartTime;
                existingMeeting.EndTime = meeting.EndTime;
                existingMeeting.Attendees = meeting.Attendees;
                existingMeeting.UpdatedAt = DateTime.Now;
            }

            return existingMeeting?.Id;
        }
    }
}
