using AutoMapper;

using Meetings.Domain;
using Meetings.Dtos;
using Meetings.Requests;

using Microsoft.AspNetCore.Mvc;

namespace MeetingsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly IMapper _mapper;

        public MeetingsController(IMeetingService meetingService, IMapper mapper)
        {
            _meetingService = meetingService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public IActionResult CreateMeeting([FromBody] CreateMeetingRequest request)
        {
            var meeting = _mapper.Map<Meeting>(request);
            var responseDto = _meetingService.CreateMeeting(meeting);
            return responseDto.IsSuccess ? Ok(responseDto) : BadRequest(responseDto);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetMeeting(Guid id)
        {

            Meeting meeting = _meetingService.GetMeeting(id);

            if (meeting == null)
            {
                return NotFound("Meeting not found.");
            }
            return Ok(meeting);
        }

        [HttpGet("List")]
        public IActionResult ListMeetings()
        {
            var meetings = _meetingService.GetMeetings();
            return Ok(meetings);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateMeeting(Guid id, [FromBody] Meeting updatedMeeting)
        {
            //if (!Meetings.ContainsKey(id))
            //{
            //    return NotFound("Meeting not found.");
            //}

            //if (updatedMeeting.StartTime >= updatedMeeting.EndTime)
            //{
            //    return BadRequest("Start time must be earlier than end time.");
            //}

            //updatedMeeting.Id = id;
            //Meetings[id] = updatedMeeting;
            //return Ok("Meeting updated successfully.");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteMeeting(Guid id)
        {
           ResponseDto responseDto =  _meetingService.DeleteMeeting(id);
            if (!responseDto.IsSuccess)
            {
                return NotFound("Meeting not found.");
            }
            return Ok("Meeting deleted successfully.");
        }

    }
}
