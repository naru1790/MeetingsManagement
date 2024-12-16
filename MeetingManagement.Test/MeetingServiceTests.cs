using MeetingsManagement.Data.Meetings;
using MeetingsManagement.Data.Tasks;
using MeetingsManagement.Domain.Meetings;
using MeetingsManagement.Dtos.Meetings;
using MeetingsManagement.Dtos.Tasks;
using MeetingsManagement.Requests.Meetings;

using Moq;

namespace MeetingManagement.Test
{
    public class MeetingServiceTests
    {
        private readonly Mock<IMeetingRepository> _mockMeetingRepository;
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly MeetingService _meetingService;

        public MeetingServiceTests()
        {
            _mockMeetingRepository = new Mock<IMeetingRepository>();
            _mockTaskRepository = new Mock<ITaskRepository>();
            _meetingService = new MeetingService(_mockMeetingRepository.Object, _mockTaskRepository.Object);
        }

        [Fact]
        public void CreateMeeting_InvalidTimings_ReturnsErrorResponse()
        {
            // Arrange
            var meeting = new Meeting
            {
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now
            };

            // Act
            var result = _meetingService.CreateMeeting(meeting);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Start time must be earlier than end time.", result.Response);
        }

        [Fact]
        public void CreateMeeting_InvalidEmail_ReturnsErrorResponse()
        {
            // Arrange
            var meeting = new Meeting
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Attendees = new List<string> { "invalid-email" }
            };

            // Act
            var result = _meetingService.CreateMeeting(meeting);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid Attendee email.", result.Response);
        }

        [Fact]
        public void CreateMeeting_ValidInput_ReturnsSuccessResponse()
        {
            // Arrange
            var meeting = new Meeting
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Attendees = new List<string> { "test@example.com" }
            };

            // Act
            var result = _meetingService.CreateMeeting(meeting);

            // Assert
            _mockMeetingRepository.Verify(m => m.Add(It.IsAny<Meeting>()), Times.Once);
            Assert.True(result.IsSuccess);
            Assert.Equal("Meeting created successfully.", result.Response);
        }

        [Fact]
        public void DeleteMeeting_ExistingMeeting_ReturnsSuccessResponse()
        {
            // Arrange
            var meetingId = Guid.NewGuid();
            _mockMeetingRepository.Setup(repo => repo.Delete(meetingId)).Returns(meetingId);

            // Act
            var result = _meetingService.DeleteMeeting(meetingId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Meeting deleted Successfully!", result.Response);
        }

        [Fact]
        public void DeleteMeeting_NonExistingMeeting_ReturnsErrorResponse()
        {
            // Arrange
            var meetingId = Guid.NewGuid();
            _mockMeetingRepository.Setup(repo => repo.Delete(meetingId)).Returns((Guid?)null);

            // Act
            var result = _meetingService.DeleteMeeting(meetingId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Meeting not found.", result.Response);
        }

        [Fact]
        public void GetMeeting_ExistingMeeting_ReturnsMeeting()
        {
            // Arrange
            var meetingId = Guid.NewGuid();
            var meeting = new Meeting { Id = meetingId };
            _mockMeetingRepository.Setup(repo => repo.Get(meetingId)).Returns(meeting);

            // Act
            var result = _meetingService.GetMeeting(meetingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(meetingId, result.Id);
        }

        [Fact]
        public void GetMeeting_NonExistingMeeting_ReturnsNull()
        {
            // Arrange
            var meetingId = Guid.NewGuid();
            _mockMeetingRepository.Setup(repo => repo.Get(meetingId)).Returns((Meeting)null);

            // Act
            var result = _meetingService.GetMeeting(meetingId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetMeetings_ValidRequest_ReturnsPaginatedMeetings()
        {
            // Arrange
            var meetings = new List<Meeting>
        {
            new Meeting { Id = Guid.NewGuid() },
            new Meeting { Id = Guid.NewGuid() }
        };

            _mockMeetingRepository.Setup(repo => repo.GetAll()).Returns(meetings);

            var request = new PaginatedRequest { PageIndex = 1, PageCount = 1 };

            // Act
            var result = _meetingService.GetMeetings(request).ToList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void UpdateMeeting_ValidInput_ReturnsSuccessResponse()
        {
            // Arrange
            var meetingId = Guid.NewGuid();
            var meeting = new Meeting
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Attendees = new List<string> { "test@example.com" }
            };

            _mockMeetingRepository.Setup(repo => repo.Update(meetingId, meeting)).Returns(meetingId);

            // Act
            var result = _meetingService.UpdateMeeting(meetingId, meeting);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Meeting Updated Successfully!", result.Response);
        }

        [Fact]
        public void UpdateMeeting_InvalidTimings_ReturnsErrorResponse()
        {
            // Arrange
            var meeting = new Meeting
            {
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now
            };

            // Act
            var result = _meetingService.UpdateMeeting(Guid.NewGuid(), meeting);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Start time must be earlier than end time.", result.Response);
        }

        [Fact]
        public void GetMeetingReport_ValidInput_ReturnsMeetingSummary()
        {
            // Arrange
            var meetings = new List<Meeting>
        {
            new Meeting
            {
                Id = Guid.NewGuid(),
                Title = "Meeting 1",
                Description = "Description",
                StartTime = DateTime.Now,
                IsActive = true
            }
        };

            var tasks = new List<TaskItem>
        {
            new TaskItem
            {
                MeetingId = meetings.First().Id,
                Title = "Task 1",
                Description = "Description",
                Status = "Open",
                IsActive = true
            }
        };

            _mockMeetingRepository.Setup(repo => repo.GetAll()).Returns(meetings);
            _mockTaskRepository.Setup(repo => repo.GetAll()).Returns(tasks);

            // Act
            var result = _meetingService.GetMeetingReport(DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1)).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Meeting 1", result.First().Title);
            Assert.Single(result.First().Tasks);
        }
    }
}