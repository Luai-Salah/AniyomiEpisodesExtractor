using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            List<(FileInfo, DirectoryInfo)> filesToBeMoved = new List<(FileInfo, DirectoryInfo)>();

            foreach (var dir in directory.GetDirectories())
            {
                foreach (var file in dir.GetFiles("*.*").Where(s => s.Extension == ".mp4" || s.Extension == ".mkv"))
                {
                    filesToBeMoved.Add((file, dir));
                    Console.WriteLine($"{directory.FullName}/{file.Name}");
                }
            }

            Console.WriteLine("\nThe following files will be processed... do you want to proceed? [Y/N]");

            KeyInput:

            var answer = Console.ReadLine();
            if (answer == "n" || answer == "N")
            {
                Console.WriteLine("Operation canceled.");
                return;
            }

            if (answer != "y" && answer != "Y")
            {
                Console.WriteLine("Please enter either Y (yes) or N (no)");
                goto KeyInput;
            }

            foreach (var (file, parent) in filesToBeMoved)
            {
                file.MoveTo($"{directory.FullName}/{file.Name}");
                filesMoved++;
                parent.Delete(true);
            }

            Console.WriteLine($"\n{filesMoved} files were moved!");
            Console.ReadKey();
        }
    }
}
