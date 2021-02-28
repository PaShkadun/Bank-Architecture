using BankArchitecture.Common;

namespace BankArchitecture.Providers
{
    public interface ICreditCardProvider
    {
        object ChooseAction(CreditCard card);
    }
}
