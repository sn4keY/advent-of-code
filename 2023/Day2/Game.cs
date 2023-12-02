using System.Text.RegularExpressions;

namespace Day2;

internal class Game
{
    public int Id { get; init; }

    public ICollection<Set> Sets { get; init; } = new List<Set>();

    public Game(string idString, string[] setsString)
    {
        Id = int.Parse(Regex.Match(idString, @"\d+").Value);
        foreach (var setString in setsString)
        {
            Sets.Add(new Set(setString));
        }
    }

    public bool IsPossible()
    {
        // 12 red cubes max
        bool tooManyRedsInASet = Sets.Any(x => x.NumberOfCubesByColor(Color.Red) > 12);
        if (tooManyRedsInASet)
        {
            return false;
        }

        // 13 green cubes max
        bool tooManyGreensInASet = Sets.Any(x => x.NumberOfCubesByColor(Color.Green) > 13);
        if (tooManyGreensInASet)
        {
            return false;
        }

        // 14 blue cubes max
        bool tooManyBluesInASet = Sets.Any(x => x.NumberOfCubesByColor(Color.Blue) > 14);
        if (tooManyBluesInASet)
        {
            return false;
        }

        return true;
    }

    public (int red, int green, int blue) MinimumNumberOfCubes()
    {
        int red = 0;
        int green = 0;
        int blue = 0;

        foreach (var set in Sets)
        {
            var minimumNumberOfCubesInASet = set.MinimumNumberOfCubes();
            if (minimumNumberOfCubesInASet.red > red)
            { 
                red = minimumNumberOfCubesInASet.red;
            }
            if (minimumNumberOfCubesInASet.green > green)
            {
                green = minimumNumberOfCubesInASet.green;
            }
            if (minimumNumberOfCubesInASet.blue > blue)
            {
                blue = minimumNumberOfCubesInASet.blue;
            }
        }

        return (red, green, blue);
    }
}
