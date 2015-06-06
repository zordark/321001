using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolishCalculateCool
{
    public class ConvertPostfix
    {
        private Stack<string> stack = new Stack<string>();
        private List<string> outList = new List<string>();
        string outstring;


       public string Converting(string[] markers)
       {
           double n;
           outstring = "";
           outList.Clear(); 
           foreach (string marker in markers)
           {
               if(double.TryParse(marker.ToString(), out n)) // если число добавляем в выходной список
               {
                   outList.Add(marker);
               }

               if (isOperator(marker) == true) // если оператор, то пока стек не пуст и приоритет оператора меньше либо равен приоритету оператора из стека
               {
                   while (stack.Count != 0 && Priority(stack.Peek()) >= Priority(marker))
                   {
                       outList.Add(stack.Pop()); //выталкиваем элменты из стека
                   }
                   stack.Push(marker);
               }

               if (isFunction(marker) == true) // если функция, то пока стек не пуст и приоритет оператора меньше либо равен приоритету оператора из стека
               {
                   while (stack.Count != 0 && Priority(stack.Peek()) >= Priority(marker))
                   {
                       outList.Add(stack.Pop()); //выталкиваем элементы из стека
                   }
                   stack.Push(marker);
               }
               if (marker == "(") //открывающую скобку просто добавляем в стек
               {
                   stack.Push(marker);
               }

               if (marker == ")") //если закрывающая скобка, то пока не наткнемся на открывающую и пока стек не пуст
               {
                   while (stack.Count != 0 && stack.Peek() != "(")
                   {
                       outList.Add(stack.Pop()); //выталкиваем элементы в выходной список
                   }
                   stack.Pop(); 
               }

           }

           //пока стек не будет пустым выталкиваем оставшиеся жлементы в выходной список
           while (stack.Count != 0) 
           {
               outList.Add(stack.Pop());
           }

          
           // формируем результат
           for (int i = 0; i < outList.Count; i++)
           {
               outstring += outList[i];
               if (i != outList.Count - 1)
               {
                   outstring += " ";
               }
           }

           return outstring;
       }


       public bool isOperator(string marker)
       {
           if (marker == "+" || marker == "-" || marker == "*" || marker == "/" || marker == "^")
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       public bool isFunction(string marker)
       {
           if (marker == "cos" || marker == "sin" || marker == "tg" || marker == "exp" )
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       int Priority(string marker)
       {
           if (marker == "^")
           {
               return 3;
           }
           else if (marker == "*" || marker == "/")
           {
               return 2;
           }
           else if (marker == "+" || marker == "-")
           {
               return 1;
           }
           else if (marker == "cos" || marker == "sin" || marker == "tg" || marker == "exp")
           {
               return 4;
           }
           else
           {
               return 0;
           }

       }

    }
}

