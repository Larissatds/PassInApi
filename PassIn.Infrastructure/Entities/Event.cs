﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure.Entities
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; } 

        public string Title { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public int Maximum_Attendees { get; set; }

        [ForeignKey("Event_Id")]
        public List<Attendee> Attendees { get; set; } = new List<Attendee>();
    }
}
