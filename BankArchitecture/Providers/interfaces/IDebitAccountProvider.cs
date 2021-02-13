using BankArchitecture.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankArchitecture.Providers
{
    public interface IDebitAccountProvider
    {
        public void ChooseAction(DebitAccount account);
    }
}
