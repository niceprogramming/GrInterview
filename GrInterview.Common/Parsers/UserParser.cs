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

        public async Task<IEnumerable<User>> Parse(TextReader reader, bool hasHeader = true)
        {
            var hasReadHeader = false;
            string? delimiter = null;
            var records = new List<User>();
            while (await reader.ReadLineAsync() is { } line)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (delimiter == null)
                {
                    delimiter = line.FindDelimiter(_delimiters);
                    var columnCount = line.Split(delimiter).Length;
                    if (columnCount != 5)
                    {
                        throw new InvalidDataException($"Expected 5 columns and found {columnCount}");
                    }
                }
               

                if (hasHeader && !hasReadHeader)
                {
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