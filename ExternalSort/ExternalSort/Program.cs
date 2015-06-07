using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ExternalMergeSort
{
    class Program
    {
       
        static void Main(string[] args)
        {

           // GenegateLargeFile();
            Split("fairy_tale_large.txt");
            SortTheChunks();
            MergeTheChunks();


        }

        static void GenegateLargeFile()
        {
            
            var sw = new StreamWriter("fairy_tale_large.txt");
            string str;
            for (int i = 0; i < 1000000; i++)
            {
                var sr = new StreamReader("fairy_tale.txt");
                while ((str = sr.ReadLine()) != null)
                    sw.WriteLine(str);
                sr.Close();
            }
        }

        
        static void MergeTheChunks()
        {
            string[] paths = Directory.GetFiles(".\\", "sorted*.dat");
            int chunks = paths.Length; // кол-во вспомогательных файлов
            int recordsize = 350; // приблизительный размер 1 строки байт
            int maxusage = 250000000; // максимальное кол-во памяти 250 мб
            int buffersize = maxusage / chunks; // байт в одном куске, загруженный  файл
            
            int bufferlen = (int)(buffersize / recordsize ); // количество элементов 

            
            StreamReader[] readers = new StreamReader[chunks];
            for (int i = 0; i < chunks; i++)
                readers[i] = new StreamReader(paths[i]);

            
            Queue<string>[] queues = new Queue<string>[chunks];
            for (int i = 0; i < chunks; i++)
                queues[i] = new Queue<string>(bufferlen);

            

            for (int i = 0; i < chunks; i++)
                LoadQueue(queues[i], readers[i], bufferlen);


            
            StreamWriter sw = new StreamWriter("fairy_tale_large.txt");
            bool done = false;
            int lowestIndex;
            string lowestValue;
            while (!done)
            {
                // находим файл с наименьшим значением
                lowestIndex = -1;
                lowestValue = "";
                for (int j = 0; j < chunks; j++)
                {
                    if (queues[j] != null)
                    {
                        if (lowestIndex < 0 || String.CompareOrdinal(queues[j].Peek(), lowestValue) < 0)
                        {
                            lowestIndex = j;
                            lowestValue = queues[j].Peek();
                        }
                    }
                }

                if (lowestIndex == -1) { done = true; break; }
                //записываем строку
                sw.WriteLine(lowestValue);
                //удаляем строку
                queues[lowestIndex].Dequeue();
                if (queues[lowestIndex].Count == 0)
                {
                    LoadQueue(queues[lowestIndex], readers[lowestIndex], bufferlen);
                    if (queues[lowestIndex].Count == 0)
                    {
                        queues[lowestIndex] = null;
                    }
                }
            }

            sw.Close();

            for (int i = 0; i < chunks; i++)
            {
                readers[i].Close();
                File.Delete(paths[i]);
            }

        }

        
        static void LoadQueue(Queue<string> queue, StreamReader file, int records)
        {
            for (int i = 0; i < records; i++)
            {
                if (file.Peek() < 0) break;
                queue.Enqueue(file.ReadLine());
            }
        }

       
        static void SortTheChunks()
        {

            foreach (string path in Directory.GetFiles(".\\", "split*.dat"))
            {

                string[] contents = File.ReadAllLines(path);
                Array.Sort(contents);
                string newpath = path.Replace("split", "sorted");
                File.WriteAllLines(newpath, contents);
                File.Delete(path);
                contents = null;

            }

        }

       
        static void Split(string file)
        {

            int split_num = 1;
            StreamWriter sw = new StreamWriter(string.Format("split{0:d5}.dat", split_num));
            using (StreamReader sr = new StreamReader(file))
            {
                while (sr.Peek() >= 0)
                {

                    
                    sw.WriteLine(sr.ReadLine());

                    
                    if (sw.BaseStream.Length > 100000000 && sr.Peek() >= 0)
                    {
                        sw.Close();
                        split_num++;
                        sw = new StreamWriter(string.Format("split{0:d5}.dat", split_num));
                    }
                }
            }

            sw.Close();

        }



    }
}
