using System;
using System.Collections.Generic;
using System.Text;

namespace TextMutation
{
   public class TextParser
    {
       private string inputString;

        public string InputString
        {
            get { return inputString; }
            set { inputString = value; }
        }
       private  List<string> outputStrings;

        public List<string> OutputStrings
        {
            get { return outputStrings; }
            set { outputStrings = value; }
        }

        public TextParser(string Str)
        {
            OutputStrings = new List<string>();
            InputString = Str;
        }


        private string PreParser(string str1)
        {
            string str = str1.ToLower(); // Перевод в нижний регистр
           
            str = str.Replace(".", " "); // Удаление лишних символов
            str = str.Replace(";", " ");
            str = str.Replace(":", " ");
            str = str.Replace(",", " ");
            str = str.Replace("?", " ");
            str = str.Replace("!", " ");
            str = str.Replace("(", " ");
            str = str.Replace(")", " ");
            str = str.Replace("\n", " ");
            str = str.Replace("\r", " ");

            return str;
        }

       private List<string> SplitToList(string str)
       {
           string[] strings = str.Split(' ');

           List<string> ListOfStrings = new List<string>();

           foreach(string str1 in strings)
           {
               if (str1 != " " && str1 != "" && !ListOfStrings.Contains(str1)) ListOfStrings.Add(str1); // проверяем на наличие повторения этого слова, и провереяем не пустая ли строка.
           }

           ListOfStrings.Sort();

           return ListOfStrings;
           
       }

       public List<string> ProcessingText(string inputStr)
       {
           OutputStrings = SplitToList(PreParser(inputStr));
           return OutputStrings;
       }


       public List<string> ProcessingText()
       {
           OutputStrings = ProcessingText(InputString);
           return OutputStrings;
       }


    }
}
