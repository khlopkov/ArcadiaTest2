using ArcadiaTest.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.DataLayer
{
    public interface ITasksRepository
    {
        /// <summary>
        ///     Finds task with specified id
        /// </summary>
        /// <param name="id"> ID of task needed to found </param>
        /// <returns> Found task or null if task was not found </returns>
        Task<TaskDTO> FindTaskByIdAsync(int id);

        /// <summary>
        ///     Adds new task into database
        /// </summary>
        /// <param name="task"> Object describing task needed to be updated </param>
        /// <returns> Saved task </returns>
        Task<TaskDTO> SaveAsync(TaskDTO task);

        /// <summary>
        ///     Updates task with passed param
        /// </summary>
        /// <param name="task">task should be updated</param>
        /// <returns>updated task, if updated successfully</returns>
        Task<TaskDTO> UpdateAsync(TaskDTO task);

        /// <summary>
        ///     Finds tasks by id of user to whom they belongs
        /// </summary>
        /// <param name="userId"> ID of user </param>
        /// <returns> IEnumerable of found tasks </returns>
        Task<IEnumerable<TaskDTO>> FindTasksByUserIdAsync(int userId);

        /// <summary>
        ///     Finds tasks by id of user to whom they belongs and status of this tasks
        /// </summary>
        /// <param name="userId"> ID of user </param>
        /// <param name="status"> Status of task</param>
        /// <returns> IEnumerable of found tasks </returns>
        Task<IEnumerable<TaskDTO>> FindTasksByUserIdAndStatusAsync(int userId, string status);

        /// <summary>
        ///     returns count of tasks grouped by status for user with specified userId
        /// </summary>
        /// <param name="userId">ID of user whose taskse should be counted </param>
        /// <returns>Enumrable TasksDashboardDTO containing in status key of grouping by and in count count of tasks with this status</returns>
        Task<IEnumerable<TasksDashboardDTO>> CountTasksGroupedByStatusAsync(int userId);

        /// <summary>
        ///     Deleting task with specified ID
        /// </summary>
        /// <param name="id">id of task which wanted to be deleted</param>
        Task DeleteAsync(TaskDTO task);
    }
}
