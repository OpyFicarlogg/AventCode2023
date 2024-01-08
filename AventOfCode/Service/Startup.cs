using AventOfCode.Dto;
using AventOfCode.Service.Interfaces;
using Microsoft.Extensions.Options;

namespace AventOfCode.Service
{
    public class Startup : IStartup
    {
        private readonly IDay day;

        public Startup(IDay day)
        {
            this.day = day;
        }

        public void Run()
        {
            System.Console.WriteLine($"StartupLoaded");
            day.Run();
        }
    }
}
