﻿using MeetingsManagement.Data.Tasks;
using MeetingsManagement.Dtos;
using MeetingsManagement.Dtos.Tasks;
using MeetingsManagement.Requests.Meetings;

namespace MeetingsManagement.Domain.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public ResponseDto CreateTask(TaskItem task)
        {
            ResponseDto response = new ResponseDto();

            if (task.DueDate < DateTime.Now)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Response = "Due date cannot be a past date."
                };
            }

            _taskRepository.Add(task);

            return new ResponseDto
            {
                IsSuccess = true,
                Id = task.Id,
                Response = "Task created successfully."
            };
        }

        public ResponseDto DeleteTask(Guid id)
        {
            var taskId = _taskRepository.Delete(id);
            return taskId != null
                ? new ResponseDto { IsSuccess = true, Response = "Task deleted Successfully!" }
                : new ResponseDto { Response = "Task not found." };
        }

        public IEnumerable<TaskItem> GetTasksByMeetingId(Guid meetingId, PaginatedRequest request)
        {
            if (meetingId == default)
            {
                return Enumerable.Empty<TaskItem>();
            }

            if (--request.PageIndex < 0)
            {
                request.PageIndex = 0;
            }

            return _taskRepository.GetByMeetingId(meetingId)
                .Skip(request.PageCount * request.PageIndex)
                .Take(request.PageCount);
        }

        public ResponseDto UpdateTask(Guid id, TaskItem task)
        {
            Guid? meetingId = _taskRepository.Update(id, task);
            return meetingId != null
                ? new ResponseDto { IsSuccess = true, Response = "Task Updated Successfully!" }
                : new ResponseDto { Response = "Task not found." };
        }
    }
}