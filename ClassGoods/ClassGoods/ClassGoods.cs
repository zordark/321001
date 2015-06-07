using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ClassGoods
{
    public class Goods: IBarcode
    {
        string category; //категория товара
        double price; //цена
        int amount = 0;


        public string Category
        {
            get{return category;}

            set{category= value;}
        }

        public double Price
        {
            get{return price;}

            
            set
            {
                if (value <= 0)
                {
                    throw new MyException("цена должна быть больше нуля");
                }
                
                price=value;
            }
        }

        

        //конструктор 
        public Goods(string CategoryGoods, double PriceGoods)
        {
            Category = CategoryGoods;
            Price = PriceGoods;
        }

        //завоз товара на склад
        public double delivery_to_the_warehouse(int AmountGoods)
        {
            amount += AmountGoods;
            return amount;

        }

        //общая цена за количество единиц товара
        public double PriceAll(int AmountGoods)
        {
            double newPrice = this.price * AmountGoods;
            return newPrice;
            
        }

        //наляпать штрих-код
        public virtual string Barcode()
        {
            Random r=new Random();
            int barcode = r.Next();
            string str = Convert.ToString(barcode);
            MessageBox.Show(str);
            return str;
        }



    }
}
