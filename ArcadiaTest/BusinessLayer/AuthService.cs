using ArcadiaTest.DataLayer;
using ArcadiaTest.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ArcadiaTest.BusinessLayer
{
    public class AuthService : IAuthService
    {
        public IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public UserDTO Authenticate(string email, string password)
        {
            var userEntity = this._userRepository.FindByEmail(email);
            if (userEntity == null)
            {
                return null;
            }
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var passedPasswordHash = sha1.ComputeHash(Encoding.ASCII.GetBytes(password));
            var passedPasswordHashString = BitConverter.ToString(passedPasswordHash).ToLower().Replace("-", string.Empty);
            if (passedPasswordHashString != userEntity.Hash)
            {
                return null;
            }
            return new UserDTO(userEntity.Id, userEntity.Name, userEntity.Email);
        }
    }
}