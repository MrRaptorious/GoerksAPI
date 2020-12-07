using GoerksAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : GoerksBaseController
    {
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<SlimmedDownWorkout>> GetWorkouts([FromQuery(Name = "from")] string from, [FromQuery(Name = "to")] string to)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            if (!long.TryParse(from, out long dfrom)
            & !long.TryParse(to, out long dto))
                return BadRequest();

            DateTime datefrom = DateTimeFromUnixTimeStamp(dfrom);
            DateTime dateTo = DateTimeFromUnixTimeStamp(dto);

            return user.Workouts?.Where(x => x.CreationDate > datefrom && x.CreationDate < dateTo).Select(x =>
            {
             return new SlimmedDownWorkout()
                {
                    Name = x.Name,
                    Date = x.Date,
                    ID = x.ID
                };
            }).ToList();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddWorkout([FromBody] Workout requestWorkout)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            Workout work = new Workout(os);
            work.Name = requestWorkout.Name;
            work.Date = requestWorkout.Date;

            user.Workouts.Add(work);
            os.CommitChanges();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public ActionResult<Measurement> DeleteWorkout([FromQuery(Name = "id")] string id)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            if (id is null || !Guid.TryParse(id, out Guid guid))
                return BadRequest();

            var workout = user.Workouts?.FirstOrDefault(x => x.ID.Equals(guid));

            if (workout is null)
                return BadRequest();

            workout.Delete();

            os.CommitChanges();

            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Measurement>> UpdateWorkout([FromBody] Workout workoutRequest)
        {
            var user = GetUserFromToken();

            if (user == null)
                return BadRequest();

            var meas = user.Measurements.FirstOrDefault(x => x.ID == workoutRequest.ID);

            if (meas is null)
                return BadRequest();

            meas.SetPropertyValue(nameof(Workout.Date), workoutRequest.Date);
            meas.SetPropertyValue(nameof(Workout.Name), workoutRequest.Name);

            os.CommitChanges();

            return Ok();
        }
    }

    public class SlimmedDownWorkout
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
