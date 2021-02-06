using BankArchitecture.Bll.Bank.interfaces;
using BankArchitecture.Common.Enums;
using BankArchitecture.Common.Models;
using BankArchitecture.Common;

namespace BankArchitecture.Bll.Bank.Implementations
{
    public class BankService : IBankService
    {
        public void AddAccount(TypeOfObject type)
        {
            if (type == TypeOfObject.Credit)
            {
                MainBank.Accounts.Add(new CreditAccount());
            }
            else
            {
                MainBank.Accounts.Add(new DebitAccount());
            }
        }

        public bool DeleteAccount(int chooseAccount)
        {
            if (chooseAccount < 0 && chooseAccount >= MainBank.Accounts.Count)
            {
                return false;
            }
            else if (MainBank.Accounts[chooseAccount].Cards.Count != 0)
            {
                return false;
            }
            else
            {
                MainBank.Accounts.RemoveAt(chooseAccount);

                return true;
            }
        }

        public string GetAccountsInfo()
        {
            if (MainBank.Accounts.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                string accountInfo = string.Empty;
                int countCards = 0;

                foreach (Account account in MainBank.Accounts)
                {
                    accountInfo += $"{countCards++}. {account.Id} {account.Balance}\n";
                }

                return accountInfo;
            }
        }

        public bool TransferMoneyToAccount(int accountIndex, int sum)
        {
            if (accountIndex > MainBank.Accounts.Count - 1 || accountIndex < 0)
            {
                return false;
            }
            else if (sum > MainBank.Balance)
            {
                return false;
            }
            else
            {
                MainBank.Accounts[accountIndex].Balance += sum;
                MainBank.Balance -= sum;

                return true;
            }
        }
    }
}
