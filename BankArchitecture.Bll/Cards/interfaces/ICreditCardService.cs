using BankArchitecture.Common.Models;

namespace BankArchitecture.Bll.Cards.interfaces
{
    public interface ICreditCardService
    {
        string GetCreditsInfo(Card card);

        bool AddCredit(Card card, int monthes, int sum);

        bool PayCredit(Card card, int chooseCredit);

        bool CheckDebtOfCredits(Card card);
    }
}
