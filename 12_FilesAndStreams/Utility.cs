using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_FilesAndStreams
{
    public static class Utility
    {
      public  static string pathA = @"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\Dir_1";
      public  static string pathB = @"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\Dir_2";

      public  static string pathA1 = "C:\\Users\\kusni\\source\\repos\\12_FilesAndStreams\\12_FilesAndStreams\\Dir_1\\";
      public  static string pathB1 = "C:\\Users\\kusni\\source\\repos\\12_FilesAndStreams\\12_FilesAndStreams\\Dir_2\\";
    public   static System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(pathA);
       public  static System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(pathB);

        // Take a snapshot of the file system.  
        public static IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
        public static IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);


    }

}
