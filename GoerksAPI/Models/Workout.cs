using AnanasCore;
using AnanasCore.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    [Persistent]
    public class Workout : PersistentObject
    {
        //private Workout() : base(null){}
        public Workout(ObjectSpace os) : base(os) { }

        private AnanasList<StrengthActivitySet> strenghtActivitySets;
        [Association("Workout-StrenghtActivitySet")]
        public AnanasList<StrengthActivitySet> StrenghtActivitySets { get => GetList(nameof(StrenghtActivitySets), ref strenghtActivitySets); }

        private AnanasList<CardioActivity> cardioActivities;
        [Association("Workout-CardioActivity")]
        public AnanasList<CardioActivity> CardioActivities { get => GetList<CardioActivity>(nameof(CardioActivities), ref cardioActivities); }

        /// <summary>
        /// Do not call its not very performant
        /// </summary>
        [JsonIgnore]
        [NonPersistent]
        public List<Activity> AllActivities
        {
            get => CardioActivities.ToList<Activity>()
            .Union(StrenghtActivitySets
                .Select(x => x.StrenghtActivities.ToList())
                .SelectMany(x => x)
                .ToList<Activity>())
            .ToList();
        }

        private User owner;
        [Association("User-Workouts")]
        [Persistent("UserID")]
        public User Owner
        {
            get => owner;
            set => SetPropertyValue(nameof(Owner), value, ref owner);
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), value, ref date);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), value, ref name);
        }
    }
}
