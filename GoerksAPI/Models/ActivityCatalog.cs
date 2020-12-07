using AnanasCore;
using AnanasCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    [Persistent]
    public class ActivityCatalog : PersistentObject
    {
        public ActivityCatalog(ObjectSpace os) : base(os) { }

        private string name;
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), value, ref name);
        }

        private string activityType;
        public string ActivityType
        {
            get => activityType;
            set => SetPropertyValue(nameof(ActivityType), value, ref activityType);
        }
    }
}
