using Bank_Architecture_.Common.Enums;
using BankArchitecture.Bll.Bank.interfaces;
using BankArchitecture.Common;
using BankArchitecture.Common.Enums;
using BankArchitecture.Common.Models;
using BankArchitecture.Resources;
using System.Collections.Generic;

namespace BankArchitecture.Providers
{
    public class BankProvider : IBankProvider
    {
        private readonly IBankService service;
        private readonly ICreditAccountProvider creditAccountProvider;
        private readonly IConsoleProvider consoleProvider;
        private readonly IDebitAccountProvider debitAccountProvider;
        private MainBank bank;

        public BankProvider(IBankService service, IConsoleProvider consoleProvider, ICreditAccountProvider creditAccountProvider, IDebitAccountProvider debitAccountProvider)
        {
            this.service = service;
            this.creditAccountProvider = creditAccountProvider;
            this.consoleProvider = consoleProvider;
            this.debitAccountProvider = debitAccountProvider;
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
                            ChooseRecipientAccount(hasTransfer);
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

        private void ChooseRecipientAccount(object information)
        {
            consoleProvider.ShowMessage(service.GetAccountsInfo(bank));

            int choose = consoleProvider.InputValue(StringConstants.InputValue);

            if (choose >= 0 && choose <= bank.Accounts.Count - 1)
            {
                Dictionary<string, dynamic> transferInformation = (Dictionary<string, dynamic>)information;

                if (transferInformation[StringConstants.Recipient] == Recipient.Account)
                {
                    if (transferInformation[StringConstants.Sender] != bank.Accounts[choose])
                    {
                        if (transferInformation[StringConstants.Sender] as Account != null)
                        {
                            ((Account)transferInformation[StringConstants.Sender]).Balance -= transferInformation[StringConstants.Money];
                        }
                        else
                        {
                            ((Card)transferInformation[StringConstants.Sender]).Balance -= transferInformation[StringConstants.Money];
                        }

                        bank.Accounts[choose].Balance += transferInformation[StringConstants.Money];

                        consoleProvider.ShowMessage(StringConstants.Successfully);
                    }
                    else
                    {
                        consoleProvider.ShowMessage(StringConstants.SenderEqualsRecipient);
                    }
                }
                else
                {
                    if (bank.Accounts[choose] as CreditAccount != null)
                    {
                        creditAccountProvider.ChooseRecipientCard((CreditAccount)bank.Accounts[choose], transferInformation);
                    }
                    else
                    {
                        debitAccountProvider.ChooseRecipientCard((DebitAccount)bank.Accounts[choose], transferInformation);
                    }

                    if (transferInformation[StringConstants.Recipient] != null)
                    {
                        if (transferInformation[StringConstants.Recipient] != transferInformation[StringConstants.Sender])
                        {
                            if (transferInformation[StringConstants.Sender] as Account != null)
                            {
                                ((Account)transferInformation[StringConstants.Sender]).Balance -= transferInformation[StringConstants.Money];
                            }
                            else
                            {
                                ((Card)transferInformation[StringConstants.Sender]).Balance -= transferInformation[StringConstants.Money];
                            }

                            ((Card)transferInformation[StringConstants.Recipient]).Balance += transferInformation[StringConstants.Money];
                        }
                        else
                        {
                            consoleProvider.ShowMessage(StringConstants.SenderEqualsRecipient);
                        }
                    }
                    else
                    {
                        consoleProvider.ShowMessage(StringConstants.InputValue + "/" + StringConstants.HaveNotCards);
                    }
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
