using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.interfaces
{
    public interface ICreditAccountService
    {
        bool DeleteCard(Account account, int cardNumber);

        void AddCard(Account account);

        bool AddCredit(Account account, int monthes, int sum);

        bool PayCredit(CreditAccount account, int chooseCredit);

        string GetCreditInfo(Account account);

        bool CheckDebtOfCredits(Account account);
    }
}
