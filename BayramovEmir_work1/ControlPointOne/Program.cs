using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlPointOne
{
   
    class Program
    {

      
        [STAThread]
        static void Main(string[] args)
        {
           
            Console.OutputEncoding = Encoding.UTF8;
           
            try
            {

                var buffer = (string)Clipboard.GetDataObject()
                                                 .GetData(DataFormats.UnicodeText);


                if (buffer == null)
                {
                    Console.WriteLine("Cilpboard is empty");
                    return;
                }

                var words = buffer.Replace('\n', ' ')
                     .Replace('\r', ' ')
                     .Replace('(', ' ')
                     .Replace(')', ' ')
                     .Replace('\t', ' ')
                     .Replace('\t', ' ')
                     .Replace('\"', ' ')
                     .Replace('\'', ' ')
                     .Split('.', '!', '?', ':', ';', ',', ' ')
                     .Where(s => s != "")
                     .Select(s => s.ToLower())
                     .Distinct()
                     .OrderBy(s => s)
                     .Take(1000);

                foreach (var word in words)
                    Console.WriteLine(word);



               Console.ReadLine();   
            }
            catch(Exception e)
            {
                
                Console.WriteLine("Unknown error: "+e.Message);
            }
        }
    }
}
