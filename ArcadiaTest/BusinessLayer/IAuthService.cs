using ArcadiaTest.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.BusinessLayer
{
    public interface IAuthService
    {
        UserDTO Authenticate(string email, string password);
    }
}