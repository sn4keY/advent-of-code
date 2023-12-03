using System.Text.RegularExpressions;

namespace Day3;

internal class Program
{
    static readonly string _symbolRegex = @"([^\d|^.]+)";
    static readonly string _numberRegex = @"\d+";

    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines("input.txt");
        int sum = 0;

        for (int i = 0; i < input.Length; i++)
        {
            MatchCollection numberMatches = Regex.Matches(input[i], _numberRegex);
            foreach (Match numberMatch in numberMatches)
            {
                int index = numberMatch.Index;
                int lefIndex = numberMatch.Index - 1;
                int rightIndex = numberMatch.Index + numberMatch.Length;

                if (HasSymbolOnLeft(i, input, index) ||
                    HasSymbolOnRight(i, input, rightIndex) ||
                    HasSymbolAbove(i, input, lefIndex, rightIndex) ||
                    HasSymbolBelow(i, input, lefIndex, rightIndex))
                {
                    sum += int.Parse(numberMatch.Value);
                    continue;
                }
            }
        }

        Console.WriteLine(sum);

        /*
        for (int i = 0; i < input.Length; i++)
        {
            MatchCollection symbolMatches = Regex.Matches(input[i], _symbolRegex);

            if (symbolMatches.Count == 0)
            {
                continue;
            }

            foreach (Match match in symbolMatches)
            {
                int index = match.Index;

                if (HasNumberOnLeft(i, input, index))
                {
                    // TODO search num on left
                }

                if (HasNumberOnRight(i, input, index))
                {
                    // TODO search num on right
                }

                if (HasNumberAbove(i, input, index))
                {
                    // TODO search num above
                }

                if (HasNumberBelow(i, input, index))
                {
                    // TODO search num below
                }
            }
        }
        */
    }

    static bool HasSymbolOnLeft(int i, string[] input, int index)
    {
        return index > 0 && input[i][index - 1] != '.';
    }

    static bool HasSymbolOnRight(int i, string[] input, int rightIndex)
    {
        return rightIndex < input[i].Length - 1 && input[i][rightIndex] != '.';
    }

    static bool HasSymbolAbove(int i, string[] input, int leftIndex, int rightIndex)
    {
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
            if (input[i-1][leftIndex] != '.')
            {
                return true;
            }

            leftIndex++;
        }

        return false;
    }

    static bool HasSymbolBelow(int i, string[] input, int leftIndex, int rightIndex)
    {
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
            if (input[i+1][leftIndex] != '.')
            {
                return true;
            }

            leftIndex++;
        }

        return false;
    }

    static string[] GetSymbols(string[] input)
    {
        string concattedInput = string.Concat(input);
        MatchCollection allSymbols = Regex.Matches(concattedInput, _symbolRegex);
        var symbols = new List<string>();
        foreach (Match symbol in allSymbols)
        {
            if (symbols.Contains(symbol.Value))
            {
                continue;
            }

            symbols.Add(symbol.Value);
        }
        return symbols.ToArray();
    }
}
