using GoerksAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Controllers
{
    [Route("api/Workouts/detail")]
    [ApiController]
    public class WorkoutDetailsController : GoerksBaseController
    {
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Workout>> GetWorkouts([FromQuery(Name = "id")] string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
                return BadRequest();

            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            return user.Workouts?.Where(x => x.ID.Equals(guid)).ToList();
        }

    }
}
