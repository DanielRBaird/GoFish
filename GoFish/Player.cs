using System;
using System.Collections.Generic;

namespace GoFish
{
    public abstract class Player
    {
        /// <summary>
        /// The unique identifier for the player. Mostly useful if we start
        /// allowing more players.
        /// </summary>
        public readonly Guid Id = Guid.NewGuid();

        /// <summary>
        /// Indicates if this is a human or computer player.
        /// Defaults to true for this class. Subclass to change.
        /// </summary>
        public bool Human { get; protected set; }

        /// <summary>
        /// Cards currently in the players hand.
        /// This is made of only card types because we don't care about the suit.
        /// This is a set because in go fish, a player takes any duplicates
        /// and converts them to points immediately.
        /// </summary>
        public HashSet<CardType> Hand { get; private set; } = new();

        /// <summary>
        /// Keeps track of how many points the player has.
        /// </summary>
        public int Score { get; private set; }

        public Player(bool human)
        {
            Human = human;
        }

        /// <summary>
        /// Adds the given card to the players hand.
        /// </summary>
        public void AddToHand(CardType card)
        {
            var added = Hand.Add(card);

            if (!added)
            {
                // If the card wasn't added then we already had the same one there.
                // Remove the existing one and add a point.
                Hand.Remove(card);
                Score++;
            }
        }

        /// <summary>
        /// Removes the card from the players hand if it was there.
        /// </summary>
        /// <returns>True if a card was removed. False if not.</returns>
        public bool RemoveFromHand(CardType card)
        {
            return Hand.Remove(card);
        }

        public abstract CardType GetTypeToAsk(List<Guess> unmatchedGuesses);
    }
}
