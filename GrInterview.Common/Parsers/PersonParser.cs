using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrInterview.Common.Interfaces;
using GrInterview.Common.Models;

namespace GrInterview.Common.Parsers
{
    public class UserParser : IParser<User>
    {
        public Task<User> Parse(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
