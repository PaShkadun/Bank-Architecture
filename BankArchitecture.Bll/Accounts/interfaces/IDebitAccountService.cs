using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.interfaces
{
    public interface IDebitAccountService
    {
        bool DeleteCard(Account account, int cardNumber);

        void AddCard(Account account);
    }
}
