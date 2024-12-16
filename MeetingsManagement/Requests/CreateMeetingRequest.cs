﻿using System.ComponentModel.DataAnnotations;

namespace Meetings.Requests
{
    public class CreateMeetingRequest
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

    public class PaginatedRequest
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
