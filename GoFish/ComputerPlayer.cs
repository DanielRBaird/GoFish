using System.Collections.Generic;
using System.Linq;

namespace GoFish
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer() : base(false)
        {
        }

        /// <summary>
        /// Uses current card data, and past player guesses to determine a move to make.
        /// </summary>
        public override CardType GetTypeToAsk(List<Guess> unmatchedGuesses)
        {
            var myGuesses = unmatchedGuesses
                .Where(x => x.Asker == this)
                .Select(x => x.Type)
                .Where(x => Hand.Contains(x));

            // See if there is a card someone has asked for that, that we have, and we also haven't asked for it yet.
            // First, get all guesses made by other people that are for a relavant type, and are not successful.
            var thingsWeCouldGuess = unmatchedGuesses
                .Where(x => Hand.Contains(x.Type) && x.Asker != this)
                .Select(x => x.Type)
                .Except(myGuesses);

            CardType bestGuess;

            // We don't have an idea of what to guess based on previous guesses
            if (thingsWeCouldGuess.Any())
            {
                bestGuess = thingsWeCouldGuess.First();
            }
            else
            {
                // Ask for something we haven't asked for if possible.
                var myGuesesHashSet = myGuesses.ToHashSet();
                var unguessedThings = Hand.Where(x => !myGuesesHashSet.Contains(x));
                if (unguessedThings.Any())
                {
                    bestGuess = unguessedThings.First();
                }
                else
                {
                    // If we don't have anything we haven't guessed, then just start over with the thing we haven't asked for
                    // in the longest time.
                    bestGuess = myGuesses.First();
                }
            }

            return bestGuess;
        }
    }
}
