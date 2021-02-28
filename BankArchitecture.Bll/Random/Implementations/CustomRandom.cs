namespace BankArchitecture.Bll.Random.Implementations
{
    public static class CustomRandom
    {
        private const int AccountIdLength = 20;
        private const int CardIdLength = 16;
        private static System.Random random;

        private const string AccessSymbolsForAccount = "1234567890abcdefghijklmnopqrstuvwxyz";
        private const string AccessSymbolsForCard = "1234567890";

        static CustomRandom()
        {
            random = new System.Random();
        }

        public static string RandomAccountNumber()
        {
            string id = string.Empty;

            for (var i = 0; i < AccountIdLength; i++)
            {
                id += AccessSymbolsForAccount[random.Next(0, AccessSymbolsForAccount.Length - 1)];
            }

            return id;
        }

        public static string RandomCardNumber()
        {
            string id = string.Empty;

            for (var i = 0; i < CardIdLength; i++)
            {
                id += AccessSymbolsForCard[random.Next(0, AccessSymbolsForCard.Length - 1)];
            }

            return id;
        }
    }
}
