using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BankArchitecture.Common;
using BankArchitecture.Common.Models;

namespace BankArchitecture.Dal
{
    public static class BankSeriallizer
    {
        private static string pathToBankFile = "bank.json";
        private static string pathToCreditAccount = "creditAccounts.json";
        private static string pathToDebitAccount = "debitAccounts.json";

        public static async void SerializeBank(MainBank bank)
        {
            List<CreditAccount> creditAccounts = new List<CreditAccount>();
            List<DebitAccount> debitAccounts = new List<DebitAccount>();

            foreach (Account account in bank.Accounts)
            {
                if (account as CreditAccount != null)
                {
                    creditAccounts.Add((CreditAccount)account);
                }
                else
                {
                    debitAccounts.Add((DebitAccount)account);
                }
            }

            using (FileStream creditAccountsFile = new FileStream(pathToCreditAccount, FileMode.Create)) 
            {
                JsonSerializer.SerializeAsync(creditAccountsFile, creditAccounts);
            }

            using (FileStream debitAccountsFile = new FileStream(pathToDebitAccount, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(debitAccountsFile, debitAccounts);
            }

            using (FileStream bankFile = new FileStream(pathToBankFile, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(bankFile, bank);
            }
        }

        public static async Task<MainBank> DeserializeBank(MainBank bank)
        {
            List<CreditAccount> creditAccount = new List<CreditAccount>();
            List<DebitAccount> debitAccount = new List<DebitAccount>();
            List<Account> accounts = new List<Account>();

            if (File.Exists(pathToBankFile))
            {
                using (FileStream fileBank = new FileStream(pathToBankFile, FileMode.OpenOrCreate))
                {
                    bank = await JsonSerializer.DeserializeAsync<MainBank>(fileBank);
                }
            }

            if (File.Exists(pathToCreditAccount))
            {
                using (FileStream fileCreditAccount = new FileStream(pathToCreditAccount, FileMode.OpenOrCreate))
                {
                    creditAccount = await JsonSerializer.DeserializeAsync<List<CreditAccount>>(fileCreditAccount);
                }
            }

            if (File.Exists(pathToDebitAccount))
            {
                using (FileStream fileDebitAccount = new FileStream(pathToDebitAccount, FileMode.OpenOrCreate))
                {
                    debitAccount = await JsonSerializer.DeserializeAsync<List<DebitAccount>>(fileDebitAccount);
                }
            }

            accounts.AddRange(creditAccount);
            accounts.AddRange(debitAccount);
            accounts.OrderBy(account => account.Id);

            bank.Accounts = accounts;

            return bank;
        }
    }
}
