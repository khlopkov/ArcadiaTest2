using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.Responses
{
    /// <summary>
    ///     Response for task dashboard
    /// </summary>
    public class DashboardResponse
    {
        /// <summary>
        ///     Count of cancelled tasks
        /// </summary>
        [JsonProperty("cancelled")]
        public int Cancelled { get; set; }
        /// <summary>
        ///     Count of active tasks
        /// </summary>
        [JsonProperty("active")]
        public int Active { get; set; }
        /// <summary>
        ///     Count of resolved tasks
        /// </summary>
        [JsonProperty("resolved")]
        public int Resolved { get; set; }
    }
}