using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}