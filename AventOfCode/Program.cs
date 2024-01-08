using AventOfCode.Dto;
using AventOfCode.Service;
using AventOfCode.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using AventOfCode.Extensions;


Console.WriteLine("Chargement de l'application");

ServiceCollection services = new();

services.ConfigureConfiguration<Params>("appSettings.json", "Params");

services.AddSingleton<IStartup, Startup>()
    .AddSingleton<IDay,Day4>()
    .BuildServiceProvider()
    .GetService<IStartup>()
    .Run();

