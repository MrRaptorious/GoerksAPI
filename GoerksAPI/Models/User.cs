using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    public class User : BaseObject
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public ICollection<Measurement> Measurements { get; set; }
        public ICollection<Workout> Workouts { get; set; }
    }
}
