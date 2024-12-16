using MeetingsManagement.Dtos.Meetings;

namespace MeetingsManagement.Data.Meetings
{
    public interface IMeetingRepository
    {
        Guid Add(Meeting meeting);
        IEnumerable<Meeting> GetAll();
        Meeting Get(Guid id);
        Guid? Delete(Guid id);
        Guid? Update(Guid id, Meeting meeting);
    }
}