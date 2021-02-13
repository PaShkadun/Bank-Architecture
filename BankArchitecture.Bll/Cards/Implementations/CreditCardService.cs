using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common;
using System;

namespace BankArchitecture.Bll.Cards.Implementations
{
    public class CreditCardService : ICreditCardService
    {
        public bool AddCredit(Card card, int monthes, int sum)
        {
            if (CheckDebtOfCredits(card))
            {
                if (sum > 0 && monthes > 0)
                {
                    card.Balance += sum;

                    ((CreditCard)card).Credits.Add(new Credit(monthes, sum));

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

        public bool CheckDebtOfCredits(Card card)
        {
            if (((CreditCard)card).Credits.Count != 0)
            {
                foreach (Credit credit in ((CreditCard)card).Credits)
                {
                    if (credit.MonthesOfDebt != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public string GetCreditsInfo(Card card)
        {
            if (((CreditCard)card).Credits.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                string creditInfo = string.Empty;
                int count = 0;

                foreach (Credit credit in ((CreditCard)card).Credits)
                {
                    creditInfo += $"{count++}. {credit.Monthes} {credit.MonthesOfDebt} {credit.MonthlySum}\n";
                }

                return creditInfo;
            }
        }

        public bool PayCredit(Card card, int chooseCredit)
        {
            if (chooseCredit < 0 || chooseCredit >= ((CreditCard)card).Credits.Count)
            {
                return false;
            }
            else if (((CreditCard)card).Credits[chooseCredit].MonthesOfDebt == 0)
            {
                return false;
            }
            else if ((((CreditCard)card).Credits[chooseCredit].MonthesOfDebt * ((CreditCard)card).Credits[chooseCredit].MonthlySum) > card.Balance)
            {
                return false;
            }
            else
            {
                card.Balance -= ((CreditCard)card).Credits[chooseCredit].MonthesOfDebt * ((CreditCard)card).Credits[chooseCredit].MonthlySum;
                ((CreditCard)card).Credits[chooseCredit].MonthesOfDebt = 0;

                return true;
            }
        }

        public bool SpendMoney(Card card, int sum)
        {
            if (sum > card.Balance || sum < 0)
            {
                return false;
            }
            else
            {
                card.Balance -= sum;

                return true;
            }
        }

        public bool TransferMoneyToCard(Card pullCard, Card pushCard, int sum)
        {
            if (pullCard == pushCard)
            {
                throw new NotImplementedException();
            }
            else if (pullCard.Balance < sum)
            {
                throw new NotImplementedException();
            }
            else if (pushCard as DebitCard != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                pullCard.Balance -= sum;
                pushCard.Balance += sum;

                return true;
            }
        }
    }
}
