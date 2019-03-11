using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.DTO
{
    public class TasksDashboardDTO
    {
        /// <summary>
        ///     Status of tasks (e.g. Cancelled, Resolved, Active) 
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        ///     Count of task with status
        /// </summary>
        public int Count { get; set; }

        public TasksDashboardDTO() { }

        /// <summary>
        ///     Creates new dashboard showing count of task with each status
        /// </summary>
        /// <param name="status">status of tasks (e.g. Cancelled, Resolved, Active)</param>
        /// <param name="count">count of tasks with this status</param>
        public TasksDashboardDTO (string status, int count)
        {
            this.Status = status;
            this.Count = count;
        }
    }
}