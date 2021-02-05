namespace BankArchitecture.Resources
{
    public static class StringConstans
    {
        public const string Welcome = "Welcome to The Bank.  Please, write name of Bank.";
        public const string IncorrectInput = "Incorrect";
        public const string ChooseBankAction =  "1. Add Account\n2. Delete Account\n3. Get info about accounts\n4. Show Balance" +
                                                "\n5. Add Money To Balance\n6. Transfer money to account";
        public const string InputName = "Input name";
        public const string InputValue = "Input value";
        public const string TypeOfObject = "Type of object: \n1. Debit\n2. Credit";
        public const string Successfully = "Successfully";
        public const string Error = "Error";
        public const string HaveNotAccounts = "You haven't accounts";
        public const string ErrorDeleteAccount = "Account isn't exists or account have card(s) or credit(s)";
        public const string ChooseAccountForTransferMoney = "Choose account for transfer money";
        public const string ErrorTransferMoneyOnAccount = "Account isn't exists or sum biggest bank balance";
        public const string MainActions = "1. Start New\n2. Upload";
    }
}
