using System;

namespace BankArchitecture.Notifiers
{
    public class ConsoleNotifier : INotifier
    {
        public void Notify(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
        }
    }
}
