using BankArchitecture.Providers;
using BankArchitecture.Routes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankArchitecture.Runners
{
    public class ConsoleRunner : IRunner
    {
        private readonly IMainProvider provider;

        public ConsoleRunner(IMainProvider provider)
        {
            this.provider = provider;
        }

        public Task RunAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    provider.MainActions();
                }
            }
            );
        }
    }
}
