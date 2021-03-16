using BankArchitecture.Common.Enums;
using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common.Models;
using BankArchitecture.Resources;
using BankArchitecture.Providers.Interfaces;
using System.Collections.Generic;

namespace BankArchitecture.Providers.Implementations
{
    public class CreditAccountProvider : ICreditAccountProvider
    {
        private readonly ICreditAccountService creditAccountService;
        private readonly IAccountService accountService;
        private readonly IConsoleProvider consoleProvider;
        private readonly ICreditCardProvider creditCardProvider;

        public CreditAccountProvider(ICreditAccountService creditAccountService, IAccountService accountService, IConsoleProvider consoleProvider, ICreditCardProvider creditCardProvider)
        {
            this.consoleProvider = consoleProvider;
            this.creditCardProvider = creditCardProvider;
            this.creditAccountService = creditAccountService;
            this.accountService = accountService;
        }

        public object ChooseAction(CreditAccount account)
        {
            int choose = -1;

            while (choose != 0)
            {
                choose = consoleProvider.InputValue(StringConstants.CreditAccountActions);

                switch ((CreditAccountFunctions)choose)
                {
                    case CreditAccountFunctions.TransferToAccount:
                        double transferSumToAccount = consoleProvider.InputValue(StringConstants.InputValue);

                        return CanTransferMoneyTo(account, transferSumToAccount);

                    case CreditAccountFunctions.TransferToCard:
                        double transferSumToCard = consoleProvider.InputValue(StringConstants.InputValue);

                        return CanTransferMoneyTo(account, transferSumToCard);

                    case CreditAccountFunctions.PayCredit:
                        string info = creditAccountService.GetCreditInfo(account);

                        if (info != string.Empty)
                        {
                            if (creditAccountService.PayCredit(account, consoleProvider.InputValue(info)))
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

                        if (creditAccountService.AddCredit(account, monthes, sum))
                        {
                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.AddCreditError);
                        }

                        break;

                    case CreditAccountFunctions.ShowCreditList:
                        string creditInfo = creditAccountService.GetCreditInfo(account);
                        
                        if (creditInfo == string.Empty)
                        {
                            creditInfo = StringConstants.HaveNotCredit;
                        }

                        consoleProvider.ShowMessage(creditInfo);

                        break;

                    case CreditAccountFunctions.SpendMoney:
                        int howMoney = consoleProvider.InputValue(StringConstants.InputValue);

                        if (howMoney <= account.Balance)
                        {
                            account.Balance -= howMoney;

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
                        accountService.AddCard(account);

                        consoleProvider.ShowMessage(StringConstants.Successfully);

                        break;

                    case CreditAccountFunctions.DeleteCard:
                        int chooseCard = consoleProvider.InputValue(accountService.GetCardsInfo(account));

                        if (creditAccountService.CheckDebtOfCredits(account))
                        {
                            accountService.DeleteCard(account, chooseCard);

                            consoleProvider.ShowMessage(StringConstants.Successfully);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.DeleteCreditCardError);
                        }

                        break;

                    case CreditAccountFunctions.ShowCardList:
                        string cardInfo = accountService.GetCardsInfo(account);

                        if (cardInfo == string.Empty)
                        {
                            cardInfo = StringConstants.HaveNotCards;
                        }

                        consoleProvider.ShowMessage(cardInfo);

                        break;

                    case CreditAccountFunctions.ChooseCard:
                        object hasTransfer = ChooseCard(account);

                        if (hasTransfer != null)
                        {
                            return hasTransfer;
                        }

                        break;

                    case CreditAccountFunctions.Exit:
                        return null;

                    default:
                        consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                        break;
                }

                consoleProvider.WaitingPressAnyKey();
            }

            return null;
        }

        private object ChooseCard(CreditAccount account)
        {
            string cardInfo = accountService.GetCardsInfo(account);

            if (cardInfo == string.Empty)
            {
                consoleProvider.ShowMessage(StringConstants.HaveNotCards);

                return null;
            }
            else
            {
                int chooseCard = consoleProvider.InputValue(cardInfo);

                if (chooseCard < 0 || chooseCard >= account.Cards.Count)
                {
                    consoleProvider.ShowMessage(StringConstants.IncorrectInput);

                    return null;
                }
                else
                {
                    return creditCardProvider.ChooseAction((CreditCard)account.Cards[chooseCard]);
                }
            }
        }

        private object CanTransferMoneyTo(Account account, double howMoney)
        {
            if (howMoney <= account.Balance && creditAccountService.CheckDebtOfCredits(account))
            {
                return new Dictionary<string, object> { { StringConstants.Sender, account }, { StringConstants.Money, howMoney }, { StringConstants.Recipient, Recipient.Account } };
            }
            else
            {
                return StringConstants.HaveNotMoney + "/" + StringConstants.TransferMoneyError;
            }
        }
    }
}
