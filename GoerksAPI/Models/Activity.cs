using AnanasCore;
using AnanasCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    [Persistent]
    public abstract class Activity : PersistentObject
    {
        protected Activity(ObjectSpace os) : base(os) { }

        private ActivityCatalog activityCatalog;
        [Persistent("ActivityTypeID")]
        public ActivityCatalog ActivityType
        {
            get => activityCatalog;
            set => SetPropertyValue(nameof(ActivityType), value, ref activityCatalog);
        }

        private DateTime start;
        public DateTime Start
        {
            get => start;
            set => SetPropertyValue(nameof(Start), value, ref start);
        }

        private DateTime end;
        public DateTime End
        {
            get => end;
            set => SetPropertyValue(nameof(End), value, ref end);
        }

        [NonPersistent]
        public bool Done
        {
            get => End > DateTime.MinValue && start > DateTime.MinValue;
        }

        private int sortIndex;
        public int SortIndex
        {
            get => sortIndex;
            set => SetPropertyValue(nameof(SortIndex), value, ref sortIndex);

        }

        [NonPersistent]
        public TimeSpan Duration
        {
            get => End - Start;
        }
    }
}
