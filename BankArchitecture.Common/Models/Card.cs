namespace BankArchitecture.Common
{
    public abstract class Card
    {
        public int Balance { get; set; }

        public string Id { get; set; }

        public Card(int money)
        {
            Balance = money;
        }

        public Card()
        {

        }
    }
}
