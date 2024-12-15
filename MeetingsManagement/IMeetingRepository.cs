using Meetings.Dtos;

namespace Meetings.Data
{
    public interface IMeetingRepository
    {
        Guid Add(Meeting meeting);
        IEnumerable<Meeting> GetAll();
    }
}