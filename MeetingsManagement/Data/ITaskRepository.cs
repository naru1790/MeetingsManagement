using Meetings.Dtos;

namespace Meetings.Data
{
    public interface ITaskRepository
    {
        Guid Add(TaskItem task);
        Guid? Delete(Guid id);
        IEnumerable<TaskItem> GetAll();
        IEnumerable<TaskItem> GetByMeetingId(Guid meetingId);
        Guid? Update(Guid id, TaskItem task);
    }
}