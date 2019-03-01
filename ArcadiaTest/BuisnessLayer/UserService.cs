using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.Responses;
using ArcadiaTest.DataLayer;

namespace ArcadiaTest.BuisnessLayer
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public UserResponse GetCurrentUser()
        {
            var userEntity = this._userRepository.FindFirst();
            return new UserResponse
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
            };
        }
    }
}