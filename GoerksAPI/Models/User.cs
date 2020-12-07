using AnanasCore;
using AnanasCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoerksAPI.Models
{
    [Persistent]
    public class User : PersistentObject
    {
        public User(ObjectSpace os) : base(os)
        {
        }

        private string userName;
        public string UserName
        {
            get => userName;
            set => SetPropertyValue(nameof(UserName), value, ref userName);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), value, ref email);
        }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => SetPropertyValue(nameof(DateOfBirth), value, ref dateOfBirth);
        }

        private string gender;
        public string Gender
        {
            get => gender;
            set => SetPropertyValue(nameof(Gender), value, ref gender);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetPropertyValue(nameof(Password), value, ref password);
        }

        private AnanasList<Measurement> measurements;
        [Association("User-Measurements")]
        public AnanasList<Measurement> Measurements { get => GetList(nameof(Measurements), ref measurements); }

        private AnanasList<Workout> workouts;
        [Association("User-Workouts")]
        public AnanasList<Workout> Workouts { get => GetList(nameof(Workouts),ref workouts); }

        internal bool CheckPassword(string password)
        {
            // TODO for debug
            return password == password;
        }
    }
}
