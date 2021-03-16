using BankArchitecture.Common.Enums;

namespace BankArchitecture.Providers.Interfaces
{
    public interface IConsoleProvider
    {
        void ShowMessage(string message);

        int InputValue(string message);

        string InputStringValue();

        TypeOfAccount ChooseTypeOfObject();

        void WaitingPressAnyKey();
    }
}