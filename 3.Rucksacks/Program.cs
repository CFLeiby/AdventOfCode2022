namespace Rucksacks;

using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        var priorities = 0;
        var sacks = File.ReadLines("Input.txt");
        if (args?.Length > 0)
        {
            var lastStart = 0;
            var totalSacks = sacks.Count();
            while(lastStart < totalSacks)
            {
                try
                {
                    priorities += GetBadgePriority(sacks.Skip(lastStart).Take(3));
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
                lastStart += 3;
            }
        }
        else
            priorities = sacks.Sum(s => { return GetSackPriority(s); });

        Console.WriteLine(priorities);
    }

    private static int GetBadgePriority(IEnumerable<string> sacks)
    {
        var sackItems = sacks.Select(s => s.Distinct()).ToArray();
        foreach(var item in sackItems[0])
        {
            if (sackItems[1].Contains(item) && sackItems[2].Contains(item))
            {
                return ItemPriorty(item);
            }
        }
        return 0;
    }

    private static int GetSackPriority(string sack)
    {
        var halfSack = sack.Length/2;
        var comp1 = sack.Substring(0, halfSack).Distinct();
        var comp2 = sack.Substring(halfSack).Distinct();
        foreach(var item in comp1)
        {
            if (comp2.Contains(item))
                return ItemPriorty(item);
        }
        return 0;
    }

    private static int ItemPriorty(char item)
    {        
        var value = (int)item;
        return value < 91 ? value - 38: value - 96;
    }
}