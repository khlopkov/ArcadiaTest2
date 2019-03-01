using ArcadiaTest.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.BuisnessLayer
{
    interface IUserService
    {
        /// <summary>
        ///     Gets current user. In this version returns first found in DB user
        /// </summary>
        /// <returns>Response containing user</returns>
        UserResponse GetCurrentUser(); 
    }
}
