namespace FreeSpace;

public class Folder
{
    internal Folder(string name, Folder? parent)
    {
        Name = name;
        Parent = parent;
    }

    public string Name { get; set; }
    public Folder? Parent { get; set; }

    public IDictionary<string, Folder> SubFolders { get; set; } = new Dictionary<string, Folder>();
    public IDictionary<string, int> Files { get; set; } = new Dictionary<string, int>();

    public int GetTotalSize()
    {
        return Files.Values.Sum() + SubFolders.Values.Select(f => f.GetTotalSize()).Sum();
    }
}