using ArcadiaTest.Models.DTO;
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
        IEnumerable<TaskChangeDTO> FindChangesByTaskID(int taskId);

        Task<IEnumerable<TaskChangeDTO>> FindChangesByTaskIDAsync(int taskId);

        IEnumerable<TaskChangeDTO> FindChangesByUserId(int userId);

        /// <summary>
        ///     Finds tasks changes for task which belongs to user
        /// </summary>
        /// <param name="userId"> id of user which  task changes should be queried </param>
        /// <returns> Enumerable of found task changes </returns>
        Task<IEnumerable<TaskChangeDTO>> FindChangesByUserIdAsync(int userId);
    }
}
