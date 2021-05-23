using System;
using System.Collections.Generic;
using System.Linq;
using GoFish.Utils;

namespace GoFish
{
    public class Game
    {
        private const int CardsPerPlayer = 8;
        private readonly List<Player> players = new();
        private Deck deck;

        // A history of public information, ie, info that all players could know.
        // Used to give some limited intelligence to the computer player.
        private List<Guess> unmatchedGuesses = new();

        public Game()
        {
            // Note: In the future we could expand this to allow for more players.
            // for now we are going to just hard code it to 1 human and 1 computer player.
            players.Add(new HumanPlayer());
            players.Add(new ComputerPlayer());
        }

        public void Start()
        {
            deck = Deck.FullDeck();
            deck.Shuffle();

            Deal(CardsPerPlayer);
            GameLoop();

            // We won't get here until the game ends.
            DeclareWinner();
        }

        private void DeclareWinner()
        {
            int maxScore = players.Max(x => x.Score);
            var winners = players.Where(x => x.Score == maxScore);

            if (winners.Count() == 1)
            {
                var winnerType = winners.First().Human ? "Human" : "Computer";
                Terminal.TypeLine($"{winnerType} player wins with {maxScore} points!");
            }
            else
            {
                Terminal.TypeLine($"It's a tie with {maxScore} points!");
            }
        }

        private void GameLoop()
        {
            while (true)
            {
                foreach (var player in players)
                {
                    TakeTurn(player);
                    if (CheckForEnd())
                    {
                        return;
                    }
                }
            }
        }

        private bool CheckForEnd()
        {
            return players.Any(x => x.Hand.Count == 0);
        }

        private void TakeTurn(Player player)
        {
            if (player.Human)
            {
                PrintCards(player.Hand);
            }
            else
            {
                Terminal.TypeLine("The computer player is thinking...");
            }

            var type = player.GetTypeToAsk(unmatchedGuesses);

            if (!player.Human)
            {
                Terminal.TypeLine($"Do you have any... {type}'s?");
            }

            // For simplicity we aren't asking which player right now, just the first player that isn't ours. Which
            // works since we are only using two players.
            var playerToAsk = players.First(x => x != player);

            var success = playerToAsk.RemoveFromHand(type);

            if (success)
            {
                Terminal.TypeLine(player.Human ?
                    "Success! The other player did have that card!" :
                    "Yes, you do. Computer player scores!"
                );

                player.AddToHand(type);
            }
            else
            {
                GoFish(player);
            }


            AddGuess(player, playerToAsk, type, success);
        }

        private void GoFish(Player player)
        {
            Terminal.TypeLine("Go fish!");
            var card = deck.Draw();
            if (card != null)
            {
                player.AddToHand(card.Type);

                if (player.Human)
                {
                    Terminal.TypeLine($"Drew a {card.Type.DisplayString()}");
                }
            }
        }

        private void AddGuess(Player asker, Player askee, CardType type, bool result)
        {
            var guess = new Guess(type, askee, asker, result);
            if (result)
            {
                // Remove all where the same player was asking and failing to get the same card.
                unmatchedGuesses.RemoveAll(x => x.Askee == askee && x.Result == false && x.Type == type);
            }
            else
            {
                unmatchedGuesses.Add(guess);
            }
        }

        /// <summary>
        /// Give players their initial starting cards.
        /// </summary>
        private void Deal(int cardsPerPlayer)
        {
            // Draw all the cards in the deck.
            for (int i = 0; i < cardsPerPlayer; i++)
            {
                foreach (var player in players)
                {
                    var card = deck.Draw();
                    if (card != null)
                    {
                        player.AddToHand(card.Type);
                    }
                }
            }
        }

        private static void PrintCards(IEnumerable<CardType> cards)
        {
            Terminal.TypeLine("Cards: ");
            foreach (var card in cards)
            {
                Terminal.Type($"{card.DisplayString()} ");
            }

            Console.WriteLine("");
        }
    }
}