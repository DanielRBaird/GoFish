using System;
using System.Collections.Generic;
using System.Linq;
using GoFish.Utils;

namespace GoFish
{
    public class HumanPlayer : Player
    {
        public HumanPlayer() : base(true)
        {
        }

        /// <summary>
        /// Used to check that a player can ask for a given card.
        /// The player must have a card in their hand the corresponds to the given input.
        /// </summary>
        private bool IsValidInput(string input)
        {
            return Hand.Any(type => string.Compare(type.DisplayString(), input, StringComparison.OrdinalIgnoreCase) == 0);
        }

        private CardType GetTypeInHand(string input)
        {
            return Hand.First(type => string.Compare(type.DisplayString(), input, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public override CardType GetTypeToAsk(List<Guess> unmatchedGuesses)
        {
            var input = Terminal.AskString("Ask other player, do you have any...", validate: IsValidInput);

            // We know this type is valid here because the ask method validates it.
            return GetTypeInHand(input);
        }
    }
}
