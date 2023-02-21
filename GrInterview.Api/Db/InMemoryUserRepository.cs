using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrInterview.Common.Models;

namespace GrInterview.Api.Db
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }
    }
}
