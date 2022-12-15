namespace RockPaperScissor;

using System.IO;

internal class Program
{
    static readonly string[] theirMoves = { "A", "B", "C" };
    static readonly string[] ourMoves = { "X", "Y", "Z" };

    static void Main(string[] args)
    {
        var score = 0;
        var rounds = File.ReadLines("Input.txt");
        if (args?.Length > 0)
            score = rounds.Sum(r => { return GetScoreBasedOnOutcome(r); });
        else
            score = rounds.Sum(r => { return GetScoreBasedOnMove(r); });

        Console.WriteLine(score);
    }

    private static int GetScoreBasedOnOutcome(string round)
    {
        var moves = round.Split(' ');
        var them = Array.IndexOf(theirMoves, moves[0]);

        switch (moves[1])
        {
            case "X":
                return ((them + 2) % 3) + 1;
            case "Y":
                return them + 4;
            case "Z":
                return ((them + 1) % 3) + 7;
            default:
                Console.WriteLine("Invalid move " + moves[1]);
                return 0;
        }
    }

    private static int GetScoreBasedOnMove(string round)
    {
        var moves = round.Split(' ');
        var them = Array.IndexOf(theirMoves, moves[0]);
        var us = Array.IndexOf(ourMoves, moves[1]);

        var score = us + 1;        
        if(us == them)
            return score + 3;
            
        if (us == (them + 1) % 3)
            return score + 6;
        
        return score;
    }
}