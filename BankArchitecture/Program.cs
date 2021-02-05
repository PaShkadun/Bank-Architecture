using BankArchitecture.Providers;
using BankArchitecture.Registers;
using BankArchitecture.Runners;
using Microsoft.Extensions.Hosting;

namespace BankArchitecture
{
    public class Program
    {
        private static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => 
                    services.Build());
        }
    }
}
