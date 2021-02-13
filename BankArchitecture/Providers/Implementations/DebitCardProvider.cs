using BankArchitecture.Bll.Cards.interfaces;
using BankArchitecture.Common;
using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;

namespace BankArchitecture.Providers
{
    public class DebitCardProvider : IDebitCardProvider
    {
        private readonly IDebitCardService service;
        private readonly IConsoleProvider consoleProvider;

        public DebitCardProvider(IDebitCardService service, IConsoleProvider consoleProvider)
        {
            this.service = service;
            this.consoleProvider = consoleProvider;
        }

        public void ChooseAction(DebitCard card)
        {
            int choose = -1;

            while (choose != 0)
            {
                choose = consoleProvider.InputValue(StringConstants.DebitCardActions);

                switch ((DebitCardFunctions)choose)
                {
                    case DebitCardFunctions.SpendMoney:
                        int sum = consoleProvider.InputValue(StringConstants.InputValue);

                        if (service.SpendMoney(card, sum))
                        {
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

                    case DebitCardFunctions.Exit:
                        return;
                }
            }
        }
    }
}
