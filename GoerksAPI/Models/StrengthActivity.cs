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
    public class StrengthActivity : Activity
    {
        public StrengthActivity(ObjectSpace os) : base(os) { }

        private int reps;
        public int Reps
        {
            get => reps;
            set => SetPropertyValue(nameof(Reps), value, ref reps);
        }

        private StrengthActivitySet activitySet;
        [Association("StrenghtActivitySet-StrenghtActivity")]
        [Persistent("StrenghtActivitySetID")]
        [JsonIgnore]
        public StrengthActivitySet ActivitySet
        {
            get => activitySet;
            set => SetPropertyValue(nameof(ActivitySet), value, ref activitySet);
        }
    }
}
