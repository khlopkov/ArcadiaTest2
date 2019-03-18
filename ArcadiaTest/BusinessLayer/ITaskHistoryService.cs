using ArcadiaTest.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArcadiaTest.BusinessLayer
{
    public interface ITaskHistoryService
    {
        /// <summary>
        ///     Returns task history of user with specified ID
        /// </summary>
        /// <param name="userId">Id of user, whose task should be found</param>
        /// <returns></returns>
        IEnumerable<TaskChangeResponse> GetTasksHistoryOfUser(int userId);
    }
}
