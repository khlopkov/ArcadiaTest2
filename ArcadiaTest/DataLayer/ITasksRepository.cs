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
        ///     Updates due date of task with specified taskId
        /// </summary>
        /// <param name="taskId"> ID of task needed to be updated </param>
        /// <param name="dueDate"> new duedate </param>
        /// <returns>updated entity or null if task with such ID was not found </returns>
        Task UpdateDueDate(int taskId, DateTime? dueDate);

        /// <summary>
        ///     Updates name of task with specified taskId
        /// </summary>
        /// <param name="taskId"> ID of task needed to be updated </param>
        /// <param name="name"> new name </param>
        /// <returns>updated entity or null if task with such ID was not found </returns>
        Task UpdateName(int taskId, string name);

        /// <summary>
        ///     Updates status of task with specified taskId
        /// </summary>
        /// <param name="taskId"> ID of task needed to be updated </param>
        /// <param name="Status"> new status </param>
        /// <returns>updated entity or null if task with such ID was not found </returns>
        Task UpdateStatus(int taskId, string status);

        /// <summary>
        ///     Updates description of task with specified taskId
        /// </summary>
        /// <param name="taskId"> ID of task needed to be updated </param>
        /// <param name="Description"> new description </param>
        /// <returns>updated entity or null if task with such ID was not found </returns>
        Task UpdateDescription(int taskId, string description);

        /// <summary>
        ///     Updates name of task with specified taskId
        /// </summary>
        /// <param name="taskId"> ID of task needed to be updated </param>
        /// <param name="type"> new type </param>
        /// <returns>updated entity or null if task with such ID was not found </returns>
        Task UpdateType(int taskId, string type);

        /// <summary>
        ///     Finds tasks by id of user to whom they belongs
        /// </summary>
        /// <param name="userId"> ID of user </param>
        /// <returns> IEnumerable of found tasks </returns>
        IEnumerable<Task> FindTasksByUserId(int userId);

        /// <summary>
        ///     Counts number of tasks of user with specified id where status is Active
        /// </summary>
        /// <param name="userId">id of user whose tasks needed to be found</param>
        /// <returns> number of counted tasks </returns>
        int CountActiveTasksByUserId(int userId);

        /// <summary>
        ///     Counts number of tasks of user with specified id where status is Resolved
        /// </summary>
        /// <param name="userId">id of user whose tasks needed to be found</param>
        /// <returns> number of counted tasks </returns>
        int CountResolvedTasksByUserId(int userId);

        /// <summary>
        ///     Counts number of tasks of user with specified id where status is Closed
        /// </summary>
        /// <param name="userId">id of user whose tasks needed to be found</param>
        /// <returns> number of counted tasks </returns>
        int CountClosedTasksByUserId(int userId);
    }
}
