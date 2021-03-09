using BankArchitecture.Common.Models;

namespace BankArchitecture.Providers.Interfaces
{
    public interface IAccountProvider
    {
        object ChooseRecipientCard(Account account);
    }
}
