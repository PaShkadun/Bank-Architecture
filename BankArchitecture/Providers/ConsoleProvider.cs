using System;

namespace BankArchitecture.Providers
{
    public class ConsoleProvider : IConsoleProvider
    {
        public string InputStringValue()
        {
            return Console.ReadLine();
        }

        public int InputValue(string message)
        {
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
