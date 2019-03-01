using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.Responses
{
    /// <summary>
    ///     Response model for task
    /// </summary>
    public class TaskResponse
    {
        /// <summary>
        ///     ID of task
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///     Title of task
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///     description of task
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///     Expiration date of task
        /// </summary>
        public DateTime? DueDate { get; set; }
        /// <summary>
        ///     Type of task
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        ///     Status of task (e.g. Cancelled, Active, Resolved)
        /// </summary>
        public string Status { get; set; }
    }
}