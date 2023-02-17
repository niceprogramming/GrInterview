using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrInterview.Common.Interfaces
{
    public interface IParser<T>
    {
        Task<T> Parse(StreamReader reader);
    }
}
