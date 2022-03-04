using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_FilesAndStreams
{
    public class SyncDirectoriesOnStart
    {
        public static void SyncDir()
        {
            string pathA = @"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\Dir_1";
            string pathB = @"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\Dir_2";

            string pathA1 = "C:\\Users\\kusni\\source\\repos\\12_FilesAndStreams\\12_FilesAndStreams\\Dir_1\\";
            string pathB1 = "C:\\Users\\kusni\\source\\repos\\12_FilesAndStreams\\12_FilesAndStreams\\Dir_2\\";

            System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(pathA);
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(pathB);

            // Take a snapshot of the file system.  
            IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

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
                var pathE = pathB1 + v.Name;
                // Console.WriteLine(pathE);
                //System.IO.File.Move(v.FullName, pathE);
                if (System.IO.File.Exists(pathE))
                {
                    System.IO.File.Delete(pathE);
                }

                if (!System.IO.File.Exists(pathE))
                {
                    System.IO.File.Copy(v.FullName, pathE);
                }
            }

            // Find the set difference between the two folders.  
            Console.WriteLine("The following files are in list2 but not list1:");
            var queryList1OnlyDir2 = (from file in list2
                                      select file).Except(list1, myFileCompare);
            foreach (var v in queryList1OnlyDir2)
            {
                Console.WriteLine(v.FullName);
                var pathE = pathA1 + v.Name;
                if (System.IO.File.Exists(pathE))
                {
                    Console.WriteLine($"{v.FullName} is updated, no need for more actions");
                }
                else
                {
                    System.IO.File.Delete(v.FullName);
                }

            }

            bool Equals(System.IO.FileInfo f1, System.IO.FileInfo f2)
            {
                return (f1.Name == f2.Name &&
                        f1.Length == f2.Length);
            }


            Console.WriteLine("Here is the result");
        }
    }
  public  class FileCompare : System.Collections.Generic.IEqualityComparer<System.IO.FileInfo>
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
}
