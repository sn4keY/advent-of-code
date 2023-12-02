using System.Text.RegularExpressions;

namespace Day2;

internal class Game
{
    public int Id { get; init; }

    public ICollection<Set> Sets { get; init; } = new List<Set>();

    public Game(string idString, string[] setsString)
    {
        Id = int.Parse(Regex.Match(idString, @"\d").Value);
        foreach (var setString in setsString)
        {
            Sets.Add(new Set(setString));
        }
    }
}
