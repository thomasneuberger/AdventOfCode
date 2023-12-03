using FreeSpace;

Console.WriteLine("Load input data...");

var lines = (await File.ReadAllLinesAsync("input.txt"))
    .ToList();
    
var rootFolder = new Folder(string.Empty, null);

var currentFolder = rootFolder;
for (var i = 1; i < lines.Count; i++)
{
    if (lines[i] == "$ ls")
    {
        i++;
        while (i < lines.Count && !lines[i].StartsWith("$"))
        {
            var output = lines[i].Split(" ");
            if (output[0] != "dir")
            {
                currentFolder.Files[output[1]] = int.Parse(output[0]);
            }
            else if (!currentFolder.SubFolders.ContainsKey(output[1]))
            {
                currentFolder.SubFolders[output[1]] = new Folder(output[1], currentFolder);
            }

            i++;
        }

        i--;
    }
    else if (lines[i].StartsWith("$ cd"))
    {
        var input = lines[i].Split(" ");
        if (input[2] == "..")
        {
            currentFolder = currentFolder.Parent ?? throw new Exception("No parent folder");
        }
        else
        {
            if (!currentFolder.SubFolders.ContainsKey(input[2]))
            {
                throw new Exception("Subfolder not found.");
            }
            currentFolder = currentFolder.SubFolders[input[2]];
        }
    }
}

PrintFolders(null, rootFolder);

var sumBelow100k = SumBelow100k(rootFolder);

Console.WriteLine($"Sum of folder below 100000: {sumBelow100k}");

var spaceToFree = rootFolder.GetTotalSize() - 40_000_000;
Console.WriteLine($"Free up {spaceToFree} space...");

var smallestFolderToDelete = FindSubFoldersAbove(rootFolder, spaceToFree)
    .MinBy(f => f.GetTotalSize());

Console.WriteLine($"Delete folder {smallestFolderToDelete.Name} with size {smallestFolderToDelete.GetTotalSize()}");

IList<Folder> FindSubFoldersAbove(Folder folder, int size)
{
    var biggerFolders = new List<Folder>();

    if (folder.GetTotalSize() >= size)
    {
        biggerFolders.Add(folder);
    }

    foreach (var subFolder in folder.SubFolders.Values)
    {
        biggerFolders.AddRange(FindSubFoldersAbove(subFolder, size));
    }

    return biggerFolders;
}

void PrintFolders(string? parentPath, Folder folder)
{
    var folderPath = string.IsNullOrWhiteSpace(parentPath) ? folder.Name : parentPath + "/" + folder.Name;
    Console.WriteLine($"Folder /{folderPath}: {folder.GetTotalSize()}");

    foreach (var subFolder in folder.SubFolders.Values)
    {
        PrintFolders(folderPath + folder.Name, subFolder);
    }
}

int SumBelow100k(Folder folder)
{
    var folderTotalSize = folder.GetTotalSize();
    var sum = folderTotalSize <= 100_000 ? folderTotalSize : 0;
    sum += folder.SubFolders.Values.Sum(SumBelow100k);
    return sum;
}