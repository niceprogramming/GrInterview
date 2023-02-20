using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrInterview.Common.Interfaces;
using GrInterview.Common.Models;

namespace GrInterview.Common.Parsers
{
    public class UserParser : IParser<User>
    {
        private readonly string[] _delimiters;

        public UserParser(string[] delimiters)
        {
            _delimiters = delimiters;
        }
        public async Task<IEnumerable<User>> Parse(TextReader reader)
        {
            var hasReadHeader = false;
            var delimiter = string.Empty;
            var records = new List<User>();
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (!hasReadHeader)
                {
                    delimiter = line.FindDelimiter(_delimiters);
                    var headerCount = line.Split(delimiter).Length;
                    if (headerCount != 5)
                    {
                        throw new InvalidDataException($"Expected 5 headers and found {headerCount}");
                    }
                    hasReadHeader = true;
                    continue;
                }

                var personData = line.Split(delimiter);

                var lastName = personData[0];
                var firstName = personData[1];
                var email = personData[2];
                var color = personData[3];
                var dateOfBirth = DateTime.Parse(personData[4]);

                var person = new User(lastName, firstName, email, color, dateOfBirth);
                records.Add(person);
            }
            return records;
        }
    }
}
