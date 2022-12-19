namespace NoSpace;

using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        var cmds = File.ReadAllLines("Input.txt");
        var directory = GetDirectory(cmds);

        var total = 0;
        if (args?.Length > 0)
            total = FindDelete(directory);
        else
            total = GetTotalSize(directory);

        Console.WriteLine(total);
    }

    static Folder GetDirectory(IEnumerable<string> cmds)
    {
        var top = new Folder("/");
        if (cmds?.Any() != true)
            return top;
        
        var currentBranch = new Stack<Folder>();
        currentBranch.Push(top);

        foreach(var cmd in cmds.Skip(1))
        {
            var parts = cmd.Split(' ');
            switch (parts[0])
            {
                case "$":
                    if (parts[1] == "cd")
                    {
                        if (parts[2] == "..")
                            _ = currentBranch.Pop();
                        else
                        {
                            var nextFolder = (Folder)currentBranch.Peek().Children.First(c => c.Name == parts[2]);
                            currentBranch.Push(nextFolder);
                        }
                    }
                    break;
                case "dir":
                    currentBranch.Peek().Children.Add(new Folder(parts[1]));
                    break;
                default:
                    currentBranch.Peek().Children.Add(new Item(parts[1], int.Parse(parts[0])));
                    break;
            }
        }

        return top;
    }

    static int GetTotalSize(Folder top)
    {
        var toInclude = new List<Folder>();
        var toCheck = new Queue<Folder>(top.Children.Where(c => c is Folder).Cast<Folder>());
        while(toCheck.Count > 0)
        {
            var folder = toCheck.Dequeue();
            if (folder.Size <= 100000)
                toInclude.Add(folder);
            
            foreach(var child in folder.Children.Where(c => c is Folder))
                toCheck.Enqueue((Folder)child);
        }
        return toInclude.Sum(i => i.Size);
    }

    static int FindDelete(Folder top)
    {
        var available = 70000000 - top.Size;
        var needed = 30000000 - available;

        var potentials = new List<Folder>();
        var toCheck = new Queue<Folder>(top.Children.Where(c => c is Folder).Cast<Folder>());
        while(toCheck.Count > 0)
        {
            var folder = toCheck.Dequeue();            
            if (folder.Size < needed)
                continue;
                
            potentials.Add(folder);            
            foreach(var child in folder.Children.Where(c => c is Folder))
                toCheck.Enqueue((Folder)child);
        }

        return potentials.Min(p => p.Size);
    }

    private class Item
    {
        public Item(string name, int size) 
        {
            Name = name;
            Size = size;
        }

        public string Name { get; private set; }
        public virtual int Size {get; private set; }
        public override string ToString() => Name;
    }
    private class Folder : Item
    {
        public Folder(string name)
            : base(name, 0)
        {
            Children = new List<Item>();
        }
        public List<Item> Children {get; private set;}
        public override int Size => Children.Sum(c => c.Size);
    }
}