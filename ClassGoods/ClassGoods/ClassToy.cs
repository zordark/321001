using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ClassGoods
{
    public class Toy : Goods
    {
        int year;//возраст ребенка
        string material;//материал из которого сделана игрушка

        public int Year
        {
            get { return year; }

            set 
            {
                if (value <= 0)
                {
                    throw new MyException("возраст должен быть отличен от нуля");
                }
                year = value; 
            }
        }

        public string Material
        {
            get { return material; }

            set { material = value; }
        }

        //конструктор
        public Toy(string CategoryGoods, double PriceGoods, int YearToy, string MaterialToy)
            : base(CategoryGoods, PriceGoods)
        {
            Year = YearToy;
            Material = MaterialToy;
        }

        //цена с учетом скидки
        public double Sale(double PriceToy, int SaleToy)
        {
            double tmp, result;
            tmp = PriceToy * SaleToy / 100;
            result = PriceToy - tmp;
            return result;
        }

        //соответствие возрастных ограничений
        public bool YearToy(int YearsBaby)
        {
            bool tmp = false;

            if (YearsBaby < Year)
            {
                tmp = true;
                return tmp;
            }

            else return false;
        }

        public override string Barcode()
        {
            Random r = new Random();
            int barcode = r.Next();
            string str = "toy-" + Convert.ToString(barcode);
            MessageBox.Show(str);
            return str;
        }
    }
}
