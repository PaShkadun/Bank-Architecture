using BankArchitecture.Notifiers;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace BankArchitecture.Runners
{
    public class Startup : BackgroundService
    {
        private readonly INotifier notifier;
        private readonly IRunner runner;

        public Startup(INotifier notifier, IRunner runner)
        {
            this.notifier = notifier;
            this.runner = runner;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            notifier.Notify("Debug: Start program from ConsoleRunner.");
            await runner.RunAsync(stoppingToken);
            notifier.Notify("Program End.");
        }

        public void Close(CancellationToken stoppingToken)
        {
            StopAsync(stoppingToken);
        }
    }
}
