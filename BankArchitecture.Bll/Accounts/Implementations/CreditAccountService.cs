using System;
using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Bll.Random.Implementations;
using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.Implementations
{
    public class CreditAccountService : ICreditAccountService
    {
        public void AddCard(Account account)
        {
            account.Cards.Add(new CreditCard() { Id = CustomRandom.RandomCardNumber() });
        }

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
            else
            {
                return false;
            }
        }

        public bool DeleteCard(Account account, int chooseCard)
        {
            if (chooseCard >= account.Cards.Count || chooseCard < 0)
            {
                return false;
            }
            else
            {
                if (((CreditCard)account.Cards[chooseCard]).Credits.Count == 0 && account.Cards[chooseCard].Balance >= 0)
                {
                    account.Balance += account.Cards[chooseCard].Balance;

                    account.Cards.RemoveAt(chooseCard);

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string GetCardsInfo(Account account)
        {
            if (account.Cards.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                string cardsInfo = string.Empty;
                int count = 0;

                foreach (CreditCard card in account.Cards)
                {
                    cardsInfo += $"{count++}. {card.Id} {card.Balance} {card.Credits.Count}\n";
                }

                return cardsInfo;
            }
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
            else if (pushAccount as DebitAccount != null)
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

        public bool TransferMoneyToCard(Account account, Card card, int sum)
        {
            if (account.Balance < sum)
            {
                throw new NotImplementedException();
            }
            else
            {
                account.Balance -= sum;
                card.Balance += sum;

                return true;
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
                foreach (Credit credit in ((CreditAccount)account).Credits)
                {
                    if (credit.MonthesOfDebt != 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool SpendMoney(Account account, int money)
        {
            if (account.Balance >= money)
            {
                account.Balance -= money;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
