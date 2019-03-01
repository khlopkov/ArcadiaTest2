using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.Responses
{
    /// <summary>
    ///     Response model for user
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        ///     ID of user
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///     Name of user
        /// </summary>
        public string Name { get; set; }
    }
}