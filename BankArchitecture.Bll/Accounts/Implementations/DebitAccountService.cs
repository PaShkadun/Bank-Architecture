using System;
using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Bll.Random.Implementations;
using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.Implementations
{
    public class DebitAccountService : IDebitAccountService
    {
        public void AddCard(Account account)
        {
            account.Cards.Add(new DebitCard() { Id = CustomRandom.RandomCardNumber()});
        }

        public bool DeleteCard(Account account, int cardNumber)
        {
            if (cardNumber < 0 || account.Cards.Count <= cardNumber)
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
                string cardsInfo = string.Empty;
                int count = 0;

                foreach (Card card in account.Cards)
                {
                    cardsInfo += $"{count++}. {card.Id} {card.Balance}\n";
                }

                return cardsInfo;
            }
        }

        public bool SpendMoney(Account account, int money)
        {
            if (account.Balance >= money)
            {
                account.Balance -= money;

                return true;
            }
            else
            {
                return false;
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
