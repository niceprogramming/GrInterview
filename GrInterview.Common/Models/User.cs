using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrInterview.Common.Models
{
    public record User(string LastName, string FirstName, string Email, string FavoriteColor, DateTime DateOfBirth);
}