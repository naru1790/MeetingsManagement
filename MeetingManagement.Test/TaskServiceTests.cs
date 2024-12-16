using MeetingsManagement.Data.Tasks;
using MeetingsManagement.Domain.Tasks;
using MeetingsManagement.Dtos.Tasks;
using MeetingsManagement.Requests.Meetings;

using Moq;

namespace MeetingManagement.Test
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockTaskRepository.Object);
        }

        [Fact]
        public void CreateTask_DueDateInPast_ReturnsErrorResponse()
        {
            // Arrange
            var task = new TaskItem
            {
                DueDate = DateTime.Now.AddDays(-1)
            };

            // Act
            var result = _taskService.CreateTask(task);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Due date cannot be a past date.", result.Response);
            _mockTaskRepository.Verify(repo => repo.Add(It.IsAny<TaskItem>()), Times.Never);
        }

        [Fact]
        public void CreateTask_ValidTask_ReturnsSuccessResponse()
        {
            // Arrange
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                DueDate = DateTime.Now.AddDays(1)
            };

            // Act
            var result = _taskService.CreateTask(task);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Task created successfully.", result.Response);
            Assert.Equal(task.Id, result.Id);
            _mockTaskRepository.Verify(repo => repo.Add(task), Times.Once);
        }

        [Fact]
        public void DeleteTask_ExistingTask_ReturnsSuccessResponse()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            _mockTaskRepository.Setup(repo => repo.Delete(taskId)).Returns(taskId);

            // Act
            var result = _taskService.DeleteTask(taskId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Task deleted Successfully!", result.Response);
            _mockTaskRepository.Verify(repo => repo.Delete(taskId), Times.Once);
        }

        [Fact]
        public void DeleteTask_NonExistingTask_ReturnsErrorResponse()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            _mockTaskRepository.Setup(repo => repo.Delete(taskId)).Returns((Guid?)null);

            // Act
            var result = _taskService.DeleteTask(taskId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Task not found.", result.Response);
            _mockTaskRepository.Verify(repo => repo.Delete(taskId), Times.Once);
        }

        [Fact]
        public void GetTasksByMeetingId_InvalidMeetingId_ReturnsEmptyList()
        {
            // Arrange
            var meetingId = default(Guid);
            var request = new PaginatedRequest { PageIndex = 1, PageCount = 5 };

            // Act
            var result = _taskService.GetTasksByMeetingId(meetingId, request);

            // Assert
            Assert.Empty(result);
            _mockTaskRepository.Verify(repo => repo.GetByMeetingId(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void GetTasksByMeetingId_ValidMeetingId_ReturnsPaginatedTasks()
        {
            // Arrange
            var meetingId = Guid.NewGuid();
            var tasks = new List<TaskItem>
        {
            new TaskItem { Id = Guid.NewGuid() },
            new TaskItem { Id = Guid.NewGuid() }
        };
            _mockTaskRepository.Setup(repo => repo.GetByMeetingId(meetingId)).Returns(tasks);

            var request = new PaginatedRequest { PageIndex = 1, PageCount = 1 };

            // Act
            var result = _taskService.GetTasksByMeetingId(meetingId, request).ToList();

            // Assert
            Assert.Single(result);
            _mockTaskRepository.Verify(repo => repo.GetByMeetingId(meetingId), Times.Once);
        }

        [Fact]
        public void GetTasksByMeetingId_PageIndexNegative_AdjustsToZero()
        {
            // Arrange
            var meetingId = Guid.NewGuid();
            var tasks = new List<TaskItem>
        {
            new TaskItem { Id = Guid.NewGuid() },
            new TaskItem { Id = Guid.NewGuid() }
        };
            _mockTaskRepository.Setup(repo => repo.GetByMeetingId(meetingId)).Returns(tasks);

            var request = new PaginatedRequest { PageIndex = 0, PageCount = 1 };

            // Act
            var result = _taskService.GetTasksByMeetingId(meetingId, request).ToList();

            // Assert
            Assert.Single(result);
            _mockTaskRepository.Verify(repo => repo.GetByMeetingId(meetingId), Times.Once);
        }

        [Fact]
        public void UpdateTask_ExistingTask_ReturnsSuccessResponse()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var task = new TaskItem
            {
                Id = taskId,
                DueDate = DateTime.Now.AddDays(1)
            };

            _mockTaskRepository.Setup(repo => repo.Update(taskId, task)).Returns(taskId);

            // Act
            var result = _taskService.UpdateTask(taskId, task);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Task Updated Successfully!", result.Response);
            _mockTaskRepository.Verify(repo => repo.Update(taskId, task), Times.Once);
        }

        [Fact]
        public void UpdateTask_NonExistingTask_ReturnsErrorResponse()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var task = new TaskItem
            {
                Id = taskId,
                DueDate = DateTime.Now.AddDays(1)
            };

            _mockTaskRepository.Setup(repo => repo.Update(taskId, task)).Returns((Guid?)null);

            // Act
            var result = _taskService.UpdateTask(taskId, task);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Task not found.", result.Response);
            _mockTaskRepository.Verify(repo => repo.Update(taskId, task), Times.Once);
        }
    }
}