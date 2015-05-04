using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _1000words
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
			//Обращаемся к буферу обмена. Считываем текст с кодировкой Юникод
            var text = Clipboard.GetData(DataFormats.UnicodeText).ToString();
			
			//Разбиваем полученный текст на слова по всем знакам препинания и управления
			string[] words = text.Split(new[] { ' ', ',', '.', ':', ';', '?', '!', '"', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
			List<string> wordList = new List<string>();

			//Проверяем каждое слово на уникальность без учета регистра 
			foreach (var word in words)
			{
				if (!(wordList.Find(existingWord => existingWord.ToLower() == word.ToLower()) != null ? true : false)) wordList.Add(word);
			}

			//Отделяем 1000 слов
			if(wordList.Count >= 1000)
			{
				wordList = wordList.GetRange(0, 999);
			}
			//Сортируем и выводим на экран 
			wordList.Sort();
			wordList.ForEach(word => Console.WriteLine(word));
			Console.ReadLine();
        }
    }
}
