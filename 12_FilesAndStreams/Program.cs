// See https://aka.ms/new-console-template for more information

using _12_FilesAndStreams;
using System.IO;
using System.Text;

SyncDirectoriesOnStart.SyncDir();



using var watcher = new FileSystemWatcher(@"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\Dir_1\");
using var watcher1 = new FileSystemWatcher(@"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\Dir_2\");

watcher.NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size;

watcher1.NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size;

watcher.Changed += OnChanged;
watcher.Created += OnCreated;
watcher.Deleted += OnDeleted;
watcher.Renamed += OnRenamed;
watcher.Error += OnError;

watcher.Filter = "*.txt";
watcher.IncludeSubdirectories = true;
watcher.EnableRaisingEvents = true;



watcher1.Changed += OnChanged1;
watcher1.Created += OnCreated1;
watcher1.Deleted += OnDeleted1;
watcher1.Renamed += OnRenamed1;
watcher1.Error += OnError1;

watcher1.Filter = "*.txt";
watcher1.IncludeSubdirectories = true;
watcher1.EnableRaisingEvents = true;

Console.WriteLine("Press enter to exit.");
Console.ReadLine();


static void OnChanged(object sender, FileSystemEventArgs e)
{

    if (e.ChangeType != WatcherChangeTypes.Changed)
    {
        return;
    }
    /*
    var pathE = Utility.pathB1 + e.Name;
    Console.WriteLine("path is {0}", pathE);
    if (System.IO.File.Exists(pathE))
    {
        System.IO.File.Delete(pathE);
    }

    if (!System.IO.File.Exists(pathE))
    {
        System.IO.File.Copy(e.FullPath, pathE);
    }
    Console.WriteLine($"Changed: {e.FullPath}");
    */
    SyncDirectoriesOnStart.SyncDir();
}

static void OnCreated(object sender, FileSystemEventArgs e)
{
    var pathE = Utility.pathB1 + e.Name;
    if (System.IO.File.Exists(pathE))
    {
        System.IO.File.Delete(pathE);
    }

    if (!System.IO.File.Exists(pathE))
    {
        System.IO.File.Copy(e.FullPath, pathE);
    }

    string value = $"Created: {e.FullPath}";
    Console.WriteLine(value);
}

static void OnDeleted(object sender, FileSystemEventArgs e)
{
    var pathE = Utility.pathB1 + e.Name;
    System.IO.File.Delete(pathE);
    Console.WriteLine($"Deleted: {e.FullPath}");
}


static void OnRenamed(object sender, RenamedEventArgs e)
{
  //  FileCompare myFileCompare = new FileCompare();
  //  var pathE = Utility.pathB1 + e.Name;
  //  if (System.IO.File.Exists(pathE))
  //  {
   //     System.IO.File.Delete(pathE);
  //  }

   // if (!System.IO.File.Exists(pathE))
   // {
   //     System.IO.File.Copy(e.FullPath, pathE);
   // }
    SyncDirectoriesOnStart.SyncDir();
    Console.WriteLine($"Renamed:");
    Console.WriteLine($"    Old: {e.OldFullPath}");
    Console.WriteLine($"    New: {e.FullPath}");
}

static void OnError(object sender, ErrorEventArgs e) =>
   PrintException(e.GetException());

static void PrintException(Exception? ex)
{
    if (ex != null)
    {
        Console.WriteLine($"Message: {ex.Message}");
        Console.WriteLine("Stacktrace:");
        Console.WriteLine(ex.StackTrace);
        Console.WriteLine();
        PrintException(ex.InnerException);
    }
}
// ---------------- Synchronizer for Directory_2

static void OnChanged1(object sender, FileSystemEventArgs e)
{

    SyncDirectoriesOnStart.SyncDir();
    Console.WriteLine($"Tried to be Changed: {e.FullPath}, but was sinchronized with file from dir1");
}

static void OnCreated1(object sender, FileSystemEventArgs e)
{

    string value = $"Created: {e.FullPath}";
    Console.WriteLine(value);
    SyncDirectoriesOnStart.SyncDir();
}

static void OnDeleted1(object sender, FileSystemEventArgs e)
{

    Console.WriteLine($"Deleted: {e.FullPath}");
    SyncDirectoriesOnStart.SyncDir();
}


static void OnRenamed1(object sender, RenamedEventArgs e)
{
    SyncDirectoriesOnStart.SyncDir();
    Console.WriteLine($"Renamed:");
    Console.WriteLine($"    Old: {e.OldFullPath}");
    Console.WriteLine($"    New: {e.FullPath}");
}

static void OnError1(object sender, ErrorEventArgs e) =>
   PrintException(e.GetException());

static void PrintException1(Exception? ex)
{
    if (ex != null)
    {
        Console.WriteLine($"Message: {ex.Message}");
        Console.WriteLine("Stacktrace:");
        Console.WriteLine(ex.StackTrace);
        Console.WriteLine();
        PrintException(ex.InnerException);
    }
}

Console.WriteLine("Press enter to exit.");
Console.ReadLine();
