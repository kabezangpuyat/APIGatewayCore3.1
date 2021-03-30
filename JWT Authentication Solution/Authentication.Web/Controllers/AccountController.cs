using Auth.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService ?? throw new ArgumentException(nameof(authenticationService));
        }
        [HttpGet("get-token/{email}"),AllowAnonymous]
        public IActionResult GetToken(string email)
        {
            var token = _authenticationService.Authenticate(email, this.IpAddress());

            return Ok(token);
        }

        #region Helper Method(s)
        private void SetTokenCookie(string token, string jwttoken, DateTime expiryDate)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expiryDate
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
            Response.Cookies.Append("jwttoken", jwttoken, cookieOptions);
            Response.Cookies.Append("tokenexpiry", expiryDate.ToString(), cookieOptions);
        }
        private void RemoveSession()
        {
            Response.Cookies.Delete("refreshToken", new CookieOptions() { Secure = true });
            Response.Cookies.Delete("jwttoken", new CookieOptions() { Secure = true });
            Response.Cookies.Delete("tokenexpiry", new CookieOptions() { Secure = true });
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        #endregion
    }
}
