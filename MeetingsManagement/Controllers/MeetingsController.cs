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

        [HttpPost()]
        public IActionResult CreateMeeting([FromBody] CreateMeetingRequest request)
        {
            var meeting = _mapper.Map<Meeting>(request);
            var responseDto = _meetingService.CreateMeeting(meeting);
            return responseDto.IsSuccess ? Ok(responseDto) : BadRequest(responseDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetMeeting(Guid id)
        {

            Meeting meeting = _meetingService.GetMeeting(id);

            if (meeting == null)
            {
                return NotFound("Meeting not found.");
            }
            return Ok(meeting);
        }

        [HttpGet()]
        public IActionResult ListMeetings([FromQuery] PaginatedRequest request)
        {
            var meetings = _meetingService.GetMeetings(request);
            return Ok(meetings);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMeeting(Guid id, [FromBody] UpdateMeetingRequest updatedMeeting)
        {
            var meeting = _mapper.Map<Meeting>(updatedMeeting);

            ResponseDto response =  _meetingService.UpdateMeeting(id, meeting);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Response);
            }

            return Ok(response.Response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(Guid id)
        {
            ResponseDto responseDto =  _meetingService.DeleteMeeting(id);
            if (!responseDto.IsSuccess)
            {
                return NotFound(responseDto.Response);
            }
            return Ok(responseDto.Response);
        }
    }
}
