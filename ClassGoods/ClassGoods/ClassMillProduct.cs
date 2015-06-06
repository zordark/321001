using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassGoods
{
    public class MilkProduct: Product,IClabber
    {
        double fatness;//жирность
        DateTime date;//срок годности
        bool clabber = false;//для простокваши

        public double Fatness
        {
            get { return fatness; }

            set 
            {
                if (value < 0)
                {
                    throw new MyException("жирность не может быть отрицательной");
                }
                fatness = value; 
            }
        }

        public DateTime Date
        {
            get { return date; }

            set { date = value; }
        }


        public MilkProduct(string CategoryGoods, double PriceGoods, string NameProduct, double WeightProduct, double FatnessProduct, DateTime DateProduct)
            :base(CategoryGoods,PriceGoods,NameProduct,WeightProduct)
        {
            Fatness = FatnessProduct;
            Date = DateProduct;
        }


        //повышение жирности продукта
        public double FatnessAdd(double FatnessProduct)
        {
            this.Fatness += FatnessProduct;
            return Fatness;
        }



        //проверка на испорченность
        public bool DateYear(DateTime CurrDate)
        {
            if (Date > CurrDate)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public void Clabber()
        {
            this.clabber = true;
        }

    }


}
