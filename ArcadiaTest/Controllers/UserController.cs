using ArcadiaTest.BusinessLayer;
using ArcadiaTest.BusinessLayer.Exceptions;
using ArcadiaTest.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace ArcadiaTest.Controllers
{
    [RoutePrefix("api/user")]
    [Authorize]
    public class UserController : ApiController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var currentUser = await this.GetCurrentUser();
                return Content(HttpStatusCode.OK, currentUser);
            }
            catch(UserNotFoundException)
            {
                return Unauthorized();
            }
        }

        private async Task<UserResponse> GetCurrentUser()
        {
            var email = ((ClaimsIdentity)User.Identity).Name;
            return await this._userService.GetUserWithEmailAsync(email);
        }
    }
}
