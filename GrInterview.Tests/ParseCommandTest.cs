using GrInterview.Common.Models;
using GrInterview.Common.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using GrInterview.Console.Commands;

namespace GrInterview.Tests
{
    public class ParseCommandTest
    {
        [Fact]
        public async Task Command_executes_successfully_given_an_existing_file_and_outputs_sorted_data()
        {
            using var console = new FakeInMemoryConsole();
            var parser = new UserParser(new[] { ",", "|", " " });
            var command = new ParseCommand(parser)
            {
                Files = new List<FileInfo>
                {
                    new("./TestFiles/FullTest.txt")
                }
            };

            var expected = TestData.SuccessfulTestOutput;
            await command.ExecuteAsync(console);

            var result = console.ReadOutputString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Command_throws_exception_given_a_nonexistent_file()
        {
            using var console = new FakeInMemoryConsole();
            var parser = new UserParser(new[] { ",", "|", " " });
            var command = new ParseCommand(parser)
            {
                Files = new List<FileInfo>
                {
                    new("thisfiledoesntexist.txt")
                }
            };
           
            await Assert.ThrowsAnyAsync<CommandException>( async () => await command.ExecuteAsync(console));

        }
    }

}

