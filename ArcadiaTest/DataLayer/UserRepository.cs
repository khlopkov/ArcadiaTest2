using ArcadiaTest.Models.DTO;
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

        private UserDTO userEntityToDto(User userEntity)
        {
            return new UserDTO(userEntity.Id, userEntity.Name, userEntity.Email, userEntity.Hash);
        }

        private IEnumerable<UserDTO> userEntitiesToUserDtos(IReadOnlyCollection<User> userEntities)
        {
            var userDtos = new List<UserDTO>(userEntities.Count);
            foreach(var entity in userEntities)
            {
                userDtos.Add(this.userEntityToDto(entity));
            }
            return userDtos;
        }

        public IEnumerable<UserDTO> FindAllUsers()
        {
            return userEntitiesToUserDtos(
               this._dbCtx.Users.ToList()
            );
        }

        public UserDTO FindByEmail(string email)
        {
            return userEntityToDto(this._dbCtx.Users.Where(u => u.Email == email).FirstOrDefault());
        }

        public UserDTO FindUserByID(int id)
        {
            return userEntityToDto(this._dbCtx.Users.Where(u => u.Id == id).FirstOrDefault());
        }
    }
}