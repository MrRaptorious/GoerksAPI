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
    public class Measurement : PersistentObject
    {
        public Measurement(ObjectSpace os) : base(os) { }

        private Measurement() : base(null)
        { }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), value, ref date);
        }

        private float weight;
        public float Weight
        {
            get => weight;
            set => SetPropertyValue(nameof(Weight), value, ref weight);
        }

        private float height;
        public float Height
        {
            get => height;
            set => SetPropertyValue(nameof(Height), value, ref height);
        }

        private User owner;
        [Association("User-Measurements")]
        [Persistent("UserID")]
        [JsonIgnore]
        public User Owner
        {
            get => owner;
            set => SetPropertyValue(nameof(Owner), value, ref owner);
        }
    }
}
