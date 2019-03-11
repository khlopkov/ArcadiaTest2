using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.DataLayer
{
    public class UserRepository : IUserRepository
    {
        ArcadiaTestEntities _dbCtx;

        /// <summary>
        ///     Creates new Repository with required dependency of dbContext
        /// </summary>
        public UserRepository(ArcadiaTestEntities dbContext)
        {
            this._dbCtx = dbContext;
        }

        public IEnumerable<User> FindAllUsers()
        {
            return this._dbCtx.Users.ToList();
        }

        public User FindByEmail(string email)
        {
            return this._dbCtx.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User FindFirst()
        {
            return this._dbCtx.Users.FirstOrDefault();
        }

        public User FindUserByID(int id)
        {
            return this._dbCtx.Users.Where(u => u.Id == id).FirstOrDefault();
        }
    }
}