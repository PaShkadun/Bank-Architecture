using BankArchitecture.Common.Models;

namespace BankArchitecture.Providers.Interfaces
{
    public interface ICreditCardProvider
    {
        object ChooseAction(CreditCard card);
    }
}
