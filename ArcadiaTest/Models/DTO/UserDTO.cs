﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArcadiaTest.Models.DTO
{
    public class UserDTO
    {
        /// <summary>
        ///     ID of user
        /// </summary>
        public int Id { get; }
        /// <summary>
        ///     Email of user
        /// </summary>
        public string Email { get; }
        /// <summary>
        ///     Name of user
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Creates new UserDTO
        /// </summary>
        /// <param name="id">id of user</param>
        /// <param name="name">name of user</param>
        /// <param name="email">email of user</param>
        public UserDTO(int id, string name, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
        }
    }
}