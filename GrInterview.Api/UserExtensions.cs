using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrInterview.Common.Models;

namespace GrInterview.Api
{
    public static class UserExtensions
    {
        public static IEnumerable<User> SortByColor(this IEnumerable<User> users) =>
            users.OrderBy(x => x.FavoriteColor.ToLower());

        public static IEnumerable<User> SortByBirthdate(this IEnumerable<User> users) =>
            users.OrderBy(x => x.DateOfBirth);

        public static IEnumerable<User> SortByLastName(this IEnumerable<User> users) =>
            users.OrderBy(x => x.LastName.ToLower());
    }
}
