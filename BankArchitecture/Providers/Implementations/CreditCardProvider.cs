using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common;
using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;

namespace BankArchitecture.Providers
{
    public class CreditCardProvider : ICreditCardProvider
    {
        private readonly IConsoleProvider consoleProvider;
        private readonly ICreditCardService service;

        public CreditCardProvider(IConsoleProvider consoleProvider, ICreditCardService service)
        {
            this.consoleProvider = consoleProvider;
            this.service = service;
        }

        public void ChooseAction(CreditCard card)
        {
            int choose = -1;

            while (choose != 0)
            {
                choose = consoleProvider.InputValue(StringConstants.CreditCardActions);

                switch ((CreditCardFunctions)choose)
                {
                    case CreditCardFunctions.AddCredit:
                        int sum = consoleProvider.InputValue(StringConstants.InputCreditSum);
                        int monthes = consoleProvider.InputValue(StringConstants.InputCreditMonthes);

                        if (service.AddCredit(card, monthes, sum))
                        {
                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.AddCreditError);
                        }

                        break;

                    case CreditCardFunctions.PayCredit:
                        ShowCredits(card);

                        int choosenCredit = consoleProvider.InputValue(StringConstants.InputValue);

                        if (service.PayCredit(card, choosenCredit))
                        {
                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.PayCreditError);
                        }

                        break;

                    case CreditCardFunctions.ShowCredits:
                        ShowCredits(card);

                        break;

                    case CreditCardFunctions.SpendMoney:
                        int spendSum = consoleProvider.InputValue(StringConstants.InputValue);

                        if (service.SpendMoney(card, spendSum))
                        {
                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotMoney);
                        }

                        break;

                    case CreditCardFunctions.ShowBalance:
                        consoleProvider.ShowMessage(card.Balance.ToString());

                        break;

                    default:
                        break;
                }
            }
        }

        private void ShowCredits(CreditCard card)
        {
            string creditInfo = service.GetCreditsInfo(card);

            if (creditInfo == string.Empty)
            {
                creditInfo = StringConstants.HaveNotCredit;
            }

            consoleProvider.ShowMessage(creditInfo);
        }
    }
}
