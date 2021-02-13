using BankArchitecture.Common.Enums;

namespace BankArchitecture.Providers
{
    public interface IConsoleProvider
    {
        public void ShowMessage(string message);

        public int InputValue(string message);

        public string InputStringValue();

        public TypeOfAccount ChooseTypeOfObject();

        public void WaitingPressAnyKey();
    }
}