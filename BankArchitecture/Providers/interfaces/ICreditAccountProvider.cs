using BankArchitecture.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankArchitecture.Providers
{
    public interface ICreditAccountProvider
    {
        public void ChooseAction(CreditAccount account);
    }
}
