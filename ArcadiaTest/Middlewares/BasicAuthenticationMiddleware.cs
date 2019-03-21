using ArcadiaTest.BusinessLayer;
using ArcadiaTest.Models.DTO;
using Microsoft.Owin;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArcadiaTest.Middlewares
{
    public class BasicAuthenticationMiddleware : OwinMiddleware
    {
        private readonly IAuthService _authService;

        public BasicAuthenticationMiddleware(OwinMiddleware next, IAuthService authService) : base(next)
        {
            this._authService = authService;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var authHeader = context.Request.Headers.Get("Authorization");
            if (authHeader == null || authHeader == "" || authHeader.Substring(0, "Basic".Length) != "Basic")
            {
                context.Response.Headers.Set("WWW-Authenticate", "Basic");
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            var authToken = context.Request.Headers
                .Get("Authorization").Substring("Basic ".Length);

            var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                Convert.FromBase64String(authToken));

            var splittedToken = decodeauthToken.Split(':');
            if (splittedToken.Length != 2)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            var user = this._authService.GetUserByCredentials(splittedToken[0], splittedToken[1]);
            if (user != null)
            {
                context.Set<UserDTO>("user", user);
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("Id", user.Id.ToString())
                };
                var identity = new ClaimsIdentity(claims, "Basic");
                context.Request.User = new ClaimsPrincipal(identity);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            await this.Next.Invoke(context);
        }
    }
}