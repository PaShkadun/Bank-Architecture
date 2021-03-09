using BankArchitecture.Common.Enums;
using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common.Models;
using BankArchitecture.Resources;
using System.Collections.Generic;
using BankArchitecture.Providers.Interfaces;

namespace BankArchitecture.Providers.Implementations
{
    public class DebitAccountProvider : IDebitAccountProvider
    {
        private readonly IConsoleProvider consoleProvider;
        private readonly IDebitCardProvider debitCardProvider;
        private readonly IDebitAccountService debitAccountService;
        private readonly IAccountService accountService;

        public DebitAccountProvider(IDebitAccountService debitAccountService, IAccountService accountService, IConsoleProvider consoleProvider, IDebitCardProvider debitCardProvider)
        {
            this.consoleProvider = consoleProvider;
            this.debitCardProvider = debitCardProvider;
            this.debitAccountService = debitAccountService;
            this.accountService = accountService;
        }

        public object ChooseAction(DebitAccount account)
        {
            int choose = -1;

            while (choose != 0)
            {
                choose = consoleProvider.InputValue(StringConstants.DebitAccountActions);

                switch ((DebitAccountFunctions)choose)
                {
                    case DebitAccountFunctions.TransferToAccount:
                        double transferSumToAccount = consoleProvider.InputValue(StringConstants.InputValue);

                        return CanTransferMoneyTo(account, transferSumToAccount);

                    case DebitAccountFunctions.TransferToCard:
                        double transferSumToCard = consoleProvider.InputValue(StringConstants.InputValue);

                        return CanTransferMoneyTo(account, transferSumToCard);

                    case DebitAccountFunctions.SpendMoney:
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

                    case DebitAccountFunctions.ShowBalance:
                        consoleProvider.ShowMessage(account.Balance.ToString());

                        break;

                    case DebitAccountFunctions.AddCard:
                        accountService.AddCard(account);
                        consoleProvider.ShowMessage(StringConstants.Successfully);

                        break;

                    case DebitAccountFunctions.DeleteCard:
                        int chooseCard = consoleProvider.InputValue(accountService.GetCardsInfo(account));

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
                        string info = accountService.GetCardsInfo(account);

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
                        object hasTransfer = ChooseCard(account);

                        if (hasTransfer != null)
                        {
                            return hasTransfer;
                        }

                        break;

                    case DebitAccountFunctions.Exit:
                        return null;

                    default:
                        consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                        break;
                }

                consoleProvider.WaitingPressAnyKey();
            }

            return null;
        }

        private object ChooseCard(Account account)
        {
            string cardInfo = accountService.GetCardsInfo(account);

            if (cardInfo != string.Empty)
            {
                int getChooseCard = consoleProvider.InputValue(accountService.GetCardsInfo(account));

                if (getChooseCard < 0 || getChooseCard >= account.Cards.Count)
                {
                    consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                    
                    return null;
                }
                else
                {
                    return debitCardProvider.ChooseAction((DebitCard)account.Cards[getChooseCard]);
                }
            }
            else
            {
                consoleProvider.ShowMessage(StringConstants.HaveNotCards);

                return null;
            }
        }

        private object CanTransferMoneyTo(Account account, double howMoney)
        {
            if (howMoney <= account.Balance)
            {
                return new Dictionary<string, object> { { StringConstants.Sender, account }, { StringConstants.Money, howMoney }, { StringConstants.Recipient, Recipient.Account } };
            }
            else
            {
                return StringConstants.HaveNotMoney;
            }
        }
    }
}
