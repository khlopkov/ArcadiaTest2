using ArcadiaTest.BusinessLayer;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.Models.Requests;
using ArcadiaTest.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArcadiaTest.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
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
            var currentUser = this._userService.GetCurrentUser();
            return Content(HttpStatusCode.OK, this._taskService.GetTasksOfUser(currentUser.Id));
        }

        [HttpPost]
        public IHttpActionResult Post(CreateTaskRequest requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var currentUser = this._userService.GetCurrentUser();
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
                return BadRequest("Due date shoul be later then today or today");
            try
            {
                this._taskService.PatchTask(taskId, requestModel);
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
            try
            {
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
            var currentUser = this._userService.GetCurrentUser();
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
    }
}
