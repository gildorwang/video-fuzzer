using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoFuzzer
{
    class Program
    {
        /// <summary>
        /// Only process files that are bigger than 50MB.
        /// </summary>
        private const int MinFileSize = 50 * 1024 * 1024;

        static void Main(string[] args)
        {
            if(!args.Any())
            {
                return;
            }
            try
            {
                var path = args[0];
                if(File.Exists(path))
                {
                    FuzzFile(path);
                }
                else if (Directory.Exists(path))
                {
                    foreach (var filename in EnumerateFiles(path))
                    {
                        FuzzFile(filename);
                    }
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private static IEnumerable<string> EnumerateFiles(string path)
        {
            return new DirectoryInfo(path)
                .EnumerateFiles("*", SearchOption.AllDirectories)
                .Where(info => info.Length > MinFileSize)
                .Select(info => info.FullName);
        }

        private static void FuzzFile(string path)
        {
            using (var stream = File.Open(path, FileMode.Append, FileAccess.Write))
            {
                stream.Write(Enumerable.Repeat((byte) 0, 16).ToArray(), 0, 16);
            }
        }
    }
}
