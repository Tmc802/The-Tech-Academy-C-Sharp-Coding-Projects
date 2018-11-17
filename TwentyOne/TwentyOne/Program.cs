using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    class Program
    {
        static void Main(string[] args)
        {
            // greet the user and prompt for their name
            Console.WriteLine("Welcome to the Grand Hotel and Casino. Let's start by telling me your name.");
            string playerName = Console.ReadLine();

            // inquire how much money the user will be playing with
            Console.WriteLine("And how much money did you bring today?");
            int bank = Convert.ToInt32(Console.ReadLine());

            // prompt the user if they'd like to start a new game
            Console.WriteLine("Hello, {0}. Would you like to join a game of 21 right now?", playerName);
            string answer = Console.ReadLine().ToLower();
            if (answer == "Yes" || answer == "yeah" || answer == "y" || answer == "ya") {

                // istantiate the players information with the user entries above
                Player player = new Player(playerName, bank);
                Game game = new TwentyOneGame();
                game += player;
                player.isActivelyPlaying = true;

                // while loop to know if the player is playing the game
                while (player.isActivelyPlaying && player.Balance > 0)
                {
                    game.Play();
                }
                game -= player;
                Console.WriteLine("Thank you for playing!");
            }
            //end game and exit while loop prompt
            Console.WriteLine("Feel free to look around the casino, Bye for now.");
            Console.ReadLine();
        }
    }
}




