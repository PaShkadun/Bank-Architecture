using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common;
using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;

namespace BankArchitecture.Providers
{
    public class DebitAccountProvider : IDebitAccountProvider
    {
        private readonly IConsoleProvider consoleProvider;
        private readonly IDebitCardProvider debitCardProvider;
        private readonly IDebitAccountService service;

        public DebitAccountProvider(IDebitAccountService service, IConsoleProvider consoleProvider, IDebitCardProvider debitCardProvider)
        {
            this.consoleProvider = consoleProvider;
            this.debitCardProvider = debitCardProvider;
            this.service = service;
        }

        public void ChooseAction(DebitAccount account)
        {
            int choose = -1;

            while (choose != 0)
            {
                choose = consoleProvider.InputValue(StringConstants.DebitAccountActions);

                switch ((DebitAccountFunctions)choose)
                {
                    case DebitAccountFunctions.TransferToAccount:
                        //Code
                        break;

                    case DebitAccountFunctions.TransferToCard:
                        //Code
                        break;

                    case DebitAccountFunctions.SpendMoney:
                        int howMoney = consoleProvider.InputValue(StringConstants.InputValue);

                        if (service.SpendMoney(account, howMoney))
                        {
                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotMoney);
                        }

                        break;

                    case DebitAccountFunctions.ShowBalance:
                        consoleProvider.ShowMessage(account.Balance.ToString());

                        break;

                    case DebitAccountFunctions.AddCard:
                        service.AddCard(account);
                        consoleProvider.ShowMessage(StringConstants.Successfully);

                        break;

                    case DebitAccountFunctions.DeleteCard:
                        int chooseCard = consoleProvider.InputValue(service.GetCardsInfo(account));

                        if (chooseCard < 0 || chooseCard >= account.Cards.Count)
                        {
                            consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                        }
                        else
                        {
                            account.Balance += account.Cards[chooseCard].Balance;
                            account.Cards.RemoveAt(chooseCard);

                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }

                        break;

                    case DebitAccountFunctions.ShowCardList:
                        string info = service.GetCardsInfo(account);

                        if (info == string.Empty)
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotCards);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(info);
                        }

                        break;

                    case DebitAccountFunctions.ChooseCard:
                        string cardInfo = service.GetCardsInfo(account);

                        if (cardInfo != string.Empty)
                        {
                            int getChooseCard = consoleProvider.InputValue(service.GetCardsInfo(account));

                            if (getChooseCard < 0 || getChooseCard >= account.Cards.Count)
                            {
                                consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                            }
                            else
                            {
                                debitCardProvider.ChooseAction((DebitCard)account.Cards[getChooseCard]);
                            }
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotCards);
                        }

                        break;

                    case DebitAccountFunctions.Exit:
                        return;

                    default:
                        consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                        break;
                }

                consoleProvider.WaitingPressAnyKey();
            }
        }
    }
}
