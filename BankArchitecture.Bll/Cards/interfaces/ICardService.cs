using BankArchitecture.Common;

namespace BankArchitecture.Bll.Cards.interfaces
{
    public interface ICardService
    {
        bool HasMoney(Card card, int sum);
    }
}
