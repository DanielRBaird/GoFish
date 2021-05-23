using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFish
{
    public class Deck
    {
        public static Deck FullDeck() => new(FullCardDeck().ToList());

        public List<Card> Cards { get; private set; }

        public Deck(List<Card> cards)
        {
            Cards = cards;
        }

        /// <summary>
        /// Returns the top card on the deck (first), or null if the deck is empty
        /// </summary>
        public Card Draw()
        {
            if (Cards.Count > 0)
            {
                var card = Cards.First();
                Cards.RemoveAt(0);
                return card;
            }

            return null;
        }

        public void Shuffle()
        {
            Random rnd = new();
            Cards = Cards.OrderBy(c => rnd.Next()).ToList();
        }

        private static IEnumerable<Card> FullCardSuit(Suit suit)
        {
            return Enum.GetValues<CardType>().Select(type => new Card(suit, type));
        }

        private static IEnumerable<Card> FullCardDeck()
        {
            return Enum.GetValues<Suit>().Select(suit => FullCardSuit(suit)).SelectMany(x => x);
        }
    }
}
