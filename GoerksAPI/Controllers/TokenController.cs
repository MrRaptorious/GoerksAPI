using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AnanasCore;
using AnanasCore.Criteria;
using GoerksAPI.Models;
using GoerksAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace GoerksAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        private ObjectSpace os;


        public TokenController(IConfiguration config)
        {
            _config = config;
            os = AnanasApplication.GetApplication().DefaultSubManager.CreateObjectSpace();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult GetRandomToken([FromBody] LoginRequest login)
        {
            var user = GetUserFromEmail(login.Email);

            if (user == null || !user.CheckPassword(login.Password))
                return Unauthorized();

            var jwt = new JwtService(_config);
            var token = jwt.GenerateSecurityToken(user);

            return new JsonResult(new { token });
        }

        private User GetUserFromEmail(string email)
        {
            return os.GetObjects<User>(new WhereClause("Email", email, ComparisonOperator.Equal)).FirstOrDefault();
        }

        [HttpPost("logout")]
        public string Logout()
        {
            return default;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public ActionResult Signup([FromBody] SignUpRequest request)
        {
            if (request.Email is null || request.Password is null)
                return BadRequest("Email and password can not be null");

            var users = os.GetObjects<User>(new WhereClause(nameof(Models.User.Email), request.Email, ComparisonOperator.Equal));
            User user;

            if (users.Count == 0)
            {
                try
                {
                    user = os.CreateObject<User>();

                    user.Email = request.Email;
                    user.UserName = request.Username;
                    user.Gender = request.Gender;
                    user.UpdatePassword(null,request.Password);
                    user.DateOfBirth = UnixTimeStampToDateTime(request.DateOfBirth);

                    os.CommitChanges();
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
                return BadRequest("Email is already registerd");


            var jwt = new JwtService(_config);
            var token = jwt.GenerateSecurityToken(user);

            return new JsonResult(new { token });
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


    }

    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public long DateOfBirth { get; set; }
        public string Gender { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
