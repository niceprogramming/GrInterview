using GrInterview.Common.Parsers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GrInterview.Common.Models;

namespace GrInterview.Tests
{
    
    public class UserParserTests
    {
        [Fact]
        public async Task Parser_returns_data_given_a_file_with_at_least_one_non_header_row()
        {
            var filePath = "./TestFiles/SingleLineCsv.txt";
            using var fileStream = File.OpenText(filePath);

            var expected = new User("User", "Test", "test.user@gmail.com", "brown", new DateTime(1994, 7, 26));
            var parser = new UserParser(new []{","});

            var result = await parser.Parse(fileStream);

            Assert.Equivalent(expected, result.FirstOrDefault());
        }

        [Fact]
        public async Task Parser_skips_rows_with_no_data_and_returns_data_for_populated_rows()
        {
            var filePath = "./TestFiles/BlankLineCsv.txt";
            using var fileStream = File.OpenText(filePath);

            var expected = new User("User", "Test", "test.user@gmail.com", "brown", new DateTime(1994, 7, 26));
            var parser = new UserParser(new[] { "," });

            var result = await parser.Parse(fileStream);

            Assert.Equivalent(expected, result.FirstOrDefault());
        }

        [Theory]
        [InlineData("./TestFiles/FourHeaderCsv.txt")]
        [InlineData("./TestFiles/SixHeaderCsv.txt")]
        public async Task Parser_throws_exception_if_file_does_not_have_five_columns(string filePath)
        {
            using var fileStream = File.OpenText(filePath);

            var parser = new UserParser(new[] { "," });

            await Assert.ThrowsAnyAsync<InvalidDataException>(() => parser.Parse(fileStream));
        }
    }
}
