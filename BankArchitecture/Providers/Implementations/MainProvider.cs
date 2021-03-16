using BankArchitecture.Providers.Interfaces;
using BankArchitecture.Resources;

namespace BankArchitecture.Providers.Implementations
{
    public class MainProvider : IMainProvider
    {
        private readonly IBankProvider bankProvider;
        private readonly IConsoleProvider consoleProvider;

        public MainProvider(IBankProvider bankProvider, IConsoleProvider consoleProvider)
        {
            this.bankProvider = bankProvider;
            this.consoleProvider = consoleProvider;
        }

        public void MainActions()
        {
            while (true)
            {
                int choose = consoleProvider.InputValue(StringConstants.MainActions);

                bankProvider.Start(choose);
            }
        }
    }
}
