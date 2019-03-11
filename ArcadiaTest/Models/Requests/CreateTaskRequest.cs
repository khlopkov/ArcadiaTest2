using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.Requests
{
    /// <summary>
    ///     Request model for creating new task
    /// </summary>
    public class CreateTaskRequest
    {
        /// <summary>
        ///     date when task becomes expired
        /// </summary>
        [JsonProperty("dueDate")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        ///     Title of new task. Required field
        /// </summary>
        [JsonProperty("title")]
        [Required(ErrorMessage = "Title was not supplied")]
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
    }
}