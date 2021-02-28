using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common;

namespace BankArchitecture.Bll.Cards.Implementations
{
    public class CardService : ICardService
    {
        public bool HasMoney(Card card, int sum)
        {
            if (sum > card.Balance || sum < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
