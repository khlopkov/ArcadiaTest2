using ArcadiaTest.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.BusinessLayer
{
    public interface IUserService
    {
        /// <summary>
        ///     Gets user with specified email
        /// </summary>
        /// <returns>Response containing user</returns>
        UserResponse GetUserWithEmail(string email); 

        Task<UserResponse> GetUserWithEmailAsync(string email); 
    }
}
