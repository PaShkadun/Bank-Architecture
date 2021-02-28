using Bank_Architecture_.Common.Enums;
using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common;
using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;
using System.Collections.Generic;

namespace BankArchitecture.Providers
{
    public class DebitCardProvider : IDebitCardProvider
    {
        private readonly ICardService service;
        private readonly IConsoleProvider consoleProvider;

        public DebitCardProvider(ICardService service, IConsoleProvider consoleProvider)
        {
            this.service = service;
            this.consoleProvider = consoleProvider;
        }

        public object ChooseAction(DebitCard card)
        {
            int choose = -1;

            while (choose != 0)
            {
                choose = consoleProvider.InputValue(StringConstants.DebitCardActions);

                switch ((DebitCardFunctions)choose)
                {
                    case DebitCardFunctions.SpendMoney:
                        int sum = consoleProvider.InputValue(StringConstants.InputValue);

                        if (service.HasMoney(card, sum))
                        {
                            card.Balance -= sum;

                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotMoney);
                        }

                        break;

                    case DebitCardFunctions.ShowBalance:
                        consoleProvider.ShowMessage(card.Balance.ToString());

                        break;

                    case DebitCardFunctions.TransferMoneyToAccount:
                        int transferSumToAccount = consoleProvider.InputValue(StringConstants.InputValue);

                        if (service.HasMoney(card, transferSumToAccount))
                        {
                            return new Dictionary<string, dynamic> { { StringConstants.Sender, card }, { StringConstants.Money, transferSumToAccount }, { StringConstants.Recipient, Recipient.Account } };
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.TransferMoneyError);
                        }

                        break;

                    case DebitCardFunctions.TransferMoneyTocard:
                        int transferSumToCard = consoleProvider.InputValue(StringConstants.InputValue);

                        if (service.HasMoney(card, transferSumToCard))
                        {
                            return new Dictionary<string, dynamic> { { StringConstants.Sender, card }, { StringConstants.Money, transferSumToCard }, { StringConstants.Recipient, Recipient.Account } };
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.TransferMoneyError);
                        }

                        break;

                    case DebitCardFunctions.Exit:
                        return null;

                    default:
                        consoleProvider.ShowMessage(StringConstants.IncorrectInput);

                        break;
                }
            }

            return null;
        }
    }
}
