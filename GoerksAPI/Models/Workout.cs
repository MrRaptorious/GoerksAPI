using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    public class Workout
    {
        public List<StrenghtActivitySet> StrenghtActivitySets { get; set; }
        public List<CardioActivity> CardioActivities { get; set; }
    }
}
