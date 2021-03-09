using BankArchitecture.Common.Enums;
using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common.Models;
using BankArchitecture.Resources;
using System.Collections.Generic;
using BankArchitecture.Providers.Interfaces;

namespace BankArchitecture.Providers.Implementations
{
    public class CreditCardProvider : ICreditCardProvider
    {
        private readonly IConsoleProvider consoleProvider;
        private readonly ICreditCardService creditCardService;

        public CreditCardProvider(IConsoleProvider consoleProvider, ICreditCardService creditCardService)
        {
            this.consoleProvider = consoleProvider;
            this.creditCardService = creditCardService;
        }

        public object ChooseAction(CreditCard card)
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

                        if (creditCardService.AddCredit(card, monthes, sum))
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

                        if (creditCardService.PayCredit(card, choosenCredit))
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

                        if (spendSum <= card.Balance)
                        {
                            card.Balance -= spendSum;

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

                    case CreditCardFunctions.TransferMoneyToAccount:
                        double transferSumToAccount = consoleProvider.InputValue(StringConstants.InputValue);

                        return CanTransferMoneyTo(card, transferSumToAccount);

                    case CreditCardFunctions.TransferMoneyToCard:
                        double transferSumToCard = consoleProvider.InputValue(StringConstants.InputValue);

                        return CanTransferMoneyTo(card, transferSumToCard);

                    case CreditCardFunctions.Exit:
                        return null;

                    default:
                        break;
                }

                consoleProvider.WaitingPressAnyKey();
            }

            return null;
        }

        private void ShowCredits(CreditCard card)
        {
            string creditInfo = creditCardService.GetCreditsInfo(card);

            if (creditInfo == string.Empty)
            {
                creditInfo = StringConstants.HaveNotCredit;
            }

            consoleProvider.ShowMessage(creditInfo);
        }

        private object CanTransferMoneyTo(Card card, double howMoney)
        {
            if (howMoney <= card.Balance && creditCardService.CheckDebtOfCredits(card))
            {
                return new Dictionary<string, object> { { StringConstants.Sender, card }, { StringConstants.Money, howMoney }, { StringConstants.Recipient, Recipient.Account } };
            }
            else
            {
               return StringConstants.HaveNotMoney + "/" + StringConstants.TransferMoneyError;
            }
        }
    }
}
