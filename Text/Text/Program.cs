using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Text
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            var stroka=(string)Clipboard.GetDataObject().GetData(DataFormats.Text);

            if (stroka == "")
            {
                Console.WriteLine("буфер пустой!");
                Console.ReadKey(true);
                return;
            }

            AnalisText tmp = new AnalisText(stroka);
            tmp.TextAnalis();

            int count = 0;

            Console.WriteLine("\nсписок уникальных слов:\n");

            foreach (string str1 in tmp.OutputStrings)
            {
                Console.WriteLine((count + 1) + ") " + str1);
                count++;
            }

            Console.ReadKey(true);
            return;
        }
    }
}
