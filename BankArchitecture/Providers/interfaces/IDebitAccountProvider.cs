using BankArchitecture.Common;
using System.Collections.Generic;

namespace BankArchitecture.Providers
{
    public interface IDebitAccountProvider
    {
        object ChooseAction(DebitAccount account);

        void ChooseRecipientCard(DebitAccount account, Dictionary<string, dynamic> information);
    }
}
