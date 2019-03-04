using Newtonsoft.Json;
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
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        ///     Title of task
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        ///     description of task
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        ///     Expiration date of task
        /// </summary>
        [JsonProperty("dueDate")]
        public DateTime? DueDate { get; set; }
        /// <summary>
        ///     Type of task
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        ///     Status of task (e.g. Cancelled, Active, Resolved)
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}