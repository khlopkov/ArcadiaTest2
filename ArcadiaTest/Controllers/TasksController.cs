using ArcadiaTest.BusinessLayer;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.Models.Requests;
using ArcadiaTest.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ArcadiaTest.Controllers
{

    [RoutePrefix("api/tasks")]
    [Authorize]
    public class TasksController : ApiController
    {
        const string DUE_DATE_LATER_TODAY_WARNING = "Due date shoul be later then today or today";

        private ITaskService _taskService;
        private IUserService _userService;

        public TasksController
        (
            ITaskService taskSerivce,
            IUserService userSerivce
        )
        {
            this._taskService = taskSerivce;
            this._userService = userSerivce;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
            UserResponse currentUser;
            try
            {
                currentUser = this._userService.GetUserWithEmail(email);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            return Content(HttpStatusCode.OK, this._taskService.GetTasksOfUser(currentUser.Id));
        }

        [HttpPost]
        public IHttpActionResult Post(CreateTaskRequest requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (requestModel.DueDate != default(DateTime) && DateTime.Now.Date > requestModel.DueDate)
                return BadRequest(DUE_DATE_LATER_TODAY_WARNING);
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
            UserResponse currentUser;
            try
            {
                currentUser = this._userService.GetUserWithEmail(email);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            try
            {
                this._taskService.CreateTask(currentUser.Id, requestModel);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            return StatusCode(HttpStatusCode.Created);
        }

        [HttpPatch]
        [Route("{taskId:int}")]
        public IHttpActionResult Patch(int taskId, MergeTaskRequest requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (DateTime.Now.Date > requestModel.DueDate && requestModel.DueDate != default(DateTime))
                return BadRequest(DUE_DATE_LATER_TODAY_WARNING);
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
            UserResponse currentUser;
            try
            {
                currentUser = this._userService.GetUserWithEmail(email);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            try
            {
                this._taskService.GetTaskOfUser(currentUser.Id, taskId);
                this._taskService.UpdateTask(taskId, requestModel);
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
        public IHttpActionResult Delete(int taskId)
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
            UserResponse currentUser;
            try
            {
                currentUser = this._userService.GetUserWithEmail(email);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            try
            {
                this._taskService.GetTaskOfUser(currentUser.Id, taskId);
                this._taskService.DeleteTask(taskId);
            }
            catch(TaskNotFoundException)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("dashboard")]
        public IHttpActionResult GetTaskDashboard()
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
            UserResponse currentUser;
            try
            {
                currentUser = this._userService.GetUserWithEmail(email);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            DashboardResponse response;
            try
            {
                response = this._taskService.GetTasksDashboard(currentUser.Id);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            return Content(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("history")]
        public IHttpActionResult GetTasksHistory()
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
            UserResponse currentUser;
            try
            {
                currentUser = this._userService.GetUserWithEmail(email);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
            return Content(HttpStatusCode.OK, this._taskService.GetTasksHistory(currentUser.Id));
        }
    }
}
