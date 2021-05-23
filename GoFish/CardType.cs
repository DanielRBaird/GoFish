namespace GoFish
{
    public enum CardType
    {
        Ace,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public static class CardTypeExtensions
    {
        public static string DisplayString(this CardType type)
        {
            return type switch
            {
                CardType.Ace => "A",
                CardType.One => "1",
                CardType.Two => "2",
                CardType.Three => "3",
                CardType.Four => "4",
                CardType.Five => "5",
                CardType.Six => "6",
                CardType.Seven => "7",
                CardType.Eight => "8",
                CardType.Nine => "9",
                CardType.Ten => "10",
                CardType.Jack => "J",
                CardType.Queen => "Q",
                CardType.King => "K",
                _ => throw new System.NotImplementedException(),
            };
        }
     }
}
