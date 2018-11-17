using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public class TwentyOneGame : Game, IWalkAway
    {
        public TwentyOneDealer Dealer { get; set; }

        public override void Play()
        {
            // instantiates new dealer in game
            Dealer = new TwentyOneDealer();
            foreach (Player player in Players)
            {
                player.Hand = new List<Card>();
                player.Stay = false;
            }
            // dealer properties
            Dealer.Hand = new List<Card>();
            Dealer.Stay = false;
            Dealer.Deck = new Deck();

            // asks each player to place a bet
            Console.WriteLine("Place your bet");

            // foreach loop adds each players bet to the pool
            foreach (Player player in Players)
            {
                int bet = Convert.ToInt32(Console.ReadLine());
                bool successFullyBet = player.Bet(bet);
                if (!successFullyBet) 
                {
                    return;
                }
                Bets[player] = bet;
            }
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Dealing");
                foreach (Player player in Player)
                {
                    Console.WriteLine("{0}: ", player.Name);
                    Dealer.Deal(player.Hand);

                    // logic check for blackjack
                    if (i == 1)
                    {
                        bool blackJack = TwentyOneRules.CheckForBlackJack(player.Hand);
                        if (blackJack)
                        {
                            // sends message player has 21
                            Console.WriteLine("Blackjack! {0} wins {1}", player.Name, Bets[player]);

                            // returns the players bet plus 1.5 times the winnings from other players bets
                            player.Balance += Convert.ToInt32((Bets[player] * 1.5 + Bets[player]));
                            //ends round
                            return;
                        }
                    }
                }
                Console.Write("Dealer: ");

                //checks dealers hand for blackjack
                Dealer.Deal(Dealer.Hand);
                if (i == 1)
                {
                    bool blackJack = TwentyOneRules.CheckForBlackJack(Dealer.Hand);
                    if (blackJack)
                    {
                        Console.WriteLine("Dealer has BlackJack! Everyone Loses!");
                        foreach (KeyValuePair<Player, int> entry in Bets)
                        {
                            Dealer.Balance += entry.Value;
                        }
                    }
                }
            }
            //shows the players cards when they choose to stay
            foreach (Player player in Players) 
            {
                Console.WriteLine("Your cards are: ");
                foreach (Card card in Player.Hand)
                {
                    Console.Write("{0} ", card.ToString());
                }

                // prompts user to hit or stay
                Console.WriteLine("\n\nHit or stay?");
                string answer = Console.ReadLine().ToLower();
                if (answer == "stay")
                {
                    player.Stay = true;
                    break;
                }
                else if (answer == "hit")
                {
                    Dealer.Deal(player.Hand);
                }

                // check if player has busted
                bool busted = TwentyOneRules.IsBusted(player.Hand);
                if (busted)
                {
                    Dealer.Balance += Bets[player];
                    Console.WriteLine("{0} Busted! You lose your bet of {1}. Your balance is new {2}.", player.Name, Bets[player], player.Balance);
                    Console.WriteLine("Do you want to play again?");
                    answer = Console.ReadLine().ToLower();
                    if (answer == "yes" || answer == "yeah")
                    {
                        player.isActivelyPlaying = true;
                    }
                    else 
                    {
                        player.isActivelyPlaying = false;
                    }
                }
            }
            // check if dealer has busted or is staying
            Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand);
            Dealer.Stay = TwentyOneRules

        }

        public override void ListPlayers()
        {
            Console.WriteLine("21 Players");
            base.ListPlayers();
        }
        public void WalkAway(Player player) 
        {
            throw new NotImplementedException();
        }
    }
}
