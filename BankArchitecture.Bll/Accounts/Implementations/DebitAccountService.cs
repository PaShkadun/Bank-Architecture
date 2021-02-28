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
    }
}
