using System;
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
            if(type == TypeOfObject.Credit)
            {
                StaticBank.Accounts.Add(new CreditAccount());
            }
            else
            {
                StaticBank.Accounts.Add(new DebitAccount());
            }
        }

        public bool DeleteAccount(int chooseAccount)
        {
            if (chooseAccount < 0 && chooseAccount >= StaticBank.Accounts.Count)
            {
                throw new NotImplementedException();
            }
            else if (StaticBank.Accounts[chooseAccount].Cards.Count != 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                StaticBank.Accounts.RemoveAt(chooseAccount);

                return true;
            }
        }

        public string GetAccountsInfo()
        {
            if (StaticBank.Accounts.Count == 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                string accountInfo = string.Empty;

                foreach (Account account in StaticBank.Accounts)
                {
                    accountInfo += $"{account.Id} {account.Balance}\n";
                }

                return accountInfo;
            }
        }

        public bool TransferMoneyToAccount(Account pullAccount, Account pushAccount, int sum)
        {
            if (pullAccount == pushAccount)
            {
                throw new NotImplementedException();
            }
            else if (pullAccount.Balance < sum)
            {
                throw new NotImplementedException();
            }
            else
            {
                pullAccount.Balance -= sum;
                pushAccount.Balance += sum;

                return true;
            }
        }
    }
}
