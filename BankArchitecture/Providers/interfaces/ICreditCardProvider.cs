using BankArchitecture.Common;

namespace BankArchitecture.Providers
{
    public interface ICreditCardProvider
    {
        public void ChooseAction(CreditCard card);
    }
}
