using System;
using System.Collections.Generic;
using System.Text;

namespace BankArchitecture.Notifiers
{
    public interface INotifier
    {
        void Notify(string message);
    }
}
