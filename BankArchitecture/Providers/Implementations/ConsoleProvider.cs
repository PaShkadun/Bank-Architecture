using BankArchitecture.Common.Enums;
using BankArchitecture.Resources;
using System;

namespace BankArchitecture.Providers
{
    public class ConsoleProvider : IConsoleProvider
    {
        public TypeOfAccount ChooseTypeOfObject()
        {
            int choose = InputValue(StringConstants.TypeOfObject);
        
            if (choose > (int)TypeOfAccount.Credit || choose < (int)TypeOfAccount.Debit)
            {
                ShowMessage(StringConstants.IncorrectInput);

                return TypeOfAccount.Incorrect;
            }
            else
            {
                return (TypeOfAccount)choose;
            }
        }

        public string InputStringValue()
        {
            return Console.ReadLine();
        }

        public int InputValue(string message)
        {
            ShowMessage(message);

            int value;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
            }
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void WaitingPressAnyKey()
        {
            Console.WriteLine(StringConstants.PressAnyKey);
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
