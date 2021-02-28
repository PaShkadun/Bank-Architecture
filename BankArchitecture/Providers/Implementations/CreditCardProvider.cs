using Bank_Architecture_.Common.Enums;
using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common;
using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;
using System.Collections.Generic;

namespace BankArchitecture.Providers
{
    public class CreditCardProvider : ICreditCardProvider
    {
        private readonly IConsoleProvider consoleProvider;
        private readonly ICreditCardService creditCardService;
        private readonly ICardService cardService;

        public CreditCardProvider(IConsoleProvider consoleProvider, ICreditCardService creditCardService, ICardService cardService)
        {
            this.consoleProvider = consoleProvider;
            this.creditCardService = creditCardService;
            this.cardService = cardService;
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

                        if (cardService.HasMoney(card, spendSum))
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
                        int transferSumToAccount = consoleProvider.InputValue(StringConstants.InputValue);

                        if (cardService.HasMoney(card, transferSumToAccount) && creditCardService.CheckDebtOfCredits(card))
                        {
                            return new Dictionary<string, dynamic> { { StringConstants.Sender, card }, { StringConstants.Money, transferSumToAccount }, { StringConstants.Recipient, Recipient.Account } };
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotMoney + "/" + StringConstants.TransferMoneyError);
                        }

                        break;

                    case CreditCardFunctions.TransferMoneyToCard:
                        int transferSumToCard = consoleProvider.InputValue(StringConstants.InputValue);

                        if (cardService.HasMoney(card, transferSumToCard) && creditCardService.CheckDebtOfCredits(card))
                        {
                            return new Dictionary<string, dynamic> { { StringConstants.Sender, card }, { StringConstants.Money, transferSumToCard }, { StringConstants.Recipient, Recipient.Account } };
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotMoney + "/" + StringConstants.TransferMoneyError);
                        }

                        break;

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
    }
}
