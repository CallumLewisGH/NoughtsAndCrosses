using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


//Recreation following solid principles

namespace NoughtsAndCrosses
{
    internal class ProgramCopy
    {
        class Valid
        {
            public static bool InputXO(out string input)
            {
                //Takes user input for turn
                Console.Write("Enter the character for the first player (O or X): ");
                input = Console.ReadLine().ToUpper();

                //Validates input
                Console.WriteLine(input == "O" || input == "X" ? $"{input}s has been selected" : "Input has to be a \"X\" or a \"O\"");
                return input == "O" || input == "X";
            }

            public static bool InputPosition(out string input, Dictionary<string, Square> positionMap, string turn)
            {
                //Takes user input for position
                Console.Write($"You are {turn}s please enter a position: ");
                input = Console.ReadLine().ToUpper();

                //Validates input
                Console.WriteLine(positionMap.ContainsKey(input) ? string.Empty : "Position is invalid");
                return positionMap.ContainsKey(input);
            }
        }
        class Board
        {
            //Defines the board as a Dictionary of Squares with the key being their position on the board
            public Dictionary<string, Square> positionMap;

            public Board()
            {
                positionMap = new Dictionary<string, Square>
                {
                    {"A1", new Square(false, "A1", " ") }, {"A2", new Square(false, "A2", " ") }, {"A3", new Square(false, "A3", " ") },
                    {"B1", new Square(false, "B1", " ") }, {"B2", new Square(false, "B2", " ") }, {"B3", new Square(false, "B3", " ") },
                    {"C1", new Square(false, "C1", " ") }, {"C2", new Square(false, "C2", " ") }, {"C3", new Square(false, "C3", " ") }
                };
            }
            public string PrintGrid()
            {
                //Returns string to print to UI
                return $"    1   2   3\n" +
                        $"A | {positionMap["A1"].character_value} | {positionMap["A2"].character_value} | {positionMap["A3"].character_value} |\n" +
                        $"    -   -   -\n" +
                        $"B | {positionMap["B1"].character_value} | {positionMap["B2"].character_value} | {positionMap["B3"].character_value} |\n" +
                        $"    -   -   -\n" +
                        $"C | {positionMap["C1"].character_value} | {positionMap["C2"].character_value} | {positionMap["C3"].character_value} |\n" +
                        $"    -   -   -";
            }
        }

        class Game
        {
            private Board board; //Initialises a board for the game to take place on
            private string turn; //X or O
            private string position; // Position on the board for a piece to go on
            public Game()
            {
                //Initailises a new instance of board for this particular game
                board = new Board();
                
                //Asks user who to go first untill X or O
                while (!Valid.InputXO(out turn));
            }

            public void play()
            {
                //While the game isn't won
                while (winChecker(board.positionMap)== " ")
                {
                    //Prints board
                    Console.WriteLine(board.PrintGrid());

                    //Asks user for valid input position
                    while (!(Valid.InputPosition(out position, board.positionMap, turn) && board.positionMap[position].updateSquare(turn)));

                    //Swaps turn
                    if (winChecker(board.positionMap) == " ")
                        { updateTurn(ref turn); }

                }
                //Prints board
                Console.WriteLine(board.PrintGrid());
                //Prints Win message
                Console.WriteLine($"{turn}s Won the game! Congratulations!");
            }

            public void updateTurn(ref string turn)
            {
                //Swaps the turn when called
                turn = turn == "X" ? "O" : "X";
            }

            public string winChecker(Dictionary<string, Square> positionMap)
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
        }


        class Square
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
                    return true;
                }
                else
                {
                    //Error message
                    Console.WriteLine("That space is already used try again: ");
                    return false;

                }
            }
        }
        static void Main(string[] args)
        {
            Console.Title = "Noughts And Crosses";

            //Creates a new instance of Game and runs the function play
            Game game = new Game();
            game.play();

            Console.ReadLine(); 
        }
    }
}
