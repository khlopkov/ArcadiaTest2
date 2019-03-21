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
        private IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public UserDTO Authenticate(string email, string password)
        {
            var user = this._userRepository.FindByEmail(email);
            if (user == null)
            {
                return null;
            }
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var passedPasswordHash = sha1.ComputeHash(Encoding.ASCII.GetBytes(password));
            var passedPasswordHashString = BitConverter.ToString(passedPasswordHash).ToLower().Replace("-", string.Empty);
            if (passedPasswordHashString != user.Hash)
            {
                return null;
            }
            return new UserDTO(user.Id, user.Name, user.Email, user.Hash);
        }
    }
}