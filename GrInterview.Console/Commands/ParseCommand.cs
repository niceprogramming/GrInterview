using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using ConsoleTables;
using GrInterview.Common.Interfaces;
using GrInterview.Common.Models;

namespace GrInterview.Console.Commands;

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

    public async ValueTask ExecuteAsync(IConsole console)
    {
        var records = new List<User>();
        foreach (var file in Files)
        {
            if (!file.Exists)
            {
                throw new CommandException($"{file.Name} doesn't exist in directory: {file.DirectoryName}");
            }

            using var reader = File.OpenText(file.FullName);

            var result = await _parser.Parse(reader);
            records.AddRange(result);
        }

        var headers = typeof(User)
            .GetProperties()
            .Select(x => x.Name);

        var table = new ConsoleTable(new ConsoleTableOptions
        {
            OutputTo = console.Output,
            EnableCount = false,
            Columns = headers
        });

        var firstOutput = records
            .OrderBy(x => x.FavoriteColor.ToLower())
            .ThenBy(x => x.LastName);

        WriteToConsole(table, firstOutput);

        var secondOutput = records
            .OrderBy(x => x.DateOfBirth);

        WriteToConsole(table, secondOutput);

        var thirdOutput = records
            .OrderByDescending(x => x.LastName.ToLower());

        WriteToConsole(table, thirdOutput);
    }

    private void WriteToConsole(ConsoleTable table, IEnumerable<User> records)
    {
        foreach (var record in records)
        {
            table.AddRow(record.LastName, record.FirstName, record.Email, record.FavoriteColor,
                record.DateOfBirth.ToString("M/d/yyyy"));
        }

        table.Write();
        table.Rows.Clear();
    }
}