using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.DataLayer.Exceptions
{
    public class IdWasNotSpecifiedException : Exception
    {
        public IdWasNotSpecifiedException() : base("Id is required, but was not specified") { }
    }
}