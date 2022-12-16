namespace Rucksacks;

using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        var priorities = 0;
        var sacks = File.ReadLines("Input.txt");
        // if (args?.Length > 0)
        //     priorities = sacks.Sum(r => { return GetScoreBasedOnOutcome(r); });
        // else
            priorities = sacks.Sum(s => { return GetSackPriority(s); });

        Console.WriteLine(priorities);
    }

    private static int GetSackPriority(string sack)
    {
        var splitPoint = sack.Length/2;
        var comp1 = sack.Substring(0, splitPoint);
        var comp2 = sack.Substring(splitPoint);
        foreach(var item in comp1.Distinct())
        {
            if (comp2.Contains(item))
                return ItemPriorty(item);
        }
        return 0;
    }

    private static int ItemPriorty(char item)
    {
        var value = (int)item - 64;
        return value < 27 ? value : value - 6;
    }
}