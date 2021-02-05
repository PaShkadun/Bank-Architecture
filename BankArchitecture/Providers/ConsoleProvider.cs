using BankArchitecture.Bll.Bank.interfaces;
using BankArchitecture.Common.Enums;
using BankArchitecture.Common.Models;
using BankArchitecture.Resources;
using System;

namespace BankArchitecture.Providers
{
    public class ConsoleProvider : IConsoleProvider
    {
        public TypeOfObject ChooseTypeOfObject()
        {
            int choose = InputValue(StringConstans.TypeOfObject);
        
            if(choose > (int)TypeOfObject.Credit || choose < (int)TypeOfObject.Debit)
            {
                ShowMessage(StringConstans.IncorrectInput);

                return TypeOfObject.Incorrect;
            }
            else
            {
                return (TypeOfObject)choose;
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
    }
}
