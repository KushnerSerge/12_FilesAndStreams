using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_FilesAndStreams
{
    internal class Fisier
    {

        public static void DriveInformation()
        {
            // Get info regarding all drives.
            DriveInfo[] myDrives = DriveInfo.GetDrives();
            // Now print stats. 
            foreach (DriveInfo d in myDrives)
            {
                Console.WriteLine("Name: {0}", d.Name);
                Console.WriteLine("Type: {0}", d.DriveType);
                if (d.IsReady)
                {
                    Console.WriteLine("Free space: {0}", d.TotalFreeSpace);
                    Console.WriteLine("Format: {0}", d.DriveFormat);
                    Console.WriteLine("Label: {0}\n", d.VolumeLabel);
                }
            }
        }

        public static void InformationAboutFileSystem()
        {
           
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\kusni\source\repos");
            DirectoryInfo[] dirs = di.GetDirectories("*p*", SearchOption.AllDirectories);
            Console.WriteLine("Number of directories with a p: {0}", dirs.Length);
            foreach (DirectoryInfo diNext in dirs)
            {
                Console.WriteLine("The number of files and  directories in {0} with an e is {1}", diNext,
                 diNext.GetFileSystemInfos("*e*").Length);
            }
        }

        public static void Reading_and_WritingData()
        {
            string[] myTasks = {"Repair water tap",
                    "Buy bread and milk",
                    "Study last C# features",
                    "Complete lecture materials",
                    "Phone to mum" };
            File.WriteAllLines(@"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\test.txt", myTasks);
            foreach (string task in
               File.ReadAllLines(@"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\test.txt"))
            {
                Console.WriteLine("To do: {0}", task);
            }

        }

        public static void ReadDataFromFile()
        {
            var Data = ReadFile(@"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\test.txt");
            string s1 = Encoding.UTF8.GetString(Data);
            Console.WriteLine("Hey, these is the result" + "\n" + s1);


            static byte[] ReadFile(string filePath)
            {
                byte[] buffer;
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                try
                {
                    int length = (int)fileStream.Length; // get file length 
                    buffer = new byte[length]; // create buffer 
                    int count; // actual number of bytes read 
                    int sum = 0; // total number of bytes read 

                    // read until Read method returns 0 (end of the stream has been reached) 
                    while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                        sum += count; // sum is a buffer offset for next reading 
                }
                finally { fileStream.Close(); }

                return buffer;
            }
        }
       public static void ReadDataDinFisier()
        {
            Console.WriteLine("Enter file name");
            string fn = Console.ReadLine();
            // Open the file and display its contents
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(fn);
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    Console.WriteLine(line);
            }
            catch (IOException e)
            { Console.WriteLine(e.Message); }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public static void WriteSomeData()
        {
            string[] myTasks = {"Wake up at 5 o'clock in the morning",
                    "Gimnastics",
                    "Pray",
                    "Complete lecture materials",
                    "BreakFast" };
            File.WriteAllLines(@"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\test.txt", myTasks);
            foreach (string task in
               File.ReadAllLines(@"C:\Users\kusni\source\repos\12_FilesAndStreams\12_FilesAndStreams\test.txt"))
            {
                Console.WriteLine("To do: {0}", task);
            }
        }

        public static void CreateAndRead()
        {
            FileInfo f = new FileInfo("BinFile.dat");
            using (BinaryWriter bw = new BinaryWriter(f.OpenWrite()))
            {
                double aDouble = 1234.67;
                int anInt = 34567;
                string aString = "A, B, C";
                bw.Write(aDouble);
                bw.Write(anInt);
                bw.Write(aString);
            }
            using (BinaryReader br = new BinaryReader(f.OpenRead()))
            {
                Console.WriteLine(br.ReadDouble());
                Console.WriteLine(br.ReadInt32());
                Console.WriteLine(br.ReadString());
            }
        }
    }
}
