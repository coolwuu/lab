namespace Lab.Entities
{
    public class Card
    {
        public CardKind Kind { get; set; }
        public int Point { get; set; }
    }

    public class Account
    {
        public int Saving { get; set; }
        public string Name { get; set; }
    }
}