using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.IO;

namespace AventOfCode.Extensions
{
    public static  class ServiceCollectionExtension
    {
        public static void ConfigureConfiguration<T>(this IServiceCollection services, string file, string section) where T : class
        {
            //Load configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName))
                .AddJsonFile(file, optional: true, reloadOnChange: true)
                .Build();

            //Add configuration to app https://pioneercode.com/post/dependency-injection-logging-and-configuration-in-a-dot-net-core-console-app 
            services.AddOptions();
            services.Configure<T>(config.GetSection(section));
        }
    }
}
