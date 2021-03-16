using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common.Models;
using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;
using System.Collections.Generic;
using BankArchitecture.Providers.Interfaces;

namespace BankArchitecture.Providers.Implementations
{
    public class DebitCardProvider : IDebitCardProvider
    {
        private readonly IConsoleProvider consoleProvider;

        public DebitCardProvider(IConsoleProvider consoleProvider)
        {
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

                        if (sum <= card.Balance)
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
                        double transferSumToAccount = consoleProvider.InputValue(StringConstants.InputValue);

                        return CanTransferMoneyTo(card, transferSumToAccount);

                    case DebitCardFunctions.TransferMoneyTocard:
                        double transferSumToCard = consoleProvider.InputValue(StringConstants.InputValue);

                        return CanTransferMoneyTo(card, transferSumToCard);

                    case DebitCardFunctions.Exit:
                        return null;

                    default:
                        consoleProvider.ShowMessage(StringConstants.IncorrectInput);

                        break;
                }
            }

            return null;
        }

        private object CanTransferMoneyTo(Card card, double howMoney)
        {
            if (howMoney <= card.Balance)
            {
                return new Dictionary<string, object> { { StringConstants.Sender, card }, { StringConstants.Money, howMoney }, { StringConstants.Recipient, Recipient.Account } };
            }
            else
            {
                return StringConstants.TransferMoneyError;
            }
        }
    }
}
