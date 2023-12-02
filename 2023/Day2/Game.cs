namespace Day2;

internal class Game
{
    public int Id { get; init; }

    public IEnumerable<Set> Sets { get; init; } = Enumerable.Empty<Set>();
}
