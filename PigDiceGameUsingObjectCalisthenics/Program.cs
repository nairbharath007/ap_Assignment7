using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace PigDiceGameUsingObjectCalisthenics
{
    internal class Program
    {
        private static int totalGames = 0;
        private static int totalAttempts = 0;
        private static int totalRolls = 0;
        private static double averageRolls = 0.0;
        private static string[] gameSummaries = new string[100];
        private static Random random = new Random();

        static void Main(string[] args)
        {
            DisplayWelcomeMessage();

            while (true)
            {
                PlayGame();

                Console.Write("\nDo you want to play again? (1 for yes/ any other key for no): ");
                char playAgain = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (playAgain != '1')
                {
                    averageRolls = (double)totalRolls / totalGames;
                    break;
                }
            }

            DisplayIndividualGames();
            DisplayGameSummary();
            Thread.Sleep(3000);
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("\t\t*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("\t\t*  Welcome to the Pig Dice Game!  *");
            Console.WriteLine("\t\t*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("GOAL: Score 20 points or more in fewer attempts.");
            Console.WriteLine("RULES:\nYou roll a die.\nIf you get:\n\t1: Forfeit all accumulated points in the current attempt and move on to the next attempt.\n\t   You retain your TOTAL SCORE, but your CURRENT TURN SCORE will be reset to zero.");
            Console.WriteLine("\t2-6: You can either:\n\t\t(i) Roll again\n\t\t(ii) Hold: Move points from your CURRENT TURN SCORE to your TOTAL SCORE. This counts as an attempt.");
            Console.WriteLine("\n--------------------------------------");
        }

        private static void PlayGame()
        {
            int totalScore = 0;
            int turnScore = 0;
            int attempts = 1;
            int numberOfRolls = 0;

            while (true)
            {
                DisplayGameState(attempts, turnScore, totalScore);

                char input = GetPlayerInput();
                Console.WriteLine();

                if (input == 'r')
                {
                    int rolledNumber = RollDie();
                    numberOfRolls++;

                    if (rolledNumber == 1)
                    {
                        HandleRolledOne(ref attempts, ref turnScore);
                    }
                    else if (totalScore + turnScore + rolledNumber >= 20)
                    {
                        HandleWin(totalScore, turnScore, rolledNumber, attempts, numberOfRolls);
                        break;
                    }
                    else
                    {
                        UpdateTurnScore(ref turnScore, rolledNumber);
                    }
                }
                else if (input == 'h')
                {
                    UpdateTotalScore(ref totalScore, ref turnScore);
                    attempts++;
                }
                else
                {
                    Console.WriteLine("Invalid key press.");
                }
            }

            UpdateStatistics(attempts, numberOfRolls);
        }

        private static char GetPlayerInput()
        {
            Console.Write("Enter 'r' to roll or 'h' to hold: ");
            return Console.ReadKey().KeyChar;
        }

        private static int RollDie()
        {
            int rolledNumber = random.Next(1, 7);
            Console.WriteLine($"You rolled a {rolledNumber}");
            return rolledNumber;
        }
        private static void DisplayGameState(int attempts, int turnScore, int totalScore)
        {
            Console.WriteLine($"\tAttempt: {attempts}");
            Console.WriteLine($"\tCurrent turn score: {turnScore}");
            Console.WriteLine($"\tTotal score: {totalScore}");
            Console.WriteLine("\n--------------------------------------");
        }

        private static void HandleRolledOne(ref int attempts, ref int turnScore)
        {
            Console.WriteLine("Rolled a 1, turn ends, no points earned.");
            Console.WriteLine("--------------------------------------");
            attempts++;
            turnScore = 0;
        }

        private static void HandleWin(int totalScore, int turnScore, int rolledNumber, int attempts, int numberOfRolls)
        {
            Console.WriteLine($"\tTotal score: {totalScore + turnScore + rolledNumber}");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($"\tCongratulations!\n\tYou reached 20 points or more in {attempts} attempts and {numberOfRolls} rolls.");
            Console.WriteLine("----------------------------------------------------------------");
        }

        private static void UpdateTurnScore(ref int turnScore, int rolledNumber)
        {
            turnScore += rolledNumber;
        }

        private static void UpdateTotalScore(ref int totalScore, ref int turnScore)
        {
            totalScore += turnScore;
            turnScore = 0;
        }

        private static void UpdateStatistics(int attempts, int numberOfRolls)
        {
            totalGames++;
            totalAttempts += attempts;
            totalRolls += numberOfRolls;

            gameSummaries[totalGames - 1] = $"Game {totalGames}: {attempts} attempts {numberOfRolls} rolls";
        }

        private static void DisplayIndividualGames()
        {
            Console.WriteLine("\n\tIndividual Game Details:");
            for (int i = 0; i < totalGames; i++)
            {
                Console.WriteLine($"\t {gameSummaries[i]}");
            }
        }

        private static void DisplayGameSummary()
        {
            Console.WriteLine("\n\t\t\tGame Summary:");
            Console.WriteLine($"\tTotal Games: {totalGames}");
            Console.WriteLine($"\tTotal Attempts: {totalAttempts}");
            Console.WriteLine($"\tTotal Rolls: {totalRolls}");
            Console.WriteLine($"\tAverage Rolls per Game: {averageRolls}");
        }
    }
}
