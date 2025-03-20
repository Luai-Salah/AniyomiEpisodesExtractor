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
                Console.WriteLine("No option was specified, the only option available is '-c' for current directory...");
                Console.ReadKey();
                return;
            }

            if (args[0] != "-c")
            {
                Console.WriteLine($"the option '{args[0]}' isn't known, the only option available is '-c' for current directory...");
                Console.ReadKey();
                return;
            }

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

            if (filesToBeMoved.Count == 0) {
                Console.WriteLine("No episodes found to be moved");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nThe following episodes will be processed... do you want to proceed? [Y/N]");

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

            Console.WriteLine($"\n{filesMoved} episodes were moved!");
            Console.ReadKey();
        }
    }
}
