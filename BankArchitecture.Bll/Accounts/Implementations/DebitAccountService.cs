using System;
using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.Implementations
{
    public class DebitAccountService : IDebitAccountService
    {
        public void AddCard(Account account)
        {
            account.Cards.Add(new DebitCard());
        }

        public bool DeleteCard(Account account, int cardNumber)
        {
            if (cardNumber < 0 || account.Cards.Count <= cardNumber)
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            else
            {
                string cardsInfo = string.Empty;

                foreach (Card card in account.Cards)
                {
                    cardsInfo += $"{card.Id} {card.Balance}\n";
                }

                return cardsInfo;
            }
        }

        public bool TransferMoneyToAccount(Account pullAccount, Account pushAccount, int sum)
        {
            if (pullAccount == pushAccount)
            {
                throw new NotImplementedException();
            }
            else if (pullAccount.Balance < sum)
            {
                throw new NotImplementedException();
            }
            else
            {
                pullAccount.Balance -= sum;
                pushAccount.Balance += sum;

                return true;
            }
        }

        public bool TransferMoneyToCard(Account account, Card card, int sum)
        {
            if (account.Balance < sum)
            {
                throw new NotImplementedException();
            }
            else
            {
                account.Balance -= sum;
                card.Balance += sum;

                return true;
            }
        }
    }
}
