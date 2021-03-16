using BankArchitecture.Common.Models;
using System.Collections.Generic;

namespace BankArchitecture.Providers.Interfaces
{
    public interface ICreditAccountProvider
    {
        object ChooseAction(CreditAccount account);
    }
}
