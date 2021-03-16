using BankArchitecture.Common.Models;

namespace BankArchitecture.Providers.Interfaces
{
    public interface IDebitCardProvider
    {
        object ChooseAction(DebitCard account);
    }
}
