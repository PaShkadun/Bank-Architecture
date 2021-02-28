using BankArchitecture.Common;

namespace BankArchitecture.Providers
{
    public interface IDebitCardProvider
    {
        object ChooseAction(DebitCard account);
    }
}
