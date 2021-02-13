using BankArchitecture.Common;

namespace BankArchitecture.Providers
{
    public interface IDebitCardProvider
    {
        public void ChooseAction(DebitCard account);
    }
}
