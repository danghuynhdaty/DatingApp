using System;
using System.Collections.Generic;

namespace DatingApp.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string KnowAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Introduction { get; set; }

        public string Interests { get; set; }

        public string Country { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public static User CreateNewUser(string username)
        {
            return new User
            {
                Username = username
            };
        }



    }
}