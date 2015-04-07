using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyHomeWork1
{
    public class ConvertText
    {
        private string sourceText;
        private List<string> uniqueWords;

        public string SourceText
        {
            get
            {
                return sourceText;
            }
            set
            {
                sourceText = value;
            }
        }

        public List<string> UniqueWords
        {
            get
            {
                return uniqueWords;
            }
            set
            {
                uniqueWords = value;
            }
        }

        public ConvertText(string s)
        {
            sourceText = s;
            uniqueWords = new List<string>();
        }

        public List<string> ConvertingText(string s) // 1) Заменяем лишние символы на пробелы 2) разделяем слова по пробелам 3) Добавляем уникальные слова 4) Сортируем
        {
            string text = s.ToLower();
            List<string> listWords = new List<string>();

            text = text.Replace(".", " ");
            text = text.Replace(",", " ");
            text = text.Replace(";", " ");
            text = text.Replace(":", " ");
            text = text.Replace("?", " ");
            text = text.Replace("!", " ");
            text = text.Replace("-", " ");
            text = text.Replace("\n", " ");
            text = text.Replace("\r", " ");

            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                if (word != "" && word != " " && !listWords.Contains(word))
                {
                    listWords.Add(word);
                }
            }

            listWords.Sort();

            return listWords; 
        }

        public List<string> doConvertText(string txt)
        {
            UniqueWords = ConvertingText(txt);

            return UniqueWords;
        }
    }
}
