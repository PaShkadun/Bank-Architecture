using BankArchitecture.Common.Models;
using System.Collections.Generic;

namespace BankArchitecture.Providers.Interfaces
{
    public interface IDebitAccountProvider
    {
        object ChooseAction(DebitAccount account);
    }
}
