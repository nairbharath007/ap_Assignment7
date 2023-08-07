using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PigDiceGameDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("\t\t*  Welcome to the Pig Dice Game!  *");
            Console.WriteLine("\t\t*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine(" GOAL : Score 20 points or more in less attempts.");
            Console.WriteLine(" Rules : \n You roll a die. \n If you get: \n\t 1 : Forfeit all the points accumulated in the current attempt and move on to the next attempt.\n\t    Here you retain your TOTAL SCORE but your CURRENT TURN SCORE will be resetted to zero.");
            Console.WriteLine("\t 2-6 : You can either:");
            Console.WriteLine("\t\t (i) Roll again ");
            Console.WriteLine("\t\t (ii) Hold : All the points accumulated by player in their CURRENT TURN SCORE\n\t\t\t\twill be added to their TOTAL SCORE.\n\t\t\t\tIt will then be counted as an attempt.");
            Console.WriteLine("\n--------------------------------------");

            int totalGames = 0;
            int totalAttempts = 0;
            int totalRolls = 0;
            double averageRolls = 0.0;
            string[] gameSummaries = new string[100];


            while (true)
            {
                int totalScore = 0;
                int turnScore = 0;
                int attempts = 1;
                int numberOfRolls = 0;
                Random random = new Random();

                while (true)
                {
                    Console.WriteLine($"\tAttempt: {attempts}");
                    Console.WriteLine($"\tCurrent turn score: {turnScore}");
                    Console.WriteLine($"\tTotal score: {totalScore}");
                    Console.WriteLine("\n--------------------------------------");

                    Console.Write("Enter 'r' to roll or 'h' to hold: ");

                    char input = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    if (input == 'r')
                    {
                        int rolledNumber = random.Next(1, 7);
                        Console.WriteLine($"You rolled a {rolledNumber}");
                        numberOfRolls++;

                        if (rolledNumber == 1)
                        {
                            Console.WriteLine("Rolled a 1, turn ends, no points earned.");
                            Console.WriteLine("--------------------------------------");
                            attempts++;
                            turnScore = 0;
                            //break;
                        }
                        else if (totalScore + turnScore + rolledNumber >= 20)
                        {
                            Console.WriteLine($"\tTotal score: {totalScore + turnScore + rolledNumber}");
                            Console.WriteLine("----------------------------------------------------------------");
                            Console.WriteLine($"\tCongratulations! \n\tYou reached 20 points or more in {attempts} attempts and {numberOfRolls} rolls.");
                            Console.WriteLine("----------------------------------------------------------------");
                            break;
                        }
                        else
                        {
                            turnScore += rolledNumber;
                        }
                    }
                    else if (input == 'h')
                    {
                        totalScore += turnScore;
                        attempts++;
                        turnScore = 0;

                        /*if (totalScore >= 20)
                        {
                            Console.WriteLine($"\tTotal score: {totalScore}");
                            Console.WriteLine($"Congratulations! You reached 20 points or more in {attempts} attempts and {numberOfRolls} rolls.");
                            break;
                        }*/
                    }
                    else
                    {
                        Console.WriteLine("Invalid Key press.");
                    }
                }

                totalGames++;
                totalAttempts += attempts;
                totalRolls += numberOfRolls;

                gameSummaries[totalGames - 1] = $"Game {totalGames}: {attempts} attempts {numberOfRolls} rolls";

                Console.Write("\nDo you want to play again? (1 for yes/ any other key for no): ");
                char playAgain = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (playAgain != '1')
                {
                    averageRolls = (double)totalRolls / totalGames;
                    break;
                }
            }

            Console.WriteLine("\n\tIndividual Game Details:");
            for (int i = 0; i < totalGames; i++)
            {
                Console.WriteLine($"\t {gameSummaries[i]}");
            }

            Console.WriteLine("\n\t\t\tGame Summary:");
            Console.WriteLine($"\tTotal Games: {totalGames}");
            Console.WriteLine($"\tTotal Attempts: {totalAttempts}");
            Console.WriteLine($"\tTotal Rolls: {totalRolls}");
            Console.WriteLine($"\tAverage Rolls per Game: {averageRolls}");
            Thread.Sleep(3000);
        }
    }
}



