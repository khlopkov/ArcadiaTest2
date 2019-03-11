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
        User FindUserByID(int id);
        /// <summary>
        ///     Finds all users in database
        /// </summary>
        /// <returns> IEnumerable containing all existing users </returns>
        IEnumerable<User> FindAllUsers();
        /// <summary>
        ///     Finds first row in user table of database
        /// </summary>
        /// <returns> Found user or null </returns>
        User FindFirst();
        /// <summary>
        ///     Finds user by email
        /// </summary>
        /// <param name="email">email of user, who needed to be found</param>
        /// <returns>Entity describing found user, or null if not found</returns>
        User FindByEmail(string email);
    }
}
