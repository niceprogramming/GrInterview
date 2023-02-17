using System.Xml.Serialization;
using CliFx;
using GrInterview.Common.Interfaces;
using GrInterview.Common.Models;
using GrInterview.Common.Parsers;
using GrInterview.Console.Commands;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IParser<User>, UserParser>();

services.AddTransient<ParseCommand>();
var serviceProvider = services.BuildServiceProvider();

return await new CliApplicationBuilder()
    .AddCommandsFromThisAssembly()
    .UseTypeActivator(serviceProvider)
    .Build()
    .RunAsync();
