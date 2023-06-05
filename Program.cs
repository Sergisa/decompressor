using System;
using System.IO;
using System.IO.Compression;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;

namespace decompressor
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            RootCommand command = new RootCommand()
            {
                new Option<FileInfo>("--file", "Microsoft SQL Server address or name")
            };
            command.Description = "decompressor";
            command.Handler = CommandHandler.Create<FileInfo>(DecompressFile);
            //DecompressFile("C:\\Users\\Sergisa\\DataGripProjects\\1c-mgppu\\config.root");
            return command.Invoke(args);
        }


        static void DecompressFile(FileInfo file)
        {
            string outPath = file.Directory.FullName + "\\" + file.Name + ".decompressed.txt";
            Console.WriteLine(outPath);
            FileStream compressedFileStream = File.Open(file.FullName, FileMode.Open);
            FileStream outputFileStream = File.Create(outPath);
            var decompressor = new DeflateStream(compressedFileStream, CompressionMode.Decompress);
            decompressor.CopyTo(outputFileStream);
        }
    }
}