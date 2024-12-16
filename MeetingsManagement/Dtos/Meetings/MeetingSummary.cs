using MeetingsManagement.Dtos.Tasks;

namespace MeetingsManagement.Dtos.Meetings
{
    public class MeetingSummary
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<TaskSummary> Tasks { get; set; }

    }
}
