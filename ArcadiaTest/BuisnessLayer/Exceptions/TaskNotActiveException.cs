﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.BuisnessLayer.Exceptions
{
    public class TaskNotActiveException : Exception
    {
        public TaskNotActiveException() : base("Task not active") { }
    }
}