namespace Tuning;

using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        var data = File.ReadAllText("Input.txt");

        var markerSize = args?.Length > 0 ? 14 : 4;
        var marker = GetFirstMarkerPosition(data, markerSize);
        Console.WriteLine(marker);
    }

    static int GetFirstMarkerPosition(string data, int markerSize)
    {
        if ((data?.Length??0) < markerSize)
            return 0;
        
        int position = markerSize;
        var candidate = new Queue<char>(data.Substring(0,markerSize));
        do 
        {
            if (candidate.Distinct().Count() == markerSize)
                return position;
            
            candidate.Dequeue();
            candidate.Enqueue(data[position]);
            position++;
        }while (position < data.Length);
        
        return 0;
    }
}