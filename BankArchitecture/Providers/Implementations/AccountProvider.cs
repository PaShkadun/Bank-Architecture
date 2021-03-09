using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common.Models;
using BankArchitecture.Providers.Interfaces;
namespace BankArchitecture.Providers.Implementations
{
    public class AccountProvider : IAccountProvider
    {
        private readonly IAccountService accountService;
        private readonly IConsoleProvider consoleProvider;

        public AccountProvider(IAccountService accountService, IConsoleProvider consoleProvider)
        {
            this.accountService = accountService;
            this.consoleProvider = consoleProvider;
        }

        public object ChooseRecipientCard(Account account)
        {
            if (account.Cards.Count == 0)
            {
                return null;
            }

            var cardInfo = string.Empty;

            if (account as CreditAccount != null)
            {
                cardInfo = accountService.GetCardsInfo(account);
            }
            else
            {
                cardInfo = accountService.GetCardsInfo(account);
            }

            int choose = consoleProvider.InputValue(cardInfo);

            if (choose < 0 || choose >= account.Cards.Count)
            {
                return null;
            }
            else
            {
                return account.Cards[choose];
            }
        }
    }
}
