namespace Day2;

internal class Program
{
    static void Main(string[] args)
    {
        var games = ReadGamesFromFile("input.txt");
        PartOne(games);
        Console.ReadLine();
    }

    static void PartOne(List<Game> games)
    {
        int sum = 0;
        foreach (var game in games)
        {
            bool possilbe = game.IsPossible();
            if (possilbe)
            {
                sum += game.Id;
            }
        }
        Console.WriteLine($"Sum of the IDs of possible games: {sum}\n");
    }

    static List<Game> ReadGamesFromFile(string fileName)
    {
        var games = new List<Game>();

        using var fileStream = new StreamReader(fileName);
        while (!fileStream.EndOfStream)
        {
            string line = fileStream.ReadLine();
            string[] split = line.Split(':', ';');
            var game = new Game(split[0], split.Skip(1).ToArray());
            games.Add(game);
        }

        return games;
    }
}
