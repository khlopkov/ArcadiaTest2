using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.BusinessLayer.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id) :base($"User with id {id} was not found") { }
        public UserNotFoundException() : base("User was not found") { }
    }
}