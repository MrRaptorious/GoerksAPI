using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GoerksAPI.Models;
using AnanasCore;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using AnanasCore.Criteria;

namespace GoerksAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : GoerksBaseController
    {
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> PutUser()
        {
            User user = GetUserFromToken();

            if (user is null)
                return ValidationProblem();

            string body = "";

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                body = await reader.ReadToEndAsync();

            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);

            // update user object
            foreach (var entry in dict)
            {
                switch (entry.Key)
                {
                    case "username":
                        user.SetPropertyValue(nameof(Models.User.UserName), entry.Value);
                        break;
                    case "gender":
                        user.SetPropertyValue(nameof(Models.User.Gender), entry.Value);
                        break;
                    case "dateOfBirth":
                        user.SetPropertyValue(nameof(Models.User.DateOfBirth), os.FieldTypeParser.CastValue<DateTime>(entry.Value));
                        break;
                }
            }

            os.CommitChanges();

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public ActionResult DeleteUser()
        {
            var user = GetUserFromToken();

            if (user != null)
            {
                user.Delete();
                os.CommitChanges();
                return new OkResult();
            }

            return new NotFoundResult();
        }
    }
}
