using Meetings.Dtos;

namespace Meetings.Data
{
    public interface IMeetingRepository
    {
        Guid Add(Meeting meeting);
        IEnumerable<Meeting> GetAll();
        Meeting Get(Guid id);
        Guid? Delete(Guid id);
    }
}