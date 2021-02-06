using System;
using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common;

namespace BankArchitecture.Bll.Accounts.Implementations
{
    public class CreditAccountService : ICreditAccountService
    {
        public void AddCard(Account account)
        {
            account.Cards.Add(new CreditCard());
        }

        public bool AddCredit(Account account, int monthes, int sum)
        {
            if (CheckDebtOfCredits(account))
            {
                ((CreditAccount)account).Credits.Add(new Credit(monthes, sum));

                return true;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public bool DeleteCard(Account account, int chooseCard)
        {
            if (chooseCard >= account.Cards.Count || chooseCard < 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                if (((CreditCard)account.Cards[chooseCard]).Credits.Count > 0 && account.Cards[chooseCard].Balance >= 0)
                {
                    account.Balance += account.Cards[chooseCard].Balance;

                    account.Cards.RemoveAt(chooseCard);

                    return true;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public string GetCardsInfo(Account account)
        {
            if (account.Cards.Count == 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                string cardsInfo = string.Empty;

                foreach (CreditCard card in account.Cards)
                {
                    cardsInfo += $"{card.Id} {card.Balance} {card.Credits.Count}\n";
                }

                return cardsInfo;
            }
        }

        public string GetCreditInfo(Account account)
        {
            if (((CreditAccount)account).Credits.Count == 0)
            {
                throw new NotImplementedException();
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

        public bool PayCredit(Account account, int chooseCredit)
        {
            if (chooseCredit > ((CreditAccount)account).Credits.Count || chooseCredit < 0)
            {
                throw new NotImplementedException();
            }
            else if (((CreditAccount)account).Credits[chooseCredit].MonthesOfDebt == 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                if ((((CreditAccount)account).Credits[chooseCredit].MonthesOfDebt * ((CreditAccount)account).Credits[chooseCredit].MonthlySum) <= account.Balance)
                {
                    account.Balance -= ((CreditAccount)account).Credits[chooseCredit].MonthesOfDebt * ((CreditAccount)account).Credits[chooseCredit].MonthlySum;
                    ((CreditAccount)account).Credits[chooseCredit].MonthesOfDebt = 0;

                    return true;
                }
                else
                {
                    throw new NotImplementedException();
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
                throw new NotImplementedException();
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
    }
}
