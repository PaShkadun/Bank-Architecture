using BankArchitecture.Common.Enums;
using BankArchitecture.Common.Models;

namespace BankArchitecture.Bll.Bank.interfaces
{
    public interface IBankService
    {
        void AddAccount(MainBank bank, TypeOfAccount type);

        bool DeleteAccount(MainBank bank, int chooseAccount);

        string GetAccountsInfo(MainBank bank);

        bool TransferMoneyToAccount(MainBank bank, int accountIndex, int sum);

        bool SeriallizeBank(MainBank bank);

        MainBank DeserializeBank();
    }
}
