using Meetings.Dtos;

namespace Meetings.Data
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly Dictionary<Guid, Meeting> _meetings = new();

        public MeetingRepository()
        {
            for (int i = 0; i < 5; i++)
            {
                var meeting = new Meeting
                {
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

            if(meeting != null)
            {
                meeting.IsActive = false;
            }

            return meeting?.Id;
        }

        public Meeting Get(Guid id)
        {
            _meetings.TryGetValue(id, out var meeting);

            return meeting;
        }

        public IEnumerable<Meeting> GetAll()
        {
            return _meetings.Values;
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
