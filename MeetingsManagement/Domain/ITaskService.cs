using Meetings.Dtos;

namespace Meetings.Domain
{
    public interface ITaskService
    {
        ResponseDto CreateTask(TaskItem task);
        ResponseDto DeleteTask(Guid id);
        IEnumerable<TaskItem> GetTasksByMeetingId(Guid meetingId, Requests.PaginatedRequest request);
        ResponseDto UpdateTask(Guid id, TaskItem task);
    }
}