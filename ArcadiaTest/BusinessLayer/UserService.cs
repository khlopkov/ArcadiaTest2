using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.Responses;
using ArcadiaTest.DataLayer;
using ArcadiaTest.BusinessLayer.Exceptions;

namespace ArcadiaTest.BusinessLayer
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public UserResponse GetUserWithEmail(string email)
        {
            var foundUser = this._userRepository.FindByEmail(email);
            if (foundUser == null)
            {
                throw new UserNotFoundException();
            }
            return new UserResponse() { Id = foundUser.Id, Name = foundUser.Name };
        }
    }
}