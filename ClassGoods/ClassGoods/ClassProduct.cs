using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ClassGoods
{
    public class Product : Goods
    {
        public string name;//название продукта
        public double weight;//вес продукта

        public string Name
        {
            get{return name;}

            set{name = value;}
        }

        public double Weight
        {
            get{return weight;}

            set
            {
                if (value < 0)
                {
                    throw new MyException("вес должен быть больше 0");
                }
                
                weight = value;
            
            }
        }

        //конструктор
        public Product(string CategoryGoods, double PriceGoods,string NameProduct, double WeightProduct)
            :base(CategoryGoods,PriceGoods)
        {
            Name = NameProduct;
            Weight = WeightProduct;
        }
        
        //подсчет цены покупки определенного количества грамм товара
        public double calculate(double price,double WeightProduct)
        {
           double result = WeightProduct * price / 1000;

           return result;
        }

        //убираем товар с прилавка
        public void ClearProduct()
        {
            this.Weight = 0.0;
        }


        public override string Barcode()
        {
            Random r = new Random();
            int barcode = r.Next();
            string str = "prod-" + Convert.ToString(barcode);
            
            MessageBox.Show(str);
            return str;
        }
    }
}
