using ArcadiaTest.Models.DTO;
using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.DataLayer
{
    public interface ITaskChangesRepository
    {
        /// <summary>
        ///     Finds tasks changes for task with specified id
        /// </summary>
        /// <param name="taskId"> id of task which changes should be queried </param>
        /// <returns> Enumerable of found task changes </returns>
        IEnumerable<TaskChange> FindChangesByTaskID(int taskId);

        /// <summary>
        ///     Finds tasks changes for task which belongs to user
        /// </summary>
        /// <param name="userId"> id of user which  task changes should be queried </param>
        /// <returns> Enumerable of found task changes </returns>
        IEnumerable<TaskChangeDTO> FindChangesByUserId(int userId);
    }
}
