namespace BankArchitecture.Bll.Random.Implementations
{
    public static class CustomRandom
    {
        private const int AccountIdLength = 20;
        private const int CardIdLength = 16;
        private static System.Random random;

        static CustomRandom()
        {
            random = new System.Random();
        }

        public static string RandomAccountNumber()
        {
            char[] accessSymbols = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
                                        'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', };

            string id = string.Empty;

            for (var i = 0; i < AccountIdLength; i++)
            {
                id += accessSymbols[random.Next(0, accessSymbols.Length - 1)];
            }

            return id;
        }

        public static string RandomCardNumber()
        {
            char[] accessSymbols = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            string id = string.Empty;

            for (var i = 0; i < CardIdLength; i++)
            {
                id += accessSymbols[random.Next(0, accessSymbols.Length - 1)];
            }

            return id;
        }
    }
}
