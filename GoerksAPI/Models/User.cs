using AnanasCore;
using AnanasCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            //set => SetPropertyValue(nameof(Password), value, ref password);
            private set => password = value;
        }

        internal bool UpdatePassword(string oldPassword, string newPassword)
        {
            if (CheckPassword(oldPassword))
            {
                SetPropertyValue(nameof(Password), EncryptPassword(newPassword), ref password);
                return true;
            }

            return false;
        }

        private AnanasList<Measurement> measurements;
        [Association("User-Measurements")]
        public AnanasList<Measurement> Measurements { get => GetList(nameof(Measurements), ref measurements); }

        private AnanasList<Workout> workouts;
        [Association("User-Workouts")]
        public AnanasList<Workout> Workouts { get => GetList(nameof(Workouts), ref workouts); }

        internal bool CheckPassword(string password)
        {
            string savedPasswordHash = Password;

            // always allow to set password if none not set
            if (string.IsNullOrEmpty(savedPasswordHash))
                return true;

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);


            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }

        private string EncryptPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);

            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
