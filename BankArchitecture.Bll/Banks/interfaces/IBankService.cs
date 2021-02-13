using BankArchitecture.Common.Enums;
using BankArchitecture.Common.Models;

namespace BankArchitecture.Bll.Bank.interfaces
{
    public interface IBankService
    {
        public void AddAccount(MainBank bank, TypeOfAccount type);

        public bool DeleteAccount(MainBank bank, int chooseAccount);

        public string GetAccountsInfo(MainBank bank);

        public bool TransferMoneyToAccount(MainBank bank, int accountIndex, int sum);

        public bool SeriallizeBank(MainBank bank);

        public MainBank DeserializeBank();
    }
}
