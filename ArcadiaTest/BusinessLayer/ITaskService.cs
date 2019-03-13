using ArcadiaTest.Models.Requests;
using ArcadiaTest.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaTest.BusinessLayer
{
    public interface ITaskService
    {
        /// <summary>
        ///     Finds tasks of user with specified id
        /// </summary>
        /// <param name="userId">ID of user, whose tasks should be found</param>
        /// <returns></returns>
        IEnumerable<TaskResponse> GetTasksOfUser(int userId);

        /// <summary>
        ///     Get tasks with specified status of user with specified id
        /// </summary>
        /// <param name="userId">ID of user, whose tasks should be found</param>
        /// <param name="status">Status with which tasks should be found</param>
        /// <returns></returns>
        IEnumerable<TaskResponse> GetTasksOfUser(int userId, string status);
        /// <summary>
        ///     Finds task which belongs to user
        /// </summary>
        /// <param name="userId">ID of user, whose task trying to find</param>
        /// <param name="taskId">ID of task wanted to be found</param>
        /// <exception cref="ArcadiaTest.BusinessLayer.Exceptions.TaskNotFoundException">
        ///     if task with specified id was not found
        /// </exception>
        /// <returns>Found taks which belongs to user</returns>
        TaskResponse GetTaskOfUser(int userId, int taskId);

        /// <summary>
        ///     Get task with specified id
        /// </summary>
        /// <exception cref="ArcadiaTest.BusinessLayer.Exceptions.TaskNotFoundException">
        ///     if task with specified id was not found
        /// </exception>
        /// <param name="id">id of required task</param>
        /// <returns> Response of task</returns>
        TaskResponse GetTask(int id);

        /// <summary>
        ///     Finds task and merge it with fields passed in patchModel
        /// </summary>
        /// <param name="id">id of task needed to be patched</param>
        /// <param name="patchModel">model containing fields with which model should be merged</param>
        /// <exception cref="ArcadiaTest.BusinessLayer.Exceptions.TaskNotFoundException">
        ///     if task with specified id was not found
        /// </exception>
        /// <exception cref="ArcadiaTest.BusinessLayer.Exceptions.TaskNotActiveException">
        ///     If status of task that want to be patched is not Active
        /// </exception>
        void UpdateTask(int id, MergeTaskRequest updateModel);

        /// <summary>
        ///     Creates new task for user
        /// </summary>
        /// <param name="userId"> User for whom task creating </param>
        /// <param name="payload"> Model containing properties of task </param>
        /// <exception cref="ArcadiaTest.BusinessLayer.Exceptions.UserNotFoundException">
        ///     if user with specified id was not found
        /// </exception>
        void CreateTask(int userId, CreateTaskRequest payload);

        /// <summary>
        ///     Get dashboard with count of tasks with each status
        /// </summary>
        /// <param name="userId">ID of user, whose dashboard is requested</param>
        /// <returns> Response containing count of tasks with each status </returns>
        DashboardResponse GetTasksDashboard(int userId);

        /// <summary>
        ///     Deletes taks with specified id
        /// </summary>
        /// <param name="taskId">id of task that wanted to be deleted</param>
        /// <exception cref="ArcadiaTest.BusinessLayer.Exceptions.TaskNotFoundException">
        ///     if task with specified id was not found
        /// </exception>
        void DeleteTask(int taskId);

        /// <summary>
        ///     Returns task history of user with specified ID
        /// </summary>
        /// <param name="userId">Id of user, whose task should be found</param>
        /// <returns></returns>
        IEnumerable<TaskChangeResponse> GetTasksHistory(int userId);
    }
}
