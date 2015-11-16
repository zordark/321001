using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SlovaSbornik
{
    class Parser
    {

        public List<string> SplitString(string str)
      {

            string str1 = str.ToLower();

            str1 = str1.Replace(".", " ");
            str1 = str1.Replace(",", " ");
            str1 = str1.Replace(";", " ");
            str1 = str1.Replace(":", " ");
            str1 = str1.Replace("?", " ");
            str1 = str1.Replace("!", " ");
            str1 = str1.Replace("\n", " ");
            str1 = str1.Replace("\r", " ");
            str1 = str1.Replace("&", " ");
            str1 = str1.Replace(" - ", " ");
            str1 = str1.Replace("%", " ");
          string[] words = str1.Split(' ');
          List<string> listWords = new List<string>();
            for (int i=0; i<words.Length;i++)
            {
                if (words[i]!="" && listWords.Contains(words[i])==false)
                {
                    listWords.Add(words[i]);
                }
            }
            return listWords;
      }

    }
}
