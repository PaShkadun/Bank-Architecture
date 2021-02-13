using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.interfaces
{
    public interface IAccountService
    {
        public bool TransferMoneyToCard(Account account, Card card, int sum);

        public string GetCardsInfo(Account account);

        public bool TransferMoneyToAccount(Account pullAccount, Account pushAccount, int sum);

        public void AddCard(Account account);

        public bool DeleteCard(Account account, int cardNumber);

        public bool SpendMoney(Account account, int money);
    }
}
