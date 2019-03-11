using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.BusinessLayer.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(int id) :base($"Task with id {id} was not found") { }
        public TaskNotFoundException() : base("Task was not found") { }
    }
}