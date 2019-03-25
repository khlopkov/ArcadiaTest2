﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArcadiaTest.Models.Responses;
using ArcadiaTest.DataLayer;
using ArcadiaTest.BusinessLayer.Exceptions;
using System.Threading.Tasks;

namespace ArcadiaTest.BusinessLayer
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<UserResponse> GetUserWithEmailAsync(string email)
        {
            var foundUser = await this._userRepository.FindByEmailAsync(email);
            if (foundUser == null)
                throw new UserNotFoundException();

            return new UserResponse() { Id = foundUser.Id, Name = foundUser.Name };
        }

        public async Task<UserResponse> GetUserWithEmailAsync(string email)
        {
            var foundUser = await this._userRepository.FindByEmailAsync(email);
            if (foundUser == null)
                throw new UserNotFoundException();

            return new UserResponse() { Id = foundUser.Id, Name = foundUser.Name };
        }
    }
}