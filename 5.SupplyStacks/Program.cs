namespace SupplyStacks;

using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        var stacksAndMoves = File.ReadLines("Input.txt");

        // if (args?.Length > 0)
        // {
        //     count = pairs.Where(p => VersionTwo(p.Split(','))).Count();
        // }
        // else
        {
            var tops = VersionOne(stacksAndMoves);
            Console.WriteLine(tops);
        }
    }

    static string VersionOne(IEnumerable<string> inputs)
    {
        int breakPoint = 0;
        foreach(var line in inputs)
        {
            breakPoint++;
            if (line.Trim().Length == 0)
                break;
        }

        var stacks = GetStacks(inputs.Take(breakPoint - 2));
        
        foreach(var move in inputs.Skip(breakPoint))
            MakeMove(stacks, move);

        return string.Join("", stacks.Select(s => s.Pop()));
    }

    static int VersionTwo(IEnumerable<string> inputs)
    {
        return 0;
    }

    static Stack<string>[] GetStacks(IEnumerable<string> inputs)
    {        
        //We'll normalize each line with a blank at the end so it's consistent across
        var stackCount = (inputs.First() + " ").Length/4;
        var stacks = new List<string>[stackCount];
        for(int i = 0; i< stackCount; i++)
            stacks[i] = new List<string>();

        foreach(var line in inputs)
        {
            var queue = new Queue<char>(line + " ");
            var stackIndex = 0;
            while(queue.Count > 0)
            {
                queue.Dequeue(); //[
                var item = queue.Dequeue().ToString(); //Box Name
                if (!string.IsNullOrWhiteSpace(item))
                    stacks[stackIndex].Add(item);
                queue.Dequeue(); //]
                queue.Dequeue(); //Blank separator
                stackIndex++;
            }
        }
        return stacks.Select(q => { 
                q.Reverse(); 
                return new Stack<string>(q);
            }).ToArray();
    }

    static void MakeMove(Stack<string>[] stacks, string move)
    {
        var moveDetails = move.Split(' ').ToArray();
        var amount = int.Parse(moveDetails[1]);
        var fromIndex = int.Parse(moveDetails[3]) - 1;
        var toIndex = int.Parse(moveDetails[5]) - 1;
        for(int i = 0; i < amount; i++)
        {
            stacks[toIndex].Push(stacks[fromIndex].Pop());
        }    
    }
}