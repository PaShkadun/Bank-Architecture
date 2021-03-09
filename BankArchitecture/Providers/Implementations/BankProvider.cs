using BankArchitecture.Bll.Bank.interfaces;
using BankArchitecture.Common.Models;
using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;
using System.Collections.Generic;
using BankArchitecture.Providers.Interfaces;

namespace BankArchitecture.Providers.Implementations
{
    public class BankProvider : IBankProvider
    {
        private readonly IBankService service;
        private readonly ICreditAccountProvider creditAccountProvider;
        private readonly IConsoleProvider consoleProvider;
        private readonly IDebitAccountProvider debitAccountProvider;
        private readonly IAccountProvider accountProvider;
        private MainBank bank;

        public BankProvider(IBankService service, IConsoleProvider consoleProvider, ICreditAccountProvider creditAccountProvider, IAccountProvider accountProvider, IDebitAccountProvider debitAccountProvider)
        {
            this.service = service;
            this.creditAccountProvider = creditAccountProvider;
            this.consoleProvider = consoleProvider;
            this.debitAccountProvider = debitAccountProvider;
            this.accountProvider = accountProvider;
        }

        public void Start(int choose)
        {
            switch (choose)
            {
                case 1: 
                    bank = new MainBank();

                    consoleProvider.ShowMessage(StringConstants.InputBankName);
                    bank.Name = consoleProvider.InputStringValue();

                    break;

                case 2:
                    bank = service.DeserializeBank();

                    consoleProvider.WaitingPressAnyKey();

                    break;

                default:
                    consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                    return;
            }

            Actions();
        }

        public void Actions()
        {
            int choose = -1;

            while (choose != 0)
            {
                choose = consoleProvider.InputValue(StringConstants.ChooseActionOfBank);

                switch ((BankFunctions)choose)
                {
                    case BankFunctions.AddAccount:
                        service.AddAccount(bank, consoleProvider.ChooseTypeOfObject());
                        consoleProvider.ShowMessage(StringConstants.Successfully);

                        break;

                    case BankFunctions.DeleteAccount:
                        ChooseAccountForDelete();

                        break;

                    case BankFunctions.GetAccountsInfo:
                        string getInfo = service.GetAccountsInfo(bank);

                        if (getInfo == string.Empty)
                        {
                            consoleProvider.ShowMessage(StringConstants.HaveNotAccounts);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(getInfo);
                        }

                        break;

                    case BankFunctions.ShowBalance:
                        consoleProvider.ShowMessage(bank.Balance.ToString());

                        break;

                    case BankFunctions.AddMoneyToBalance:
                        bank.Balance += consoleProvider.InputValue(StringConstants.InputValue);

                        consoleProvider.ShowMessage(StringConstants.Successfully);

                        break;

                    case BankFunctions.TransferMoneyToAccount:
                        ChooseAccountForTransfer();

                        break;

                    case BankFunctions.ActionsWithAccounts:
                        object hasTransfer = ChooseAccount();

                        if (hasTransfer != null)
                        {
                            if (hasTransfer as Dictionary<string, object> != null)
                            {
                                ChooseRecipientAccount(hasTransfer);
                            }
                            else
                            {
                                consoleProvider.ShowMessage(hasTransfer.ToString());
                            }
                        }

                        break;

                    case BankFunctions.SeriallizedBank:
                        service.SeriallizeBank(bank);

                        break;

                    default:
                        consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                        break;
                };

                consoleProvider.WaitingPressAnyKey();
            }

            return;
        }

        private void TransferMoneyOperation(Dictionary<string, object> info, object recipient)
        {
            if (info[StringConstants.Sender] == recipient || recipient == null)
            {
                consoleProvider.ShowMessage(StringConstants.TransferMoneyError);
            }
            else
            {
                double howMoney = (double)info[StringConstants.Money];

                if (recipient as Card != null)
                {
                    ((Card)recipient).Balance += howMoney;
                }
                else
                {
                    ((Account)recipient).Balance += howMoney;
                }

                if (info[StringConstants.Sender] as Card != null)
                {
                    ((Card)info[StringConstants.Sender]).Balance -= howMoney;
                }
                else
                {
                    ((Account)info[StringConstants.Sender]).Balance -= howMoney;
                }

                consoleProvider.ShowMessage(StringConstants.Successfully);
            }
        }

        private void ChooseRecipientAccount(object information)
        {
            consoleProvider.ShowMessage(service.GetAccountsInfo(bank));

            int choose = consoleProvider.InputValue(StringConstants.InputValue);

            if (choose >= 0 && choose <= bank.Accounts.Count - 1)
            {
                Dictionary<string, object> transferInfo = (Dictionary<string, object>)information;

                if ((Recipient)transferInfo[StringConstants.Recipient] == Recipient.Account)
                {
                    TransferMoneyOperation(transferInfo, bank.Accounts[choose]);
                }
                else
                {
                    TransferMoneyOperation(transferInfo, accountProvider.ChooseRecipientCard(bank.Accounts[choose]));
                }
            }
            else
            {
                consoleProvider.ShowMessage(StringConstants.IncorrectInput);
            }
        }

        private object ChooseAccount()
        {
            int chooseAccount = consoleProvider.InputValue(service.GetAccountsInfo(bank));

            if (bank.Accounts.Count < chooseAccount || chooseAccount < 0)
            {
                consoleProvider.ShowMessage(StringConstants.IncorrectInput);

                return null;
            }
            else if (bank.Accounts[chooseAccount] as CreditAccount != null)
            {
                return creditAccountProvider.ChooseAction((CreditAccount)bank.Accounts[chooseAccount]);
            }
            else
            {
                return debitAccountProvider.ChooseAction((DebitAccount)bank.Accounts[chooseAccount]);
            }
        }

        public void ChooseAccountForTransfer()
        {
            string getInfo = service.GetAccountsInfo(bank);

            if (getInfo == string.Empty)
            {
                consoleProvider.ShowMessage(StringConstants.ErrorTransferMoneyOnAccount);
            }
            else
            {
                consoleProvider.ShowMessage(getInfo);

                int chooseAccount = consoleProvider.InputValue(StringConstants.ChooseAccountForTransferMoney);
                int chooseSum = consoleProvider.InputValue(StringConstants.InputValue);

                if (service.TransferMoneyToAccount(bank, chooseAccount, chooseSum))
                {
                    consoleProvider.ShowMessage(StringConstants.Successfully);
                }
                else
                {
                    consoleProvider.ShowMessage(StringConstants.ErrorTransferMoneyOnAccount);
                }
            }
        }

        public void ChooseAccountForDelete()
        {
            string getInfo = service.GetAccountsInfo(bank);

            if (getInfo == string.Empty)
            {
                consoleProvider.ShowMessage(StringConstants.HaveNotAccounts);
            }
            else
            {
                if (service.DeleteAccount(bank, consoleProvider.InputValue(getInfo)))
                {
                    consoleProvider.ShowMessage(StringConstants.Successfully);
                }
                else
                {
                    consoleProvider.ShowMessage(StringConstants.ErrorDeleteAccount);
                }
            }
        }

        public string InputBankName()
        {
            string name;

            while (true)
            {
                name = consoleProvider.InputStringValue();

                if (string.IsNullOrWhiteSpace(name))
                {
                    consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                }
                else
                {
                    break;
                }
            }

            return name;
        }
    }
}
