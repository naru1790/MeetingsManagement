using MeetingsManagement.Dtos;
using MeetingsManagement.Dtos.Tasks;
using MeetingsManagement.Requests.Meetings;

namespace MeetingsManagement.Domain.Tasks
{
    public interface ITaskService
    {
        ResponseDto CreateTask(TaskItem task);
        ResponseDto DeleteTask(Guid id);
        IEnumerable<TaskItem> GetTasksByMeetingId(Guid meetingId, PaginatedRequest request);
        ResponseDto UpdateTask(Guid id, TaskItem task);
    }
}