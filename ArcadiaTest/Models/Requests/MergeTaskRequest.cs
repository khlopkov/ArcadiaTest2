using Newtonsoft.Json;
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
        [JsonProperty("dueDate")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        ///     Title of new task
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Description of new task
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Type of task
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Status of task which could be Cancelled, Active or resolved
        /// </summary>
        [JsonProperty("status")]
        [RegularExpression("^(Resolved|Cancelled|Active)$", ErrorMessage ="Status should be 'Resolved', 'Cancelled' or 'Active'")]
        public string Status { get; set; }
    }
}