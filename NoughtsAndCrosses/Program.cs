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
            public Dictionary<string, Square> positionMap = new Dictionary<string, Square>
            {
                {"A1", new Square(false, "A1", " ") }, {"A2", new Square(false, "A2", " ") }, {"A3", new Square(false, "A3", " ") },
                {"B1", new Square(false, "B1", " ") }, {"B2", new Square(false, "B2", " ") }, {"B3", new Square(false, "B3", " ") },
                {"C1", new Square(false, "C1", " ") }, {"C2", new Square(false, "C2", " ") }, {"C3", new Square(false, "C3", " ") }
            };

            public string turn = "X";

            public string printGrid()
            {
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
                turn = turn == "X" ? "O" : "X";
            }

            public string winChecker()
            {
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
                public bool used;
                public string position;
                public string character_value;

                public Square(bool used, string position, string character_value)
                {
                    this.used = used;
                    this.position = position;
                    this.character_value = character_value;
                }

                public bool updateSquare(string turn)
                {

                    if (!used)
                    {
                        this.used = true;
                        this.character_value = turn;
                        return true;
                    }
                    else
                    {
                        Console.Write("That space is already used try again: ");
                        return false;

                    }
                }

            }
        }
        static void Main(string[] args)
        {
            Console.Title = "Noughts And Crosses";

            Board playing = new Board();

            while (playing.winChecker() == " ")
            {
                Console.WriteLine(playing.printGrid());
                Console.Write($"You are {playing.turn}s please enter a position: ");
                while (!playing.positionMap[Console.ReadLine().ToUpper()].updateSquare(playing.turn)) ;
                playing.updateTurn(ref playing.turn);
            }
            Console.WriteLine(playing.printGrid());
            Console.WriteLine($"{playing.winChecker()}s Won the game!");

            Console.ReadLine();
        }
    }
}
