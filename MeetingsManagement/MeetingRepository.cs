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

        public IEnumerable<Meeting> GetAll()
        {
            return _meetings.Values;
        }
    }
}
