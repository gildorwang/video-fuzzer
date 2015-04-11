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
        static void Main(string[] args)
        {
            if(!args.Any())
            {
                return;
            }
            try
            {
                using(var stream = File.Open(args[0], FileMode.Append, FileAccess.Write))
                {
                    stream.Write(Enumerable.Repeat((byte)0, 16).ToArray(), 0, 16);
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
