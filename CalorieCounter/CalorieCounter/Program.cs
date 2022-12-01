namespace CalorieCounter;

internal class Program
{
    static void Main()
    {
        TopThree();
    }

    private static void JustMax()
    {
        long maxCalories = 0;
        long currentCalories = 0;
        int elf = 1;
        foreach (var calorieItem in CalorieInput.CalorieList.Split(Environment.NewLine))
        {
            if (string.IsNullOrEmpty(calorieItem))
            {
                maxCalories = Math.Max(maxCalories, currentCalories);
                currentCalories = 0;
                elf++;
                continue;
            }

            currentCalories += long.Parse(calorieItem);
        }

        Console.WriteLine("Elf " + elf + " is carrying " + maxCalories + " calories.");
    }

    private static void TopThree()
    {
        var perElfCounts = new List<long>();
        long currentCalories = 0;
        foreach (var calorieItem in CalorieInput.CalorieList.Split(Environment.NewLine))
        {
            if (string.IsNullOrEmpty(calorieItem))
            {
                perElfCounts.Add(currentCalories);
                currentCalories = 0;
                continue;
            }

            currentCalories += long.Parse(calorieItem);
        }

        perElfCounts.Sort();
        perElfCounts.Reverse();
        Console.WriteLine("Top 3 elves are carrying " + perElfCounts.Take(3).Sum() + " calories.");
    }
}