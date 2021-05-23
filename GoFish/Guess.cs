using System;
namespace GoFish
{
    /// <summary>
    /// Used to record guesses made.
    /// </summary>
    public record Guess(CardType Type, Player Askee, Player Asker, bool Result);
}
