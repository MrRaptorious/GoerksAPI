using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    public abstract class Activity
    {
        public ActivityCatalog ActivityType { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Done { get; set; }
        public int SortIndex { get; set; }
    }
}
