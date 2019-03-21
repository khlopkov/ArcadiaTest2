using ArcadiaTest.Models.DTO;
using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.DataLayer
{
    public interface IUserRepository
    {
        /// <summary>
        ///     Finds user with specified id
        /// </summary>
        /// <param name="id"> id of user needed to be found </param>
        /// <returns> Found user or null </returns>
        Task<UserDTO> FindUserByIDAsync(int id);

        /// <summary>
        ///     Finds all users in database
        /// </summary>
        /// <returns> IEnumerable containing all existing users </returns>
        Task<IEnumerable<UserDTO>> FindAllUsersAsync();

        /// <summary>
        ///     Finds user by email
        /// </summary>
        /// <param name="email">email of user, who needed to be found</param>
        /// <returns>Entity describing found user, or null if not found</returns>
        UserDTO FindByEmail(string email);

        Task<UserDTO> FindByEmailAsync(string email);
    }
}
