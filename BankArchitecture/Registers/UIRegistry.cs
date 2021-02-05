using BankArchitecture.Providers;
using BankArchitecture.Runners;
using Microsoft.Extensions.DependencyInjection;
using BankArchitecture.Di;
using Microsoft.Extensions.Hosting;
using BankArchitecture.Notifiers;

namespace BankArchitecture.Registers
{
    public static class UIRegistry
    {
        public static void Build(this IServiceCollection services)
        {
            services.AddHostedService<Startup>();
            services.Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = true);

            services.AddSingleton<IMainProvider, MainProvider>();
            services.AddSingleton<IConsoleProvider, ConsoleProvider>();
            services.AddSingleton<IBankProvider, BankProvider>();
            services.AddSingleton<INotifier, ConsoleNotifier>();
            services.AddSingleton<IRunner, ConsoleRunner>();
            services.BuildIoC();
       }
    }
}
