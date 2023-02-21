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
        [Theory]
        [InlineData("./TestFiles/SingleLineCsv.txt")]
        [InlineData("./TestFiles/SingleLinePipe.txt")]
        [InlineData("./TestFiles/SingleLineSpaces.txt")]
        [InlineData("./TestFiles/SingleLineCsvNoHeader.txt", false)]
        [InlineData("./TestFiles/SingleLinePipeNoHeader.txt", false)]
        [InlineData("./TestFiles/SingleLineSpacesNoHeader.txt", false)]
        public async Task Parser_returns_data_given_a_file_with_delimiter_and_at_least_one_non_header_row(string filePath, bool hasHeader = true)
        {
            using var fileStream = File.OpenText(filePath);

            var expected = new User("User", "Test", "test.user@gmail.com", "brown", new DateTime(1994, 7, 26));
            var parser = new UserParser(new[] { ",", "|", " " });

            var result = await parser.Parse(fileStream, hasHeader);

            Assert.Equal(expected, result.FirstOrDefault());
        }

        [Fact]
        public async Task Parser_skips_rows_with_no_data_and_returns_data_for_populated_rows()
        {
            var filePath = "./TestFiles/BlankLineCsv.txt";
            using var fileStream = File.OpenText(filePath);

            var expected = new User("User", "Test", "test.user@gmail.com", "brown", new DateTime(1994, 7, 26));
            var parser = new UserParser(new[] { "," });

            var result = await parser.Parse(fileStream);

            Assert.Equal(expected, result.FirstOrDefault());
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
