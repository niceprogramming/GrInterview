using CliFx;
using GrInterview.Console.Commands;

return await new CliApplicationBuilder()
    .AddCommand<ParseCommand>()
    .Build()
    .RunAsync();
