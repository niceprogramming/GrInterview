using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace GrInterview.Console.Commands
{
    [Command]
    public class ParseCommand : ICommand
    {
        [CommandParameter(0)] 
        public IReadOnlyList<FileInfo> Files { get; init; } = new List<FileInfo>();

        public ValueTask ExecuteAsync(IConsole console)
        {
            console.Output.WriteLine("Parsed");

            return default;
        }
    }
}
