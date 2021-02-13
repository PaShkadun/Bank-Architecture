using BankArchitecture.Bll.Bank.interfaces;
using BankArchitecture.Common;
using BankArchitecture.Common.Enums;
using BankArchitecture.Common.Models;
using BankArchitecture.Resources;

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

                    case BankFunctions.ActionsWithCards:
                        int chooseAccount = consoleProvider.InputValue(service.GetAccountsInfo(bank));

                        if (bank.Accounts.Count < chooseAccount || chooseAccount < 0)
                        {
                            consoleProvider.ShowMessage(StringConstants.IncorrectInput);
                        }
                        else if (bank.Accounts[chooseAccount] as CreditAccount != null)
                        {
                            creditAccountProvider.ChooseAction((CreditAccount)bank.Accounts[chooseAccount]);
                        }
                        else
                        {
                            debitAccountProvider.ChooseAction((DebitAccount)bank.Accounts[chooseAccount]);
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
