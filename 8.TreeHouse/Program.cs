namespace TreeHouse;

using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        var rows = File.ReadAllLines("Input.txt").ToArray();
        var grid = BuildGrid(rows);
        
        var total = 0;
        if (args?.Length > 0)
             total = MaxScenicScore(grid);
         else
            total = CountVisible(grid);

        Console.WriteLine(total);
    }

    static int[,] BuildGrid(string[] rows)
    {
        //Build a two dimensional array to work with
        var grid = new int[rows.Length,rows.Length];
        for(var y = 0; y < rows.Length; y++)
        {
            for (var x = 0; x < rows.Length; x++)
                grid[x,y] = int.Parse(rows.Skip(y).First()[x].ToString());
        }
        return grid;
    }

    static int CountVisible(int[,] grid)
    {
        var size = grid.GetLength(0);
        var visible = 0;        
        var hidden = new List<Tuple<int, int>>();

        //Ignoring the permeter, count the ones that can be seen from east/west         
        //and note the coordinates of any that can't be seen
        for(var i = 1; i < size - 1; i++)
        {
            for(var j = 1; j < size - 1; j++)
            {
                var tree = grid[j,i];
                //If it's a 0, anything else will be same or taller, so skip altogether
                if (tree == 0)
                    continue;

                //Assume it's seen to start
                var seen = true;
                for(var k = 0; k < j; k++)
                {
                    //Any to the east same height or taller?
                    if (tree <= grid[k,i])
                    {
                        seen = false;
                        break;
                    }
                }

                if (seen)
                {
                    visible++;
                    continue;
                }

                //How about the west?
                seen = true;
                for(var k = j + 1; k < size; k++)
                {
                    if (tree <= grid[k,i])
                    {
                        seen = false;
                        break;
                    }
                }

                if (seen)
                    visible++;
                else
                    hidden.Add(new Tuple<int, int>(j,i));
            }
        }

        //Of the ones that are hidden east/west, we'll now check north/south
        foreach(var hider in hidden)
        {
            var tree = grid[hider.Item1, hider.Item2];
            var seen = true;
            for(var i = 0; i < hider.Item2; i++)
            {
                if (tree <= grid[hider.Item1, i])
                {
                    seen = false; 
                    break;
                }
            }
            if (seen)
            {
                visible++;
                continue;
            }

            seen = true;
            for(var i = hider.Item2 + 1; i < size; i++)
            {
                if (tree <= grid[hider.Item1, i])
                {
                    seen = false; 
                    break;
                }
            }
            if (seen)
                visible++;
        }

        //Throw in all the ones on the perimeter
        return visible + 4 * (size - 1);
    }

    static int MaxScenicScore(int[,] grid)
    {
        var maxScore = 0;
        var size = grid.GetLength(0);
        for(int x = 1; x < size - 1; x++)
        {
            //We can skip trees along the perimeter; one of their distances will be 0, meaning a score of 0
            for(int y = 1; y < size - 1; y++)
            {
                var tree = grid[x,y];

                //Guaranteed to only see one tree in each direction; not gonna be max
                if (tree == 0)
                    continue;

                var east = 0;
                for(int e = x - 1; e > -1; e--)
                {
                    east++;
                    if (grid[e,y] >= tree)
                        break;
                }
                
                var west = 0;
                for(int w = x + 1; w < size; w++)
                {
                    west++;
                    if (grid[w,y] >= tree)
                        break;
                }
                
                var north = 0;
                for(int n = y - 1; n > -1; n--)
                {
                    north++;
                    if(grid[x,n] >= tree)
                        break;
                }
                
                var south = 0;
                for(int s = y + 1; s < size; s++)
                {
                    south++;
                    if(grid[x,s] >= tree)
                        break;
                }

                var score = east * west * north * south;
                if (score > maxScore)
                    maxScore = score;
            }
        }
        return maxScore;
    }
}