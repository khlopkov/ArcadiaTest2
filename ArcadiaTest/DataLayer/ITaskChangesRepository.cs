using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.DataLayer
{
    interface ITaskChangesRepository
    {
        IEnumerable<TaskChange> FindChangesByTaskID(int taskId);
        IEnumerable<TaskChange> FindChangesByUserId(int userId);
    }
}
