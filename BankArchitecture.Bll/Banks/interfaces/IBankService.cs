using BankArchitecture.Common.Enums;

namespace BankArchitecture.Bll.Bank.interfaces
{
    public interface IBankService
    {
        public void AddAccount(TypeOfObject type);

        public bool DeleteAccount(int chooseAccount);

        public string GetAccountsInfo();

        public bool TransferMoneyToAccount(int indexAccount, int sum);
    }
}
