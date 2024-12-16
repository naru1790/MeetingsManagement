using Meetings.Dtos;

namespace Meetings.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly Dictionary<Guid, TaskItem> _tasks = new();
        private readonly Guid[] _guids =
        [
            new("28cd5bb3-f90e-4217-ab73-678e5160347c"),
            new("cc2b5b17-2c03-4561-aea4-7da59e4d010b"),
            new("5133a319-46a0-40fa-97c5-9ed3e236c5d6"),
            new("f1f0cc3b-de0a-4e95-bfb5-b04e52831a7b"),
            new("01734672-9278-4ae6-b234-2144f50f684a"),

        ];
        private readonly Guid[] _taskGuids =
        [
            new("27c015d9-e734-430b-9c2b-5f180dc666f5"),
            new("fda5cb0d-baca-4ee0-a047-1c94c290f932"),
            new("6a406e35-56c3-4c07-8b97-509ae3c9df66"),
            new("48961523-0d4b-4751-9501-c2c18dfc9135"),
            new("f4cfe6d0-7bf0-4032-97bd-c421ec3475a8"),

        ];
        public TaskRepository()
        {
            for (int i = 0; i < 5; i++)
            {
                var task = new TaskItem()
                {
                    Id = _taskGuids[i],
                    Description = $"Task Description {i}",
                    Title = $"Task Title {i}",
                    IsActive = true,
                    DueDate = DateTime.Today.AddHours(1),
                    Status = "Pending",
                    MeetingId = _guids[i],
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,

                };

                _tasks.Add(task.Id, task);
            }
        }
        public Guid Add(TaskItem task)
        {
            _tasks.Add(task.Id, task);

            return task.Id;
        }

        public Guid? Delete(Guid id)
        {
            _tasks.TryGetValue(id, out var task);

            if (task != null)
            {
                task.IsActive = false;
            }

            return task?.Id;
        }

        public IEnumerable<TaskItem> GetByMeetingId(Guid meetingId)
        {
            return _tasks.Values
                .Where(t => t.MeetingId == meetingId)
                .ToList();
        }

        public Guid? Update(Guid id, TaskItem task)
        {
            _tasks.TryGetValue(id, out var existingTask);

            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.UpdatedAt = DateTime.Now;
            }

            return existingTask?.Id;
        }
    }
}