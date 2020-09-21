using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    public class Workout : BaseObject
    {
        public ICollection<StrenghtActivitySet> StrenghtActivitySets { get; set; }
        public ICollection<CardioActivity> CardioActivities { get; set; }
    }
}
