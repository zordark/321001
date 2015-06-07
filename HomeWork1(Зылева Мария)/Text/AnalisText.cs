using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Text
{
    public class AnalisText
    {
        private string input_string;
        private List<string> output_strings;

        public string InputString
        {
            get { return input_string; }
            set { input_string = value; }
        }
        
        public List<string> OutputStrings
        {
            get { return output_strings; }
            set { output_strings = value; }
        }

        //конструктор
        public AnalisText(string Stroka)
        {
            OutputStrings = new List<string>();
            InputString = Stroka;
        }


        private string Parser(string stroka_)
        {
            string str = stroka_.ToLower(); // Перевод в нижний регистр

            str = str.Replace(".", " "); // Удаление лишних символов
            str = str.Replace(";", " ");
            str = str.Replace(":", " ");
            str = str.Replace(",", " ");
            str = str.Replace("?", " ");
            str = str.Replace("!", " ");
            str = str.Replace("\n", " ");
            str = str.Replace("\r", " ");

            return str;
        }

        private List<string> SplitToList(string str)
        {
            string[] strings = str.Split(' ');

            List<string> ListOfStrings = new List<string>();

            foreach (string str1 in strings)
            {
                if (str1 != " " && str1 != ""&& !ListOfStrings.Contains(str1)) 
                {
                    ListOfStrings.Add(str1);
                }
            }

            ListOfStrings.Sort();
            ListOfStrings.Take(1000);
            return ListOfStrings;

        }

        public List<string> TextAnalis(string inputStr)
        {
            OutputStrings = SplitToList(Parser(inputStr));
            return OutputStrings;
        }


        public List<string> TextAnalis()
        {
            OutputStrings = TextAnalis(InputString);
            return OutputStrings;
        }
    }
}
