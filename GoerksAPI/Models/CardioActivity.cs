using AnanasCore;
using AnanasCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    [Persistent]
    public class CardioActivity : Activity
    {
        public CardioActivity(ObjectSpace os) : base(os)
        {
        }

        private float distance;
        public float Distance
        {
            get => distance;
            set => SetPropertyValue(nameof(Distance), value, ref distance);
        }

        private Workout activityWorkout;
        [Association("Workout-CardioActivity")]
        [Persistent("WorkoutID")]
        [JsonIgnore]
        public Workout ActivityWorkout
        {
            get => activityWorkout;
            set => SetPropertyValue(nameof(ActivityWorkout), value, ref activityWorkout);
        }

    }
}
