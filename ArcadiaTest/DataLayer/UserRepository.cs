using ArcadiaTest.Models.DTO;
using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;

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

        public async Task<IEnumerable<UserDTO>> FindAllUsersAsync()
        {
            var users = await this._dbCtx.Users.ToListAsync();
            return users.ToDtos();
        }

        public UserDTO FindByEmail(string email)
        {
            return this._dbCtx.Users.Where(u => u.Email == email).FirstOrDefault()?.ToDto();
        }

        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            var foundUser = await this._dbCtx.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return foundUser?.ToDto();
        }

        public async Task<UserDTO> FindUserByIDAsync(int id)
        {
            var foundUser = await this._dbCtx.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            return foundUser?.ToDto();
        }
    }
    
    public static class UserExtension
    {
        public static UserDTO ToDto(this User userEntity)
        {
            return new UserDTO(userEntity.Id, userEntity.Name, userEntity.Email, userEntity.Hash);
        }

        public static IEnumerable<UserDTO> ToDtos(this IEnumerable<User> userEntities)
        {
            return userEntities.Select(ue => ue.ToDto()).ToList();
        }
    }
}