using ArcadiaTest.BusinessLayer;
using ArcadiaTest.BusinessLayer.Exceptions;
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
        public IHttpActionResult Get()
        {
            var claims = ((ClaimsIdentity)User.Identity).Claims;
            var email = claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            try
            {
                var currentUser = this._userService.GetUserWithEmail(email);
            }
            catch(UserNotFoundException)
            {
                Unauthorized();
            }
            return Content(HttpStatusCode.OK, this._userService.GetUserWithEmail(email));
        }
    }
}
