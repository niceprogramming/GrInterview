using CliFx;
using GrInterview.Console.Commands;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<ParseCommand>();
var serviceProvider = services.BuildServiceProvider();

return await new CliApplicationBuilder()
    .AddCommandsFromThisAssembly()
    .UseTypeActivator(serviceProvider)
    .Build()
    .RunAsync();
