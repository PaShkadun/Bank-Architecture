using BankArchitecture.Common.Models;

namespace BankArchitecture.Bll.Accounts.interfaces
{
    public interface ICreditAccountService
    {
        bool AddCredit(Account account, int monthes, int sum);

        bool PayCredit(CreditAccount account, int chooseCredit);

        string GetCreditInfo(Account account);

        bool CheckDebtOfCredits(Account account);
    }
}
