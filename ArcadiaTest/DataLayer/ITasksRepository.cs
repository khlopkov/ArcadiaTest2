using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadiaTest.DataLayer
{
    interface ITasksRepository
    {
        Task FindTaskById(int userId);
        Task Save(Task task);
        Task Update(Task task);
        IEnumerable<Task> FindTasksByUserId(int userId);
        int CountActiveTasksByUserId(int userId);
        int CountResolvedTasksByUserId(int userId);
        int CountClosedTasksByUserId(int userId);
    }
}
