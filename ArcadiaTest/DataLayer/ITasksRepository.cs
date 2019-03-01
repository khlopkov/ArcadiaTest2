using ArcadiaTest.Models.DTO;
using ArcadiaTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadiaTest.DataLayer
{
    interface ITasksRepository
    {
        /// <summary>
        ///     Finds task with specified id
        /// </summary>
        /// <param name="id"> ID of task needed to found </param>
        /// <returns> Found task or null if task was not found </returns>
        Task FindTaskById(int id);

        /// <summary>
        ///     Adds new task into database
        /// </summary>
        /// <param name="task"> Object describing task needed to be updated </param>
        /// <returns> Saved task </returns>
        Task Save(Task task);

        /// <summary>
        ///     Updates task with passed param
        /// </summary>
        /// <param name="task">task should be updated</param>
        /// <returns>updated task, if updated successfully</returns>
        Task Update(Task task);

        /// <summary>
        ///     Finds tasks by id of user to whom they belongs
        /// </summary>
        /// <param name="userId"> ID of user </param>
        /// <returns> IEnumerable of found tasks </returns>
        IEnumerable<Task> FindTasksByUserId(int userId);
        /// <summary>
        ///     Finds tasks by id of user to whom they belongs and status of this tasks
        /// </summary>
        /// <param name="userId"> ID of user </param>
        /// <param name="status"> Status of task</param>
        /// <returns> IEnumerable of found tasks </returns>
        IEnumerable<Task> FindTasksByUserIdAndStatus(int userId, string status);

        /// <summary>
        ///     returns count of tasks grouped by status for user with specified userId
        /// </summary>
        /// <param name="userId">ID of user whose taskse should be counted </param>
        /// <returns>Enumrable TasksDashboardDTO containing in status key of grouping by and in count count of tasks with this status</returns>
        IEnumerable<TasksDashboardDTO> CountTasksGroupedByStatus(int userId);
    }
}
