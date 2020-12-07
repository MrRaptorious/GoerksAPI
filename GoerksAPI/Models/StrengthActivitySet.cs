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
    public class StrengthActivitySet : PersistentObject
    {
        public StrengthActivitySet(ObjectSpace os) : base(os) { }

        private int sortIndex;
        public int SortIndex
        {
            get => sortIndex;
            set => SetPropertyValue(nameof(SortIndex), value, ref sortIndex);
        }

        private AnanasList<StrengthActivity> strenghtActivities;
        [Association("StrenghtActivitySet-StrenghtActivity")]
        public AnanasList<StrengthActivity> StrenghtActivities { get => GetList(nameof(StrenghtActivities), ref strenghtActivities); }

        private Workout activityWorkout;
        [Association("Workout-StrenghtActivitySet")]
        [Persistent("WorkoutID")]
        [JsonIgnore]
        public Workout ActivityWorkout
        {
            get => activityWorkout;
            set => SetPropertyValue(nameof(ActivityWorkout), value, ref activityWorkout);
        }
    }
}
