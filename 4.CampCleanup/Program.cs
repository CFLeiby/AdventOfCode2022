namespace CampCleanup;

using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        var count = 0;
        var pairs = File.ReadLines("Input.txt");
        if (args?.Length > 0)
            count = pairs.Where(p => Overlap(p.Split(','))).Count();
        else
            count = pairs.Where(p => FullContainment(p.Split(','))).Count();
        Console.WriteLine(count);
    }
    private static bool Overlap(IEnumerable<string> assignments)
    {
        var rangeA = assignments.First().Split('-').Select(x => int.Parse(x)).ToArray();
        var rangeB = assignments.Last().Split('-').Select(x => int.Parse(x)).ToArray();
        
        //A & B start on the same section 
        if (rangeA[0] == rangeB[0])
            return true;
        
        //A starts before the start of B
        if (rangeA[0] < rangeB[0]) 
            return rangeA[1] >= rangeB[0];

        //A starts after the start of B
        if (rangeA[0] > rangeB[0])
            return rangeA[0] <= rangeB[1];

        return false;
    }
    private static bool FullContainment(IEnumerable<string> assignments)
    {
        var rangeA = assignments.First().Split('-').Select(x => int.Parse(x)).ToArray();
        var rangeB = assignments.Last().Split('-').Select(x => int.Parse(x)).ToArray();
        
        if (rangeA[0] < rangeB[0])
            return rangeB[1] <= rangeA[1];
        else if (rangeA[0] > rangeB[0])
            return rangeB[1] >= rangeA[1];
        else
            return true;
    }
}