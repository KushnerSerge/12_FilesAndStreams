// See https://aka.ms/new-console-template for more information


using SyncDeepWatcher;

SyncFolders_Automatic.SyncAutomatic();

using var watcher = new FileSystemWatcher(@"C:\Users\kusni\source\repos\12_FilesAndStreams\SyncDeepWatcher\Dir_1\");
using var watcher1 = new FileSystemWatcher(@"C:\Users\kusni\source\repos\12_FilesAndStreams\SyncDeepWatcher\Dir_2\");

watcher.NotifyFilter = NotifyFilters.Attributes
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

watcher.Filter = "*";
watcher.IncludeSubdirectories = true;
watcher.EnableRaisingEvents = true;



watcher1.NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size;

watcher1.Changed += OnChanged1;
watcher1.Created += OnCreated1;
watcher1.Deleted += OnDeleted1;
watcher1.Renamed += OnRenamed1;
watcher1.Error += OnError1;

watcher1.Filter = "*";
watcher1.IncludeSubdirectories = true;
watcher1.EnableRaisingEvents = true;



static void OnChanged(object sender, FileSystemEventArgs e)
{

    if (e.ChangeType != WatcherChangeTypes.Changed)
    {
        return;
    }

    SyncFolders_Automatic.SyncAutomatic();

}


static void OnCreated(object sender, FileSystemEventArgs e)
{
    /*
       if (e.FullPath.Contains('.'))
       {
            string destinationForFile = e.FullPath.Replace("Dir_1", "Dir_2");
          //  File.Delete(destinationForFile);
            File.Copy(e.FullPath, destinationForFile);
       }
    */
    SyncFolders_Automatic.SyncAutomatic();
  //  SyncFolders_Automatic.SyncDirectoryNumberTwo();
}



static void OnDeleted(object sender, FileSystemEventArgs e)
{

    SyncFolders_Automatic.SyncAutomatic();

}


static void OnRenamed(object sender, RenamedEventArgs e)
{
    if (!e.FullPath.Contains('.'))
    {

        string destinationForFile = e.FullPath.Replace("Dir_1", "Dir_2");
        if (!Directory.Exists(destinationForFile))
        {
            Directory.CreateDirectory(destinationForFile);
        }
        string[] dirs_2 = Directory.GetDirectories(destinationForFile, "*", SearchOption.AllDirectories);
        var resultToDelete = dirs_2.ToList();

        while (resultToDelete.Count() > 0)
        {
            for (int i = 0; i < resultToDelete.Count; i++)
            {
                string[] files = Directory.GetFiles(resultToDelete[i]);
                foreach (var file in files)
                {
                    System.IO.File.Delete(file);
                }
                if (Utility.IsDirectoryEmpty(resultToDelete[i]))
                {
                    Directory.Delete(resultToDelete[i]);
                    resultToDelete.Remove(resultToDelete[i]);
                }
                else
                {
                    Console.WriteLine("Dir is not empty");
                }
            }

            Console.WriteLine($"  hhh  Old: {e.OldFullPath}");
            Console.WriteLine($"  Hhh   New: {e.FullPath}");
        }
        Directory.Delete(destinationForFile);
        SyncFolders_Automatic.SyncAutomatic();
      // SyncFolders_Automatic.SyncDirectoryNumberTwo();
      
        Console.WriteLine($"Renamed:");
        Console.WriteLine($"    Old: {e.OldFullPath}");
        Console.WriteLine($"    New: {e.FullPath}");
    }
    if (e.FullPath.Contains('.'))
    {
        //    string destinationForFile = e.FullPath.Replace("Dir_1", "Dir_2");
        SyncFolders_Automatic.SyncAutomatic();
        //   File.Delete(e.FullPath);
    }
    //  SyncFolders_Automatic.SyncAutomatic();
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


// ------------ --------------------------  ---------------------------  ------------

static void OnChanged1(object sender, FileSystemEventArgs e)
{

    if (e.ChangeType != WatcherChangeTypes.Changed)
    {
        return;
    }
    SyncFolders_Automatic.SyncAutomatic();
  //   SyncFolders_Automatic.SyncDirectoryNumberTwo();

}



static void OnCreated1(object sender, FileSystemEventArgs e)
{


  //  if (e.FullPath.Contains('.'))
  //  {
  //      File.Delete(e.FullPath);
        //  SyncFolders_Automatic.SyncAutomatic();
 //   }
    /*
    if (!e.FullPath.Contains('.'))
    {
        Directory.Delete(e.FullPath);
        SyncFolders_Automatic.SyncAutomatic();
    }
    */
    SyncFolders_Automatic.SyncAutomatic();

}



static void OnDeleted1(object sender, FileSystemEventArgs e)
{
    SyncFolders_Automatic.SyncAutomatic();
    // SyncFolders_Automatic.SyncDirectoryNumberTwo();

}


static void OnRenamed1(object sender, RenamedEventArgs e)
{
    SyncFolders_Automatic.SyncAutomatic();
    //   SyncFolders_Automatic.onRenamedDir2(e.FullPath);
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

