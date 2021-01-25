using BankArchitecture.Common;

namespace BankArchitecture.Bll.Cards.interfaces
{
    public interface ICardService
    {
        public bool TransferMoneyToCard(Card pullCard, Card pushCard, int sum);

        public bool SpendMoney(Card card, int sum);
    }
}
