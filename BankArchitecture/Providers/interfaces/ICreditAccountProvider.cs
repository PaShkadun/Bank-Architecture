using BankArchitecture.Common;
using System.Collections.Generic;

namespace BankArchitecture.Providers
{
    public interface ICreditAccountProvider
    {
        object ChooseAction(CreditAccount account);

        void ChooseRecipientCard(CreditAccount account, Dictionary<string, dynamic> information);
    }
}
