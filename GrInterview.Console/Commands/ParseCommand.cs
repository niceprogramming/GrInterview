using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using GrInterview.Common.Interfaces;
using GrInterview.Common.Models;

namespace GrInterview.Console.Commands
{
    [Command]
    public class ParseCommand : ICommand
    {
        private readonly IParser<User> _parser;

        [CommandParameter(0)] 
        public IReadOnlyList<FileInfo> Files { get; init; } = new List<FileInfo>();

        public ParseCommand(IParser<User> parser)
        {
            _parser = parser;
        }

        public ValueTask ExecuteAsync(IConsole console)
        {
            console.Output.WriteLine("Parsed");

            return default;
        }
    }
}
