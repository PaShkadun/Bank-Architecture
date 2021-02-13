using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common;
using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;

namespace BankArchitecture.Providers
{
    public class CreditAccountProvider : ICreditAccountProvider
    {
        private readonly ICreditAccountService servise;
        private readonly IConsoleProvider consoleProvider;
        private readonly ICreditCardProvider creditCardProvider;

        public CreditAccountProvider(ICreditAccountService servise, IConsoleProvider consoleProvider, ICreditCardProvider creditCardProvider)
        {
            this.consoleProvider = consoleProvider;
            this.creditCardProvider = creditCardProvider;
            this.servise = servise;
        }

        public void ChooseAction(CreditAccount account)
        {
            int choose = -1;

            while (choose != 0)
            {
                choose = consoleProvider.InputValue(StringConstants.CreditAccountActions);

                switch ((CreditAccountFunctions)choose)
                {
                    case CreditAccountFunctions.TransferToAccount:
                        //code
                        break;

                    case CreditAccountFunctions.TransferToCard:
                        //code
                        break;

                    case CreditAccountFunctions.PayCredit:
                        string info = servise.GetCreditInfo(account);

                        if (info != string.Empty)
                        {
                            if (servise.PayCredit(account, consoleProvider.InputValue(info)))
                            {
                                consoleProvider.ShowMessage(StringConstants.Successfully);
                            }
                            else
                            {
                                consoleProvider.ShowMessage(StringConstants.PayCreditError);
                            }
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.PayCreditError);
                        }

                        break;

                    case CreditAccountFunctions.AddCredit:
                        int sum = consoleProvider.InputValue(StringConstants.InputCreditSum);
                        int monthes = consoleProvider.InputValue(StringConstants.InputCreditMonthes);

                        if (servise.AddCredit(account, monthes, sum))
                        {
                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.AddCreditError);
                        }

                        break;

                    case CreditAccountFunctions.ShowCreditList:
                        string creditInfo = servise.GetCreditInfo(account);
                        
                        if (creditInfo == string.Empty)
                        {
                            creditInfo = StringConstants.HaveNotCredit;
                        }

                        consoleProvider.ShowMessage(creditInfo);

                        break;

                    case CreditAccountFunctions.SpendMoney:
                        int howMoney = consoleProvider.InputValue(StringConstants.InputValue);

                        if (servise.SpendMoney(account, howMoney))
                        {
                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotMoney);
                        }

                        break;

                    case CreditAccountFunctions.ShowBalance:
                        consoleProvider.ShowMessage(account.Balance.ToString());

                        break;

                    case CreditAccountFunctions.AddCard:
                        servise.AddCard(account);

                        consoleProvider.ShowMessage(StringConstants.Successfully);

                        break;

                    case CreditAccountFunctions.DeleteCard:
                        int chooseCard = consoleProvider.InputValue(servise.GetCardsInfo(account));

                        if (servise.DeleteCard(account, chooseCard))
                        {
                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.DeleteCreditCardError);
                        }

                        break;

                    case CreditAccountFunctions.ShowCardList:
                        string cardInfo = servise.GetCardsInfo(account);

                        if (cardInfo == string.Empty)
                        {
                            cardInfo = StringConstants.HaveNotCards;
                        }

                        consoleProvider.ShowMessage(cardInfo);

                        break;

                    case CreditAccountFunctions.ChooseCard:
                        ChooseCard(account);

                        break;

                    case CreditAccountFunctions.Exit:
                        return;

                    default:
                        consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                        break;
                }

                consoleProvider.WaitingPressAnyKey();
            }
        }

        private void ChooseCard(CreditAccount account)
        {
            string cardInfo = servise.GetCardsInfo(account);

            if (cardInfo == string.Empty)
            {
                consoleProvider.ShowMessage(StringConstants.HaveNotCards);

                return;
            }
            else
            {
                int chooseCard = consoleProvider.InputValue(cardInfo);

                if (chooseCard < 0 || chooseCard >= account.Cards.Count)
                {
                    consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                }
                else
                {
                    creditCardProvider.ChooseAction((CreditCard)account.Cards[chooseCard]);
                }
            }
        }
    }
}
