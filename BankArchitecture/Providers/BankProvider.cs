using BankArchitecture.Bll.Bank.interfaces;
using BankArchitecture.Common.Enums;
using BankArchitecture.Common.Models;
using BankArchitecture.Resources;
using System;

namespace BankArchitecture.Providers
{
    public class BankProvider : IBankProvider
    {
        private readonly IBankService service;
        //private readonly IAccountProvider accountProvider;
        private readonly IConsoleProvider consoleProvider;
        private MainBank bank;

        public BankProvider(IBankService service, IConsoleProvider consoleProvider)
        {
            this.service = service;
            //this.accountProvider = accountProvider;
            this.consoleProvider = consoleProvider;
        }

        public void Start(int choose)
        {
            switch(choose)
            {
                case 1: 
                    //bank = new MainBank();
                    break;

                // Тут будет создание MainBank с помощью JSON-сериализации
                case 2:
                    consoleProvider.ShowMessage("Don't work yet");
                    break;

                default:
                    consoleProvider.ShowMessage(StringConstans.IncorrectInput);
                    return;
            }

            Actions();
        }

        public void Actions()
        {
            int choose = -1;

            while (choose != 0)
            {
                choose = consoleProvider.InputValue(StringConstans.ChooseBankAction);

                switch ((BankFunctions)choose)
                {
                    case BankFunctions.AddAccount:
                        service.AddAccount(consoleProvider.ChooseTypeOfObject());
                        consoleProvider.ShowMessage(StringConstans.Successfully);

                        break;

                    case BankFunctions.DeleteAccount:
                        ChooseAccountForDelete();

                        break;

                    case BankFunctions.GetAccountsInfo:
                        string getInfo = service.GetAccountsInfo();

                        if (getInfo == string.Empty)
                        {
                            consoleProvider.ShowMessage(StringConstans.HaveNotAccounts);
                        }
                        else
                        {
                            consoleProvider.ShowMessage(getInfo);
                        }

                        break;

                    case BankFunctions.ShowBalance:
                        consoleProvider.ShowMessage(MainBank.Balance.ToString());

                        break;

                    case BankFunctions.AddMoneyToBalance:
                        MainBank.Balance += consoleProvider.InputValue(StringConstans.InputValue);

                        consoleProvider.ShowMessage(StringConstans.Successfully);

                        break;

                    case BankFunctions.TransferMoneyToAccount:
                        ChooseAccountForTransfer();

                        break;

                    default:
                        consoleProvider.ShowMessage(StringConstans.IncorrectInput);
                        break;
                };

                Console.ReadKey(true);

                Console.Clear();
            }

            return;
        }

        public void ChooseAccountForTransfer()
        {
            string getInfo = service.GetAccountsInfo();

            if (getInfo == string.Empty)
            {
                consoleProvider.ShowMessage(StringConstans.ErrorTransferMoneyOnAccount);
            }
            else
            {
                consoleProvider.ShowMessage(getInfo);

                int chooseAccount = consoleProvider.InputValue(StringConstans.ChooseAccountForTransferMoney);
                int chooseSum = consoleProvider.InputValue(StringConstans.InputValue);

                if (service.TransferMoneyToAccount(chooseAccount, chooseSum))
                {
                    consoleProvider.ShowMessage(StringConstans.Successfully);
                }
                else
                {
                    consoleProvider.ShowMessage(StringConstans.ErrorTransferMoneyOnAccount);
                }
            }
        }

        public void ChooseAccountForDelete()
        {
            string getInfo = service.GetAccountsInfo();

            if (getInfo == string.Empty)
            {
                consoleProvider.ShowMessage(StringConstans.HaveNotAccounts);
            }
            else
            {
                if (service.DeleteAccount(consoleProvider.InputValue(getInfo)))
                {
                    consoleProvider.ShowMessage(StringConstans.Successfully);
                }
                else
                {
                    consoleProvider.ShowMessage(StringConstans.ErrorDeleteAccount);
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
                    consoleProvider.ShowMessage(StringConstans.IncorrectInput);
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
