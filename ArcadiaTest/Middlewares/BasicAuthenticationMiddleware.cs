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
        private const string BasicString = "Basic";

        public BasicAuthenticationMiddleware(OwinMiddleware next, IAuthService authService) : base(next)
        {
            this._authService = authService;
        }

        public override async Task Invoke(IOwinContext context)
        {
            var authHeader = context.Request.Headers.Get("Authorization");
            if (authHeader == null || authHeader == "" || authHeader.StartsWith(BasicString))
            {
                context.Response.Headers.Set("WWW-Authenticate", BasicString);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            var authToken = authHeader.Substring((BasicString + " ").Length);

            var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                Convert.FromBase64String(authToken));

            var credentials = decodeauthToken.Split(':');
            if (credentials.Length != 2)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            var user = await this._authService.GetUserByCredentialsAsync(credentials[0], credentials[1]);
            if (user != null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("Id", user.Id.ToString())
                };
                var identity = new ClaimsIdentity(claims, BasicString);
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