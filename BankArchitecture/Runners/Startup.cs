using BankArchitecture.Common.Enums;
using BankArchitecture.Common.Models;
using BankArchitecture.Providers;
using BankArchitecture.Resources;

namespace BankArchitecture.Runners
{
    public static class Startup
    {
        public static void Start()
        {
            ConsoleProvider consoleProvider = new ConsoleProvider();

            consoleProvider.ShowMessage(StringConstans.Welcome);

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
                    StaticBank.Name = name;
                    break;
                }
            }

            while (true)
            {
                switch ((BankFunctions)consoleProvider.InputValue(StringConstans.ChooseBankAction))
                {
                    case BankFunctions.AddAccount:
                        break;

                    case BankFunctions.AddMoneyToBalance:
                        break;

                    case BankFunctions.DeleteAccount:
                        
                        break;

                    case BankFunctions.ShowBalance:
                        consoleProvider.ShowMessage(StaticBank.Balance.ToString());
                        break;

                    case BankFunctions.RenamedBank:

                        consoleProvider.ShowMessage(StringConstans.InputName);

                        while (true)
                        {
                            name = consoleProvider.InputStringValue();

                            if (string.IsNullOrEmpty(name))
                            {
                                consoleProvider.ShowMessage(StringConstans.IncorrectInput);
                            }
                            else
                            {
                                StaticBank.Name = name;
                                break;
                            }
                        }

                        StaticBank.Name = name;
                        break;

                    default:
                        consoleProvider.ShowMessage(StringConstans.IncorrectInput);
                        break;
                }
            }
        }
    }
}
