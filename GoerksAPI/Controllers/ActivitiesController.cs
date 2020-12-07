using AnanasCore.Criteria;
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
    public class ActivitiesController : GoerksBaseController
    {
        #region cardio
        [HttpPost("cardio")]
        [Authorize]
        public IActionResult AddCardioActivities([FromBody] CardioActivityRequest cardioActivityRequest)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            var workout = os.GetObject<Workout>(cardioActivityRequest.WorkoutID, true);

            if (workout is null || !user.ID.Equals(workout.Owner))
                return BadRequest();

            CardioActivity ca = new CardioActivity(os);
            ca.Start = cardioActivityRequest.StartDate;
            ca.End = cardioActivityRequest.EndDate;
            ca.ActivityType = os.GetObjects<ActivityCatalog>(new WhereClause(nameof(ActivityCatalog.Name), cardioActivityRequest.Name, ComparisonOperator.Equal)).FirstOrDefault();
            ca.Distance = cardioActivityRequest.Distance;

            workout.CardioActivities.Add(ca);

            os.CommitChanges();
            return Ok();
        }

        [HttpPut("cardio")]
        [Authorize]
        public IActionResult UpdateCardioActivities([FromBody] CardioActivityRequest requestCardioActivity)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            var activity = os.GetObject<CardioActivity>(requestCardioActivity.ID, true);

            if (activity is null || !user.ID.Equals(activity?.ActivityWorkout?.Owner))
                return BadRequest();

            activity.Start = requestCardioActivity.StartDate;
            activity.End = requestCardioActivity.EndDate;
            activity.ActivityType = os.GetObjects<ActivityCatalog>(new WhereClause(nameof(ActivityCatalog.Name), requestCardioActivity.Name, ComparisonOperator.Equal)).FirstOrDefault();
            activity.Distance = requestCardioActivity.Distance;

            os.CommitChanges();
            return Ok();
        }

        [HttpDelete("cardio")]
        [Authorize]
        public ActionResult<Measurement> DeleteCardioActivity([FromQuery(Name = "id")] string id)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            if (id is null || !Guid.TryParse(id, out Guid guid))
                return BadRequest();

            var activity = os.GetObject<CardioActivity>(guid, true);

            if (activity is null)
                return BadRequest();

            if (!user.ID.Equals(activity?.ActivityWorkout?.Owner?.ID))
                return BadRequest();

            activity.Delete();

            os.CommitChanges();

            return Ok();
        }
        #endregion

        #region strenght
        [HttpPost("strenght")]
        [Authorize]
        public IActionResult AddStrengthActivity([FromBody] StrengthActivityRequest strengthActivityRequest)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            var set = os.GetObject<StrengthActivitySet>(strengthActivityRequest.StrenghtActivitySetID, true);

            if (set is null || !user.ID.Equals(set?.ActivityWorkout?.Owner))
                return BadRequest();

            StrengthActivity strengthActivity = new StrengthActivity(os);
            strengthActivity.Start = strengthActivityRequest.StartDate;
            strengthActivity.End = strengthActivityRequest.EndDate;
            strengthActivity.ActivityType = os.GetObjects<ActivityCatalog>(new WhereClause(nameof(ActivityCatalog.Name), strengthActivityRequest.Name, ComparisonOperator.Equal)).FirstOrDefault();
            strengthActivity.SortIndex = strengthActivityRequest.SortIndex;
            strengthActivity.Reps = strengthActivityRequest.Reps;

            set.StrenghtActivities.Add(strengthActivity);

            os.CommitChanges();
            return Ok();
        }

        [HttpPost("strenght")]
        [Authorize]
        public IActionResult AddStrengthActivitySet([FromBody] StrengthActivitySetRequest strengthActivitySetRequest)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            var workout = os.GetObject<Workout>(strengthActivitySetRequest.WorkoutID, true);

            if (workout is null || !user.ID.Equals(workout.Owner))
                return BadRequest();

            StrengthActivitySet set = new StrengthActivitySet(os);
            set.SortIndex = strengthActivitySetRequest.SortIndex;

            workout.StrenghtActivitySets.Add(set);

            os.CommitChanges();
            return Ok();
        }

        [HttpPut("strenght")]
        [Authorize]
        public IActionResult UpdateStrengthActivity([FromBody] StrengthActivityRequest strengthActivityRequest)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            var activity = os.GetObject<StrengthActivity>(strengthActivityRequest.ID, true);

            if (activity is null || !user.ID.Equals(activity?.ActivitySet?.ActivityWorkout?.Owner))
                return BadRequest();

            activity.Start = strengthActivityRequest.StartDate;
            activity.End = strengthActivityRequest.EndDate;
            activity.ActivityType = os.GetObjects<ActivityCatalog>(new WhereClause(nameof(ActivityCatalog.Name), strengthActivityRequest.Name, ComparisonOperator.Equal)).FirstOrDefault();
            activity.Reps = strengthActivityRequest.Reps;

            os.CommitChanges();
            return Ok();
        }

        [HttpDelete("strenght")]
        [Authorize]
        public ActionResult<StrengthActivity> DeleteStrenghtActivity([FromQuery(Name = "id")] string id)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            if (id is null || !Guid.TryParse(id, out Guid guid))
                return BadRequest();

            var activity = os.GetObject<StrengthActivity>(guid, true);

            if (activity is null)
                return BadRequest();

            if (!user.ID.Equals(activity?.ActivitySet?.ActivityWorkout?.Owner?.ID))
                return BadRequest();

            activity.Delete();

            os.CommitChanges();

            return Ok();
        }

        [HttpDelete("strenght")]
        [Authorize]
        public ActionResult<StrengthActivitySet> DeleteStrenghtActivitySet([FromQuery(Name = "id")] string id)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            if (id is null || !Guid.TryParse(id, out Guid guid))
                return BadRequest();

            var activitySet = os.GetObject<StrengthActivitySet>(guid, true);

            if (activitySet is null)
                return BadRequest();

            if (!user.ID.Equals(activitySet?.ActivityWorkout?.Owner?.ID))
                return BadRequest();

            activitySet.Delete();

            os.CommitChanges();

            return Ok();
        }
        #endregion

        [HttpPost("reorder")]
        [Authorize]
        public ActionResult ReorderActivities([FromBody] List<ReorderRequest> reoderRequests)
        {
            var user = GetUserFromToken();

            if (user is null)
                return BadRequest();

            foreach (var item in reoderRequests)
            {
                Activity activity = os.GetObject<StrengthActivity>(item.ID, true);

                if (activity != null)
                    activity = os.GetObject<CardioActivity>(item.ID, true);

                activity.SortIndex = item.ActivitySortIndex;
            }

            os.CommitChanges();

            return Ok();
        }
    }

    public class ReorderRequest
    {
        public Guid ID { get; set; }
        public int ActivitySortIndex { get; set; }
    }

    public class CardioActivityRequest
    {
        public Guid ID { get; set; }
        public Guid WorkoutID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public float Distance { get; set; }
    }

    public class StrengthActivitySetRequest
    {
        public int SortIndex { get; set; }
        public Guid WorkoutID { get; set; }
    }

    public class StrengthActivityRequest
    {
        public Guid ID { get; set; }
        public int SortIndex { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public float Distance { get; set; }
        public int Reps { get; set; }

        public Guid StrenghtActivitySetID { get; set; }
    }
}
