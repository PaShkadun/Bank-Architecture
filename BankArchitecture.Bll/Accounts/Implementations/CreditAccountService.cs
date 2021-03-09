using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Bll.Random.Implementations;
using BankArchitecture.Common.Models;

namespace BankArchitecture.Bll.Accounts.Implementations
{
    public class CreditAccountService : ICreditAccountService
    {
        public bool AddCredit(Account account, int monthes, int sum)
        {
            if (CheckDebtOfCredits(account))
            {
                if (sum > 0 && monthes > 0)
                {
                    account.Balance += sum;

                    ((CreditAccount)account).Credits.Add(new Credit(monthes, sum));

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public string GetCreditInfo(Account account)
        {
            if (((CreditAccount)account).Credits.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                string creditInfo = string.Empty;

                foreach (Credit credit in ((CreditAccount)account).Credits)
                {
                    creditInfo += $"{credit.Monthes} {credit.MonthesOfDebt} {credit.MonthlySum}\n";
                }

                return creditInfo;
            }
        }

        public bool PayCredit(CreditAccount account, int chooseCredit)
        {
            if (chooseCredit > account.Credits.Count || chooseCredit < 0)
            {
                return false;
            }
            else if (account.Credits[chooseCredit].MonthesOfDebt == 0)
            {
                return false;
            }
            else
            {
                if ((account.Credits[chooseCredit].MonthesOfDebt * account.Credits[chooseCredit].MonthlySum) <= account.Balance)
                {
                    account.Balance -= account.Credits[chooseCredit].MonthesOfDebt * account.Credits[chooseCredit].MonthlySum;
                    account.Credits[chooseCredit].MonthesOfDebt = 0;

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckDebtOfCredits(Account account)
        {
            if (((CreditAccount)account).Credits.Count == 0)
            {
                return true;
            }
            else
            {
                foreach (var credit in ((CreditAccount)account).Credits)
                {
                    if (credit.MonthesOfDebt != 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
