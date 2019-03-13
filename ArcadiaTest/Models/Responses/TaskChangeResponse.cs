using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.Responses
{
    public class TaskChangeResponse
    {
        [JsonProperty("when")]
        public DateTime When { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}