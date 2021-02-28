using Bank_Architecture_.Common.Enums;
using BankArchitecture.Bll.Accounts.interfaces;
using BankArchitecture.Common;
using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;
using System.Collections.Generic;

namespace BankArchitecture.Providers
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
                        int transferSumToAccount = consoleProvider.InputValue(StringConstants.InputValue);

                        if (accountService.HasMoney(account, transferSumToAccount) && creditAccountService.CheckDebtOfCredits(account))
                        {
                            return new Dictionary<string, dynamic> { { StringConstants.Sender, account }, { StringConstants.Money, transferSumToAccount }, { StringConstants.Recipient, Recipient.Account } };
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotMoney + "/" + StringConstants.TransferMoneyError);
                        }

                        break;

                    case CreditAccountFunctions.TransferToCard:
                        int transferSumToCard = consoleProvider.InputValue(StringConstants.InputValue);

                        if (accountService.HasMoney(account, transferSumToCard) && creditAccountService.CheckDebtOfCredits(account))
                        {
                            return new Dictionary<string, dynamic> { { StringConstants.Sender, account }, { StringConstants.Money, transferSumToCard }, { StringConstants.Recipient, Recipient.Account } };
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotMoney + "/" + StringConstants.TransferMoneyError);
                        }

                        break;

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

                        if (accountService.HasMoney(account, howMoney))
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
                        creditAccountService.AddCard(account);

                        consoleProvider.ShowMessage(StringConstants.Successfully);

                        break;

                    case CreditAccountFunctions.DeleteCard:
                        int chooseCard = consoleProvider.InputValue(accountService.GetCardsInfo(account));

                        if (creditAccountService.DeleteCard(account, chooseCard))
                        {
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

        public void ChooseRecipientCard(CreditAccount account, Dictionary<string, dynamic> information)
        {
            consoleProvider.ShowMessage(accountService.GetCardsInfo(account));

            int chooseRecipientCard = consoleProvider.InputValue(StringConstants.InputValue);

            if (chooseRecipientCard >= 0 && chooseRecipientCard < account.Cards.Count)
            {
                information[StringConstants.Recipient] = account.Cards[chooseRecipientCard];
            }
            else
            {
                information[StringConstants.Recipient] = null;
            }
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
    }
}
