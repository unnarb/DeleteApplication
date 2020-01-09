using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DeleteApplication
{
    class Program
    {
        static string path = "c:\\tmp\\be";
        static int size = 0;
        static bool subFolders = true;
        static int i = 0;
        static long amount = 0;

        private static void DeleteFiles(string path)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string filename in files)
            {
                byte[] bytes = File.ReadAllBytes(filename);

                if (bytes.Length < size || size == 0)
                {
                    amount += bytes.Length;

                    try
                    {
                        File.Delete(filename);
                        i++;
                    }
                    catch
                    {
                        Console.WriteLine("Couldn't Delete: " + filename);
                    }
                }
            }
        }

        private static void DeleteDirectory(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                try
                {
                    DeleteFiles(dir);
                }
                catch
                { }
                
                string[] subDirs = Directory.GetDirectories(path);
                {
                    foreach (string dirSub in subDirs)
                    {
                        DeleteDirectory(dirSub);
                    }
                }

                try
                {
                    Directory.Delete(dir);
                }
                catch { }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                path = args[0];
            }
            catch
            {}

            try
            {
                size = int.Parse(args[1]);
            }
            catch
            {}

            try
            {
                var b = args[2];
                if (b == "false")
                    subFolders = false;
                if (b == "0")
                    subFolders = false;
            }
            catch { }

            if (subFolders)
            {
                DeleteDirectory(path);
                DeleteFiles(path);
            }
            else
            {
                DeleteFiles(path);
            }

            Console.WriteLine( i.ToString() + " cached items deleted total of " + amount + " bytes");
            Console.Read();
        }
    }
}
