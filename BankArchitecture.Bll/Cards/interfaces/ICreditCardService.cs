using BankArchitecture.Common;

namespace BankArchitecture.Bll.Cards.interfaces
{
    public interface ICreditCardService : ICardService
    {
        public string GetCreditsInfo(Card card);

        public bool AddCredit(Card card, int monthes, int sum);

        public bool PayCredit(Card card, int chooseCredit);

        public bool CheckDebtOfCredits(Card card);
    }
}
