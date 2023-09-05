// See https://aka.ms/new-console-template for more information
using Schneider.Minefield;

internal class Program
{
    private static MinefieldCore minefieldGame = new MinefieldCore();

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Console.WriteLine("Keys are:  Up, Down, Left, Right, x to exit");

        minefieldGame.CurrentMinefield.PlantMines(20);
        Console.Write(minefieldGame.CurrentMinefield.ShowMap());

        var inProgress = true;
        while (inProgress)
        {
            var keyPressed = Console.ReadKey(true).Key;

            switch (keyPressed)
            {
                case ConsoleKey.UpArrow:
                    inProgress = ProcessOutcome(minefieldGame.Move(Direction.Up));
                    //Console.WriteLine("UP");
                    break;

                case ConsoleKey.DownArrow:
                    inProgress = ProcessOutcome(minefieldGame.Move(Direction.Down));
                    //Console.WriteLine("down");
                    break;

                case ConsoleKey.LeftArrow:
                    inProgress = ProcessOutcome(minefieldGame.Move(Direction.Left));
                    //Console.WriteLine("left");
                    break;

                case ConsoleKey.RightArrow:
                    inProgress = ProcessOutcome(minefieldGame.Move(Direction.Right));
                    //Console.WriteLine("right");
                    break;

                case ConsoleKey.X:
                    Console.WriteLine("Exiting ...");
                    inProgress = false;
                    break;

                default:
                    Console.WriteLine("Wrong Key try again");
                    break;
            }
            DisplayScore(minefieldGame.Score, minefieldGame.CurrentPosition, minefieldGame.NumberOfLives);
        }
    }

    private static bool ProcessOutcome(MoveOutcome outcome)
    {
        switch (outcome)
        {
            case MoveOutcome.MineExplodes:
                Console.WriteLine("Kaboom! Mine explodes");
                break;

            case MoveOutcome.NoMine:
                Console.WriteLine("No Mine");
                break;

            case MoveOutcome.Completed:
                Console.WriteLine("------------------------\n YOU WIN\n------------------------\n");
                Console.WriteLine($"Score: {minefieldGame.Score}");

                return false;

            case MoveOutcome.NotAllowed:
                Console.WriteLine("You can not move in that direction");
                break;

            case MoveOutcome.ExplodedMine:
                Console.WriteLine("You are on an exploded mine");
                break;

            default:
                break;
        }

        if (minefieldGame.NumberOfLives <= 0)
        {
            Console.WriteLine("------------------------\n GAME OVER\n------------------------\n");
            return false;
        }
        return true;
    }

    private static void DisplayScore(int score, Coordinate currentPosition, int lives)
    {
        Console.WriteLine($"CurrentScore: {score}, Position: {currentPosition}, Lives: {lives}");
    }
}