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

        public IEnumerable<UserDTO> FindAllUsers()
        {
            return this._dbCtx.Users.ToList().ToDtos();
        }

        public UserDTO FindByEmail(string email)
        {
            return this._dbCtx.Users.Where(u => u.Email == email).FirstOrDefault().ToDto();
        }

        public UserDTO FindUserByID(int id)
        {
            return this._dbCtx.Users.Where(u => u.Id == id).FirstOrDefault().ToDto();
        }
    }
    
    public static class UserExtension
    {
        public static UserDTO ToDto(this User userEntity)
        {
            return new UserDTO(userEntity.Id, userEntity.Name, userEntity.Email, userEntity.Hash);
        }

        public static IEnumerable<UserDTO> ToDtos(this IReadOnlyCollection<User> userEntities)
        {
            var userDtos = new List<UserDTO>(userEntities.Count);
            foreach(var entity in userEntities)
            {
                userDtos.Add(entity.ToDto());
            }
            return userDtos;
        }
    }
}