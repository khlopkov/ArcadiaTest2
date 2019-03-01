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
        public int Cancelled { get; set; }
        /// <summary>
        ///     Count of active tasks
        /// </summary>
        public int Active { get; set; }
        /// <summary>
        ///     Count of resolved tasks
        /// </summary>
        public int Resolved { get; set; }
    }
}