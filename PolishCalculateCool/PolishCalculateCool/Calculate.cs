using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolishCalculateCool
{
    public class Calculate : ConvertPostfix
    {
        public double result = 0;
        Stack<double> stackCalculate = new Stack<double>(); 

        
        public double CalculatePolish(string[] markers)
        {
            double n;


            foreach(string marker in markers)
            {
                if (double.TryParse(marker.ToString(), out n))
                {
                      stackCalculate.Push(double.Parse(marker));
                }
                else if (isOperator(marker)) // если оператор берем два последних значения из стека
                {
                    double a = stackCalculate.Pop();
                    double b = stackCalculate.Pop();
                    

                    switch (marker)
                    {
                        case "+": result = b + a; break;
                        case "-": result = b - a; break;
                        case "*": result = b * a; break;
                        case "/": result = b / a; break;
                        case "^": result = Math.Pow(b, a); break;
                    }
                    stackCalculate.Push(result);
                }
                else if (isFunction(marker))
                {
                    double a = stackCalculate.Pop();
                    switch (marker)
                    {
                        case "sin": result = Math.Sin(a); break;
                        case "cos": result = Math.Cos(a); break;
                        case "exp": result = Math.Exp(a); break;
                        case "tg": result = Math.Tan(a); break;

                    }
                    stackCalculate.Push(result);
                }
            }
            return stackCalculate.Peek();
        }  
    }
}


