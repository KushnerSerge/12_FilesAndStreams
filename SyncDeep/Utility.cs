using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncDeep
{
    class FileCompare : System.Collections.Generic.IEqualityComparer<System.IO.FileInfo>
    {
        public FileCompare() { }

        public bool Equals(System.IO.FileInfo f1, System.IO.FileInfo f2)
        {
            return (f1.Name == f2.Name &&
                    f1.Length == f2.Length);
        }

        // Return a hash that reflects the comparison criteria. According to the
        // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must  
        // also be equal. Because equality as defined here is a simple value equality, not  
        // reference identity, it is possible that two or more objects will produce the same  
        // hash code.  
        public int GetHashCode(System.IO.FileInfo fi)
        {
            string s = $"{fi.Name}{fi.Length}";
            return s.GetHashCode();
        }
    }

    public static class Utility
    {
        public static void DeleteFoldersFromDir2WhichIsNotInDir1()
        {
            string pathA = @"C:\Users\kusni\source\repos\12_FilesAndStreams\SyncDeep\Dir_1";
            string pathB = @"C:\Users\kusni\source\repos\12_FilesAndStreams\SyncDeep\Dir_2";

            string[] dirs_1 = Directory.GetDirectories(pathA, "*", SearchOption.AllDirectories);
            string[] dirs_2 = Directory.GetDirectories(pathB, "*", SearchOption.AllDirectories);

            Console.WriteLine("-----AAAAA--------");
            List<string> list_1 = new List<string>();
            foreach (var dir in dirs_1)
            {
                string s = dir.Replace("Dir_1", "Dir_2");
                list_1.Add(s);
            }
            List<string> list_2 = dirs_2.ToList();
            var resultToDelete = list_2.Except(list_1);

            foreach (var item in resultToDelete)
            {
              string [] files =  Directory.GetFiles(item);
                foreach(var file in files)
                {
                    System.IO.File.Delete(file);
                }
                if(IsDirectoryEmpty(item)){
                    Directory.Delete(item);
                }
               else
                {
                    Console.WriteLine("Dir is not empty");
                }

               
            }
            Console.WriteLine("-----BBBBB----------");
           
        }

        public static void CreateFoldersFromDir1ToDir2()
        {
            string pathA = @"C:\Users\kusni\source\repos\12_FilesAndStreams\SyncDeep\Dir_1";
 //           string pathB = @"C:\Users\kusni\source\repos\12_FilesAndStreams\SyncDeep\Dir_2";

            string[] dirs_1 = Directory.GetDirectories(pathA, "*", SearchOption.AllDirectories);
//            string[] dirs_2 = Directory.GetDirectories(pathB, "*", SearchOption.AllDirectories);

            Console.WriteLine("-----AAAAA--------");
            List<string> list_1 = new List<string>();
            foreach (var dir in dirs_1)
            {
                string s = dir.Replace("Dir_1", "Dir_2");
                list_1.Add(s);
            }
            foreach(var item in list_1)
            {
                Directory.CreateDirectory(item);
            }
          
            Console.WriteLine("-----BBBBB----------");

        }
        static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
    }
}

