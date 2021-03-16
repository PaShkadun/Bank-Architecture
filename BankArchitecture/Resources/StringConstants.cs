namespace BankArchitecture.Resources
{
    public static class StringConstants
    {
        public const string InputBankName = "Input bank name";
        public const string Welcome = "Welcome to The Bank.  Please, write name of Bank.";
        public const string IncorrectInput = "Incorrect";
        public const string ChooseActionOfBank =  "1. Add Account\n2. Delete Account\n3. Get info about accounts\n4. Show Balance" +
                                                "\n5. Add Money To Balance\n6. Transfer money to account\n7. Choose account for actions\n8. Serialize";
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
        public const string DebitAccountActions = "1. Transfer money to account\n2. Transfer money to card\n3. Spend money\n4. Show balance\n5. Add card\n6. Delete card" +
                                                    "\n7. Show cards list\n8. Choose card for operation\n0. Go back";
        public const string HaveNotCards = "You haven't cards in this account";
        public const string CreditAccountActions = "1. Transfer money to account\n2. Transfer money to card\n3. Pay credit\n4. Add credit\n5. Show credit list\n6. Spend money" +
                                                    "\n7. Show balance\n8. Add card\n9. Delete card\n10. Show card list\n11. ChooseCard\n0. Exit";
        public const string InputCreditSum = "Input credit sum";
        public const string InputCreditMonthes = "Input credit monthes";
        public const string PayCreditError = "You haven't money/credit isn't exists/credit paid";
        public const string DeleteCreditCardError = "You have credits or debt or card isn't exist";
        public const string TransferMoneyError = "You have credit(s) or debt/sender == recepient/haven't money";
        public const string HaveNotMoney = "You haven't money or sum is negative";
        public const string HaveNotCredit = "You haven't credits or haven't debt of credits";
        public const string CreditCardActions = "1. Add credit\n2. Pay credit\n3. Show credits\n4. Spend money\n5. Show balance\n6. Transfer money to account\n7. Transfer money to card\n0. Exit";
        public const string AddCreditError = "You have debt of credits or incorrect sum/count of monthes";
        public const string DebitCardActions = "1. Spend money\n2. Show balance\n3. Transfer money to account\n4. Transfer money to card\n0. Exit";
        public const string PressAnyKey = "Press any key to continue";
        public const string Recipient = "recipient";
        public const string Money = "money";
        public const string Sender = "sender";
        public const string SenderEqualsRecipient = "Error. Sender == Recipient";
    }
}
