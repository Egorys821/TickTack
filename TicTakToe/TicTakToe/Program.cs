using System.Numerics;
using TicTakToe;

namespace TicTakToe
{

    internal class Program
    {
        public static void UpdateDisplay(string s)
        {
            Console.Clear();
            Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            Player Adam = new Player("Adam", FieldType.X, "Hurray!");
            Player Beth = new Player("Betheny", FieldType.O, "I am better");


            int[] ints;
            ints = new int[1000];
            ints[50] = 1;
            ints[100] = 2;

            List<int> intlist = new List<int>();

            intlist.Add(5);

            


            //Game game = new Game(Adam, Beth, 2, 3, false,false,3);
            Game game = new GameBuilder()
                .WithPlayer(Adam)
                .WithPlayer(Beth)
                .WithInitialFieldSize(5)
                .WithWinLength(3)
                .WithFixedFieldSize()
                .Build();


            while (!game.IsGameDone)
            {

                UpdateDisplay(game.GetGameState());


                while (true)
                {
                    Console.Write(game.CurrentPlayer.Name + " Turn: ");
                    string? turn = Console.ReadLine();
                    Coordinate coordinate;
                    try
                    {
                        coordinate = new Coordinate(turn);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Invalid Coordinate, syntax is 'x,y' example: 5,3");
                        continue;
                    }

                    if (game.TryMakeMove(coordinate))
                    {
                        WinCondition wc = game.GetGameReport().WinCondition;
                        if (wc != WinCondition.UNKNOWN)
                        {
                            UpdateDisplay(game.GetGameState());
                            Console.WriteLine(wc);

                            return;
                        }

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move!, try again");
                    }
                }



                game.ChangePlayers();

            }

        }

    }
}
