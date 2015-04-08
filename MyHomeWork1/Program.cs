using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace MyHomeWork1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            Console.OutputEncoding = Encoding.UTF8;
            string txt;
            ConvertText converter;
            int count = 1;
            Console.WriteLine("Скопируйте в буфер обмена текст. И нажмите Enter. Для выхода нажмите Escape.");
            txt = (String)Clipboard.GetData(DataFormats.UnicodeText); // вытаскиваем из буфера текст
            do
            {

                if (Console.ReadKey().Key == ConsoleKey.Enter && txt != "" && txt != " ")
                {
                    converter = new ConvertText(txt);
                    converter.doConvertText(txt);

                    foreach (string word in converter.UniqueWords)
                    {
                        if (count < 1000)
                        {
                            Console.WriteLine(count + ". " + word);
                            count++;
                        }
                        else break;
                    }

                }
                else Console.WriteLine("Вы ничего не скопировали, либо не нажали Enter");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

        }
    }
}
