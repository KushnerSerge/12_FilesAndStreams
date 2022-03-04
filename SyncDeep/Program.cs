// See https://aka.ms/new-console-template for more information

using SyncDeep;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;



string pathA = @"C:\Users\kusni\source\repos\12_FilesAndStreams\SyncDeep\Dir_1";
string pathB = @"C:\Users\kusni\source\repos\12_FilesAndStreams\SyncDeep\Dir_2";

string pathA1 = "C:\\Users\\kusni\\source\\repos\\12_FilesAndStreams\\SyncDeep\\Dir_1\\";
string pathB1 = "C:\\Users\\kusni\\source\\repos\\12_FilesAndStreams\\SyncDeep\\Dir_2\\";

System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(pathA);
System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(pathB);

// Take a snapshot of the file system.  
IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

// Copy directories from Dir_1 to Dir_2
Utility.CreateFoldersFromDir1ToDir2();


FileCompare myFileCompare = new FileCompare();


bool areIdentical = list1.SequenceEqual(list2, myFileCompare);

if (areIdentical == true)
{
    Console.WriteLine("These 2 folders have same nr of files with the same content");

}
else
{
    Console.WriteLine("The two folders are not the same");
}

// Find the set difference between the two folders.  
// For this example we only check one way.  
var queryList1OnlyDir1 = (from file in list1
                          select file).Except(list2, myFileCompare);

Console.WriteLine("The following files are in list1 but not list2:");
foreach (var v in queryList1OnlyDir1)
{
    Console.WriteLine(v.FullName);

    string destinationForFile = v.FullName.Replace("Dir_1", "Dir_2");
    string destinationDirectory = destinationForFile.Replace(v.Name, "");
    Directory.CreateDirectory(destinationDirectory);
    //  v.CopyTo(destinationForFile);
    // System.IO.File.Copy(v.FullName, destination);

    if (System.IO.File.Exists(destinationForFile))
    {
        System.IO.File.Delete(destinationForFile);
    }

    if (!System.IO.File.Exists(destinationForFile))
    {
        v.CopyTo(destinationForFile);
    }

}


// Find the set difference between the two folders.  
Console.WriteLine("The following files are in list2 but not list1:");
var queryList1OnlyDir2 = (from file in list2
                          select file).Except(list1, myFileCompare);
 List<string> dirToCheckIfDelete = new List<string>();
foreach (var v in queryList1OnlyDir2)
{
    string destinationForFile = v.FullName.Replace("Dir_2", "Dir_1");
    
    if (System.IO.File.Exists(destinationForFile))
    {
        Console.WriteLine($"{v.FullName} is updated, no need for more actions");
    }
    if(!System.IO.File.Exists(destinationForFile))
    {
        System.IO.File.Delete(v.FullName);
        //  v.Delete();
    }


}


Utility.DeleteFoldersFromDir2WhichIsNotInDir1();
//DeleteAllEmptyDirectories();

Console.ReadKey();









