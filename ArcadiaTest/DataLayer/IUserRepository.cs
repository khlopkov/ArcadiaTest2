using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.DataLayer
{
    interface IUserRepository
    {
        User FindUserByID(int id);
        IEnumerable<User> FindAllUsers(int id);
        User FindFirst();
    }
}
