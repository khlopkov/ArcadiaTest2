using ArcadiaTest.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ArcadiaTest.BusinessLayer
{
    public interface IAuthService
    {
        UserDTO GetUserByCredentials(string email, string password);

        Task<UserDTO> GetUserByCredentialsAsync(string email, string password);
    }
}