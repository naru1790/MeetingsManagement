using AutoMapper;

using MeetingsManagement.Domain.Tasks;
using MeetingsManagement.Dtos;
using MeetingsManagement.Dtos.Tasks;
using MeetingsManagement.Requests.Meetings;
using MeetingsManagement.Requests.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace MeetingsManagement.Controllers
{
    [ApiController]
    [Route("api")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TasksController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpPost("meetings/{meetingId}/[controller]")]
        public IActionResult CreateTask(Guid meetingId, [FromBody] CreateTaskRequest task)
        {
            var taskItem = _mapper.Map<TaskItem>(task);
            var response = _taskService.CreateTask(taskItem);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("meetings/{meetingId}/[controller]")]
        public IActionResult GetTasksByMeetingId(Guid meetingId, [FromQuery] PaginatedRequest request)
        {
            return Ok(_taskService.GetTasksByMeetingId(meetingId, request));
        }

        [HttpPut("[controller]/{id}")]
        public IActionResult UpdateTask(Guid id, [FromBody] UpdateTaskRequest request)
        {
            var task = _mapper.Map<TaskItem>(request);

            ResponseDto response = _taskService.UpdateTask(id, task);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Response);
            }

            return Ok(response.Response);
        }

        [HttpDelete("[controller]/{id}")]
        public IActionResult DeleteTask(Guid id)
        {
            ResponseDto responseDto = _taskService.DeleteTask(id);
            if (!responseDto.IsSuccess)
            {
                return NotFound(responseDto.Response);
            }
            return Ok(responseDto.Response);
        }
    }
}
