using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrInterview.Common.Models
{
    public class User
    {
        public string LastName { get; }
        public string FirstName { get; }
        public string Email { get; }
        public string FavoriteColor { get; }
        public string DateOfBirth { get; }

        public User(string lastName, string firstName, string email, string favoriteColor, string dateOfBirth)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            FavoriteColor = favoriteColor;
            DateOfBirth = dateOfBirth;
        }
    }
}
