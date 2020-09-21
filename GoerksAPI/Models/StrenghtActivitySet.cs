using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    public class StrenghtActivitySet : BaseObject
    {
        public int SortIndex { get; set; }
        public ICollection<StrenghtActivity> StrenghtActivities { get; set; }
    }
}
