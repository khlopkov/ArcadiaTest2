using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.DTO
{
    public class TaskChangeDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Operation { get; set; }
        public System.DateTime ChangedAt { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    
        public TaskDTO Task { get; set; }
    }
}