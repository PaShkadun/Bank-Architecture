using BankArchitecture.Common.Models;

namespace BankArchitecture.Bll.Accounts.interfaces
{
    public interface IAccountService
    {
        bool DeleteCard(Account account, int cardNumber);

        string GetCardsInfo(Account account);

        void AddCard(Account account);
    }
}
