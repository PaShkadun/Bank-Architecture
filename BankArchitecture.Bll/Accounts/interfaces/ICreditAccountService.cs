using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.interfaces
{
    public interface ICreditAccountService : IAccountService
    {
        public bool AddCredit(Account account, int monthes, int sum);

        public bool PayCredit(CreditAccount account, int chooseCredit);

        public string GetCreditInfo(Account account);

        public bool CheckDebtOfCredits(Account account);
    }
}
