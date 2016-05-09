using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace TextMutation
{
    class Program
    {
        
        [STAThread]

        static void Main(string[] args)
        {

            string str;
            TextParser txp; //текстовый парсер-обработчик 

            Console.WriteLine("\nПожалуйста, скопируйте текст в буфер обмена и нажмите клавишу enter.");

            do
            {
                while (Console.ReadKey().Key != ConsoleKey.Enter) { Console.WriteLine("\nСкопируйте текст в буфер обмена и нажмите клавишу enter."); }

                str = (String)Clipboard.GetText(); //Копируем текст из буффера

                if (str != "") //Выполняем обработку текста если строка не пустая
                {
                    txp = new TextParser(str);
                    txp.ProcessingText();


                 
                    Console.WriteLine("------------------------------------------------------------\nКоличество слов в тексте: " + (txp.InputString.Split(' ')).Length);
                    Console.WriteLine("Количество слов в обрботанном списке: " + txp.OutputStrings.Count);

                    int count = 1;
                    Console.WriteLine("\nУникальные слова в тексте:\n");

                    foreach (string str1 in txp.OutputStrings)
                    {
                          if (count <= 1000)
                        {
                            Console.WriteLine((count) + ") " + str1);
                            count++;
                        }
                        else break;
                    }


                }
                else Console.WriteLine("Буффер-обмена пустой, повторите действие."); // А если пустая, то информируем о фиаско пользователя

            } while (Console.ReadKey().Key != ConsoleKey.Escape); //программа работает в цикле пока не нажата клавиша escape
            
        }
    }
}
