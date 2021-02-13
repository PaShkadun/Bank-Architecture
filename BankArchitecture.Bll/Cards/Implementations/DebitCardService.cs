using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common;
using System;

namespace BankArchitecture.Bll.Cards.Implementations
{
    public class DebitCardService : IDebitCardService
    {
        public bool SpendMoney(Card card, int sum)
        {
            if (card.Balance < sum || sum < 0)
            {
                return false;
            }
            else
            {
                card.Balance -= sum;

                return true;
            }
        }

        public bool TransferMoneyToCard(Card pullCard, Card pushCard, int sum)
        {
            if (pullCard == pushCard)
            {
                throw new NotImplementedException();
            }
            else if (pullCard.Balance <= sum || sum < 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                pullCard.Balance -= sum;
                pushCard.Balance += sum;

                return true;
            }
        }
    }
}
