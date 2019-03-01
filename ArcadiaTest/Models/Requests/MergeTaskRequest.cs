using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.Requests
{
    /// <summary>
    ///     Request model for merging task entity with passed params
    /// </summary>
    public class MergeTaskRequest
    {
        /// <summary>
        ///     date when task becomes expired
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        ///     Title of new task
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Description of new task
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Type of task
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     Status of task which could be Cancelled, Active or resolved
        /// </summary>
        [RegularExpression("^(Resolved|Cancelled|Active)$", ErrorMessage ="Status should be 'Resolved', 'Cancelled' or 'Active'")]
        public string Status { get; set; }
    }
}