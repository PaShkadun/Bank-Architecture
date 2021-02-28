using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.Implementations
{
    public class AccountService : IAccountService
    {
        public string GetCardsInfo(Account account)
        {
            if (account.Cards.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                var cardsInfo = string.Empty;
                var count = 0;

                foreach (var card in account.Cards)
                {
                    cardsInfo += $"{count++}. {card.Id} {card.Balance}\n";
                }

                return cardsInfo;
            }
        }

        public bool HasMoney(Account account, int money)
        {
            if (account.Balance >= money && money > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
