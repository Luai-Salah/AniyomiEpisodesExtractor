using System;
using System.IO;

namespace AniyomiEpisodesExtractor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No Argumants provided, so no action will be taken!");
                return;
            }

            if (args[0] != "-c")
                return;

            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            int filesMoved = 0;


            foreach (var dir in directory.GetDirectories())
            {
                foreach (var file in dir.GetFiles("*.mp4"))
                {
                    file.MoveTo($"{directory.FullName}/{file.Name}");
                    Console.WriteLine($"{directory.FullName}/{file.Name}");
                    filesMoved++;
                    dir.Delete(true);
                }
            }

            Console.WriteLine($"\n{filesMoved} files were moved!");
        }
    }
}
