using ArcadiaTest.BusinessLayer;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.Models.Requests;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace ArcadiaTest.Controllers
{

    [RoutePrefix("api/user/tasks")]
    [Authorize]
    public class TasksController : ApiController
    {
        const string DUE_DATE_LATER_TODAY_WARNING = "Due date shoul be later then today or today";

        private ITaskService _taskService;
        private IUserService _userService;
        private ITaskHistoryService _taskHistoryService;
        private IStatisticsService _statisticsService;

        public TasksController
        (
            ITaskService taskSerivce,
            IUserService userSerivce,
            ITaskHistoryService taskHistoryService,
            IStatisticsService statisticsService
        )
        {
            this._taskService = taskSerivce;
            this._userService = userSerivce;
            this._taskHistoryService = taskHistoryService;
            this._statisticsService = statisticsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;

            try
            {
                var currentUser = await this._userService.GetUserWithEmailAsync(email);
                var response = await this._taskService.GetTasksOfUserAsync(currentUser.Id);
                return Content(HttpStatusCode.OK, response);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(CreateTaskRequest requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (requestModel.DueDate != default(DateTime) && DateTime.Now.Date > requestModel.DueDate)
                return BadRequest(DUE_DATE_LATER_TODAY_WARNING);

            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;

            try
            {
                var currentUser = await this._userService.GetUserWithEmailAsync(email);
                await this._taskService.CreateTaskAsync(currentUser.Id, requestModel);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }

            return StatusCode(HttpStatusCode.Created);
        }

        [HttpPatch]
        [Route("{taskId:int}")]
        public async Task<IHttpActionResult> Patch(int taskId, MergeTaskRequest requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (DateTime.Now.Date > requestModel.DueDate && requestModel.DueDate != default(DateTime))
                return BadRequest(DUE_DATE_LATER_TODAY_WARNING);

            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;

            try
            {
                var currentUser = await this._userService.GetUserWithEmailAsync(email);
                await this._taskService.GetTaskOfUserAsync(currentUser.Id, taskId);
                await this._taskService.UpdateTaskAsync(taskId, requestModel);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            catch(TaskNotFoundException)
            {
                return NotFound();
            }
            catch(TaskNotActiveException)
            {
                return Conflict();
            }

            return StatusCode(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{taskId:int}")]
        public async Task<IHttpActionResult> Delete(int taskId)
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;

            try
            {
                var currentUser = await this._userService.GetUserWithEmailAsync(email);
                await this._taskService.GetTaskOfUserAsync(currentUser.Id, taskId);
                await this._taskService.DeleteTaskAsync(taskId);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            catch(TaskNotFoundException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("dashboard/byStatus")]
        public async Task<IHttpActionResult> GetTaskDashboard()
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;

            try
            {
                var currentUser = this._userService.GetUserWithEmail(email);
                var response = await this._statisticsService.GetStatisticsOfTaskCountGroupedByStatusAsync(currentUser.Id);
                return Content(HttpStatusCode.OK, response);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("history")]
        public async Task<IHttpActionResult> GetTasksHistory()
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;

            try
            {
                var currentUser = await this._userService.GetUserWithEmailAsync(email);
                return Content(HttpStatusCode.OK, await this._taskHistoryService.GetTasksHistoryOfUserAsync(currentUser.Id));
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
        }
    }
}
