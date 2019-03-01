using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.BuisnessLayer.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id) :base($"Task with id {id} was not found") { }
        public UserNotFoundException() : base("Task was not found") { }
    }
}