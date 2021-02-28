namespace BankArchitecture.Providers
{
    public interface IBankProvider
    {
        string InputBankName();

        void ChooseAccountForDelete();

        void ChooseAccountForTransfer();

        void Actions();

        void Start(int choose);
    }
}
