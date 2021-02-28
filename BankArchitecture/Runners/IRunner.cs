using System.Threading;
using System.Threading.Tasks;

namespace BankArchitecture.Runners
{
    public interface IRunner
    {
        Task RunAsync(CancellationToken stoppingToken);
    }
}
