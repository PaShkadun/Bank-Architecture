using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.interfaces
{
    public interface IAccountService
    {
        string GetCardsInfo(Account account);

        bool HasMoney(Account account, int money);
    }
}
