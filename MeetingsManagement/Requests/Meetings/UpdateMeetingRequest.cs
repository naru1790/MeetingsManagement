using System.ComponentModel.DataAnnotations;

namespace MeetingsManagement.Requests.Meetings
{
    public class UpdateMeetingRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required]
        public List<string> Attendees { get; set; }
    }
}
