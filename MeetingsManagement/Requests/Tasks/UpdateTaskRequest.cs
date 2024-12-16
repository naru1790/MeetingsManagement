﻿using System.ComponentModel.DataAnnotations;

namespace MeetingsManagement.Requests.Tasks
{
    public class UpdateTaskRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }
        [Required]
        public Guid? MeetingId { get; set; }
        [Required]
        public string Status { get; set; }
    }
}