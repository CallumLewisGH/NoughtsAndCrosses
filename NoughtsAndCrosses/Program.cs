using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//This probably is not following SOLID I hardly understand what it means and i need to familiarise myself with classes more

namespace NoughtsAndCrosses
{
    internal class Program
    {
        class Board
        {

            //Defines the board as a Dictionary of Squares with the key being their position on the board
            public Dictionary<string, Square> positionMap = new Dictionary<string, Square>
            {
                {"A1", new Square(false, "A1", " ") }, {"A2", new Square(false, "A2", " ") }, {"A3", new Square(false, "A3", " ") },
                {"B1", new Square(false, "B1", " ") }, {"B2", new Square(false, "B2", " ") }, {"B3", new Square(false, "B3", " ") },
                {"C1", new Square(false, "C1", " ") }, {"C2", new Square(false, "C2", " ") }, {"C3", new Square(false, "C3", " ") }
            };

            //Sets the turn to start X by default
            public string turn = "X";

            public string printGrid()
            {
                //Returns the string which is used in the UI
                return  $"    1   2   3\n" +
                        $"A | {positionMap["A1"].character_value} | {positionMap["A2"].character_value} | {positionMap["A3"].character_value} |\n" +
                        $"    -   -   -\n" +
                        $"B | {positionMap["B1"].character_value} | {positionMap["B2"].character_value} | {positionMap["B3"].character_value} |\n" +
                        $"    -   -   -\n" +
                        $"C | {positionMap["C1"].character_value} | {positionMap["C2"].character_value} | {positionMap["C3"].character_value} |\n" +
                        $"    -   -   -";
            }
            public void updateTurn(ref string turn)
            {
                //Swaps the turn when called
                turn = turn == "X" ? "O" : "X";
            }

            public string winChecker()
            {
                //Creates 2D list of win conditions
                var winList = new List<List<string>>
                {
                    new List<string> { "A1", "A2", "A3" },
                    new List<string> { "B1", "B2", "B3" },
                    new List<string> { "C1", "C2", "C3" },
                    new List<string> { "A1", "B1", "C1" },
                    new List<string> { "A2", "B2", "C2" },
                    new List<string> { "A3", "B3", "C3" },
                    new List<string> { "A1", "B2", "C3" },
                    new List<string> { "A3", "B2", "C1" }
                };

                //Loops through and checks if the conditions are met for a win
                foreach (var winCondition in winList)
                {
                    if (winCondition.TrueForAll(position => positionMap[position].character_value == positionMap[winCondition[0]].character_value))
                    {
                        return positionMap[winCondition[0]].character_value;
                    }
                }
                return " ";
            }

            public class Square
            {
                public bool used; //Has a square been played in yet?
                public string position; //Position on the board a square is
                public string character_value; // Character either " ", "X" or "O"

                public Square(bool used, string position, string character_value)
                {
                    //initialises all values when a new instance is created
                    this.used = used;
                    this.position = position;
                    this.character_value = character_value;
                }

                public bool updateSquare(string turn)
                {
                    //Checks if the square position has been used already
                    //returns true if a position is not taken and false if it is taken

                    if (!used)
                    {
                        //Updates squares values
                        this.used = true;
                        this.character_value = turn;
                        return false;
                    }
                    else
                    {
                        //Error message
                        Console.Write("That space is already used try again: ");
                        return true;

                    }
                }

            }
        }
        static void Main(string[] args)
        {
            Console.Title = "Noughts And Crosses";

            //Initialises a new board

            Board playing = new Board();

            //Allows the game to repeat untill the win condition is met

            while (playing.winChecker() == " ")
            {
                Console.WriteLine(playing.printGrid());
                Console.Write($"You are {playing.turn}s please enter a position: ");

                //
                while (playing.positionMap[Console.ReadLine().ToUpper()].updateSquare(playing.turn)) ;
                playing.updateTurn(ref playing.turn);
            }
            Console.WriteLine(playing.printGrid());
            Console.WriteLine($"{playing.winChecker()}s Won the game!");

            Console.ReadLine();
        }
    }
}
