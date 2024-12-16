﻿using System.ComponentModel.DataAnnotations;

namespace Meetings.Requests
{
    public class CreateTaskRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
