using BankArchitecture.Bll.Bank.interfaces;
using BankArchitecture.Common.Enums;
using BankArchitecture.Common.Models;
using BankArchitecture.Common;
using BankArchitecture.Dal;
using BankArchitecture.Bll.Random.Implementations;

namespace BankArchitecture.Bll.Bank.Implementations
{
    public class BankService : IBankService
    {
        public void AddAccount(MainBank bank, TypeOfAccount type)
        {
            if (type == TypeOfAccount.Credit)
            {
                bank.Accounts.Add(new CreditAccount() { Id = CustomRandom.RandomAccountNumber() }) ;
            }
            else
            {
                bank.Accounts.Add(new DebitAccount() { Id = CustomRandom.RandomAccountNumber() });
            }
        }

        public bool DeleteAccount(MainBank bank, int chooseAccount)
        {
            if (chooseAccount < 0 && chooseAccount >= bank.Accounts.Count)
            {
                return false;
            }
            else if (bank.Accounts[chooseAccount].Cards.Count != 0)
            {
                return false;
            }
            else
            {
                bank.Accounts.RemoveAt(chooseAccount);

                return true;
            }
        }

        public string GetAccountsInfo(MainBank bank)
        {
            if (bank.Accounts.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                string accountInfo = string.Empty;
                int countCards = 0;

                foreach (Account account in bank.Accounts)
                {
                    accountInfo += $"{countCards++}. {account.Id} {account.Balance}\n";
                }

                return accountInfo;
            }
        }

        public bool TransferMoneyToAccount(MainBank bank, int accountIndex, int sum)
        {
            if (accountIndex > bank.Accounts.Count - 1 || accountIndex < 0)
            {
                return false;
            }
            else if (sum > bank.Balance)
            {
                return false;
            }
            else
            {
                bank.Accounts[accountIndex].Balance += sum;
                bank.Balance -= sum;

                return true;
            }
        }

        public bool SeriallizeBank(MainBank bank)
        {
            BankSeriallizer.SerializeBank(bank);

            return true;
        }

        public MainBank DeserializeBank()
        {
            MainBank bank = new MainBank();
            bank = BankSeriallizer.DeserializeBank(bank).Result;

            return bank;
        }
    }
}
