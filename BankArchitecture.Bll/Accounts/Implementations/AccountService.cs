using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common.Models;

namespace BankArchitecture.Bll.Accounts.Implementations
{
    public class AccountService : IAccountService
    {
        public void AddCard(Account account)
        {
            if (account as CreditAccount != null)
            {
                account.Cards.Add(new CreditCard());
            }
            else
            {
                account.Cards.Add(new DebitCard());
            }
        }

        public bool DeleteCard(Account account, int cardNumber)
        {
            if (cardNumber < 0 || account.Cards.Count >= cardNumber)
            {
                return false;
            }
            else if (account as CreditAccount != null && ((CreditAccount)account).Credits.Count > 0)
            {
                return false;
            }
            else
            {
                account.Balance += account.Cards[cardNumber].Balance;
                account.Cards.RemoveAt(cardNumber);

                return true;
            }
        }

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
    }
}
