using Microsoft.Extensions.Hosting;
using BankArchitecture.Providers.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace BankArchitecture.Runners
{
    public class Startup : BackgroundService
    {
        private readonly IMainProvider mainProvider;

        public Startup(IMainProvider mainProvider)
        {
            this.mainProvider = mainProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(() => mainProvider.MainActions());
        }

        private void Start()
        {
            mainProvider.MainActions();
        }
    }
}


