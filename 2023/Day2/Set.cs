using System.Text.RegularExpressions;

namespace Day2;

internal class Set
{
    private readonly string _colorRegexPattern = @"(green)|(red)|(blue)";

    public ICollection<Cube> Cubes { get; init; } = new List<Cube>();

    public Set(string cubesString)
    {
        string[] cubes = cubesString.Split(',');
        foreach (var cube in cubes)
        {
            int numberOfCubes = int.Parse(Regex.Match(cube, @"\d+").Value);
            for (int i = 0; i < numberOfCubes; i++)
            {
                var match = Regex.Match(cube, _colorRegexPattern);
                Color color = Enum.Parse<Color>(match.Value, true);
                Cubes.Add(new Cube(color));
            }
        }
    }

    public int NumberOfCubesByColor(Color color)
    {
        return Cubes.Where(x => x.Color == color).Count();
    }

    public (int red, int green, int blue) MinimumNumberOfCubes()
    {
        return (0, 0, 0);
    }
}

