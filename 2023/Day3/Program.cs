using System.Text.RegularExpressions;

namespace Day3;

internal class Program
{
    static readonly string _symbolRegex = @"([^\d|^.]+)";
    static readonly string _starSymbolRegex = @"([\*])";
    static readonly string _numberRegex = @"\d+";

    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines("input.txt");
        PartOne(input);
        PartTwo(input);
    }

    static void PartOne(string[] input)
    {
        int sum = 0;

        for (int i = 0; i < input.Length; i++)
        {
            MatchCollection numberMatches = Regex.Matches(input[i], _numberRegex);
            foreach (Match numberMatch in numberMatches)
            {
                int index = numberMatch.Index;
                int lefIndex = numberMatch.Index - 1;
                int rightIndex = numberMatch.Index + numberMatch.Length;
                (int x, int y) symbolPos;

                if (HasSymbolOnLeft(i, input, index, out symbolPos) ||
                    HasSymbolOnRight(i, input, rightIndex, out symbolPos) ||
                    HasSymbolAbove(i, input, lefIndex, rightIndex, out symbolPos) ||
                    HasSymbolBelow(i, input, lefIndex, rightIndex, out symbolPos))
                {
                    sum += int.Parse(numberMatch.Value);
                }
            }
        }

        Console.WriteLine($"Sum of all the engine parts: {sum}\n");
    }

    static void PartTwo(string[] input)
    {
        Dictionary<(int, int), (int, int)> ratios = InitRatios(input);

        for (int i = 0; i < input.Length; i++)
        {
            MatchCollection numberMatches = Regex.Matches(input[i], _numberRegex);
            foreach (Match numberMatch in numberMatches)
            {
                int index = numberMatch.Index;
                int lefIndex = numberMatch.Index - 1;
                int rightIndex = numberMatch.Index + numberMatch.Length;
                (int x, int y) symbolPos;

                if (HasSymbolOnLeft(i, input, index, out symbolPos, _starSymbolRegex) ||
                    HasSymbolOnRight(i, input, rightIndex, out symbolPos, _starSymbolRegex) ||
                    HasSymbolAbove(i, input, lefIndex, rightIndex, out symbolPos, _starSymbolRegex) ||
                    HasSymbolBelow(i, input, lefIndex, rightIndex, out symbolPos, _starSymbolRegex))
                {

                    ratios[symbolPos] = (ratios[symbolPos].Item1 * int.Parse(numberMatch.Value), ratios[symbolPos].Item2 + 1);
                }
            }
        }

        Console.WriteLine($"Sum of all the gear ratios: {ratios.Values.Where(x => x.Item2 == 2).Select(x => x.Item1).Sum()}");
    }

    static bool HasSymbolOnLeft(int i, string[] input, int index, out (int x, int y) symbolPos, string symbolRegex = @"([^\d|^.]+)")
    {
        symbolPos = (int.MaxValue, int.MaxValue);

        if (index <= 0) 
        {  
            return false;
        }

        string leftChar = input[i][index - 1].ToString();
        if (Regex.IsMatch(leftChar, symbolRegex))
        {
            symbolPos = (index - 1, i);
            return true;
        }

        return false;
    }

    static bool HasSymbolOnRight(int i, string[] input, int rightIndex, out (int x, int y) symbolPos, string symbolRegex = @"([^\d|^.]+)")
    {
        symbolPos = (int.MaxValue, int.MaxValue);

        if (rightIndex >= input[i].Length - 1)
        {
            return false;
        }

        string rightChar = input[i][rightIndex].ToString();
        if (Regex.IsMatch(rightChar, symbolRegex))
        {
            symbolPos = (rightIndex, i);
            return true;
        }
            
        return false;
    }

    static bool HasSymbolAbove(int i, string[] input, int leftIndex, int rightIndex, out (int x, int y) symbolPos, string symbolRegex = @"([^\d|^.]+)")
    {
        symbolPos = (int.MaxValue, int.MaxValue);

        if (i <= 0)
        {
            return false;
        }
        
        if (leftIndex < 0)
        {
            leftIndex = 0;
        }

        if (rightIndex > input[i-1].Length - 1)
        {
            rightIndex = input[i-1].Length - 1;
        }

        while (leftIndex <= rightIndex)
        {
            string charAbove = input[i-1][leftIndex].ToString();
            if (Regex.IsMatch(charAbove, symbolRegex))
            {
                symbolPos = (leftIndex, i-1);
                return true;
            }

            leftIndex++;
        }

        return false;
    }

    static bool HasSymbolBelow(int i, string[] input, int leftIndex, int rightIndex, out (int x, int y) symbolPos, string symbolRegex = @"([^\d|^.]+)")
    {
        symbolPos = (int.MaxValue, int.MaxValue);

        if (i >= input.Length - 1)
        {
            return false;
        }

        if (leftIndex < 0)
        {
            leftIndex = 0;
        }

        if (rightIndex > input[i+1].Length - 1)
        {
            rightIndex = input[i+1].Length - 1;
        }

        while (leftIndex <= rightIndex)
        {
            string charBelow = input[i+1][leftIndex].ToString();
            if (Regex.IsMatch(charBelow, symbolRegex))
            {
                symbolPos = (leftIndex, i+1);
                return true;
            }

            leftIndex++;
        }

        return false;
    }

    static Dictionary<(int, int), (int, int)> InitRatios(string[] input)
    {
        Dictionary<(int, int), (int, int)> ratios = new Dictionary<(int, int), (int, int)>();
        for (int i = 0; i < input.Length; i++)
        {
            MatchCollection starSymbolMatches = Regex.Matches(input[i], _starSymbolRegex);
            foreach (Match match in starSymbolMatches)
            {
                ratios.Add((match.Index, i), (1, 0));
            }
        }

        return ratios;
    }
}
