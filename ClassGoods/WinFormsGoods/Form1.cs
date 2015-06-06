using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassGoods;

namespace WinFormsGoods
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        Goods goods;
        Product product;
        Toy toy;
        MilkProduct milk;

        public delegate string AllBarcode();

        AllBarcode deleg;

        void addToDel(IBarcode object_)
        {
            deleg += object_.Barcode;
        }

        #region кнопки работы с классом товар

        double tmp,tmp2;
        bool flag = false, flag1 = false;

        // создание товара
        private void button3_Click(object sender, EventArgs e)
        {
            flag = false; flag1 = false;
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    throw new MyException("вы заполнили не все поля");
                }
                goods = new Goods(textBox1.Text,Convert.ToDouble(textBox2.Text));
                addToDel(goods);
                InfoGoods();
            }

            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        //завоз товара на склад
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (goods != null)
                {
                    if (textBox3.Text == "")
                    {
                        throw new MyException("вы не заполнили поле - количество:");
                    }
                    flag = true;
                    tmp = goods.delivery_to_the_warehouse(Convert.ToInt32(textBox3.Text));
                    InfoGoods();
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        //рассчет оптовой покупки
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (goods != null)
                {
                    if (textBox4.Text == "")
                    {
                        throw new MyException("вы не заполнили поле - количество");
                    }
                    
                    flag1 = true;
                    tmp2 = goods.PriceAll(Convert.ToInt32(textBox4.Text));
                    InfoGoods();
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //сбор полной информации о товаре
        void InfoGoods()
        {
            if (goods != null)
            {
                Info.Text = "произведен товар категории: " + goods.Category +" "+ "с ценой: " + goods.Price + "\n\n";

                if (flag)
                {
                    Info.Text += "на склад прибыл товар в количестве: " + textBox3.Text +" " + "штук. " +"\n" + "всего товара: " + tmp +"\n\n";
                    
                }

                if (flag1)
                {
                    Info.Text += "общая цена за " + textBox4.Text +  " " + "штук: " +  tmp2 + "\n\n";
                    
                }

            }

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Clear();
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            textBox4.Clear();
        }


        #endregion


        #region кнопки для работы с классом продукт

        bool flag3=false, flag4=false;
        double tmp3;

        //выкладываем произведенныйтовар в магазин
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (goods != null)
                {
                    if (textBox8.Text == "" || textBox7.Text == "")
                    {
                        throw new MyException("вы заполнили не все поля");
                    }
                    flag3 = false; flag4 = false;
                    product = new Product(goods.Category, goods.Price, textBox8.Text, Convert.ToInt32(textBox7.Text));
                    addToDel(product);
                    InfoProduct();
                }
                else
                {
                    throw new MyException("сначала нужно произвести товар");
                }
            }
            catch(MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        //отовариваемся в магазине
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (product != null )
                {
                    if (textBox6.Text == "")
                    {
                        throw new MyException("вы заполнили не все поля");
                    }
                    if (Convert.ToInt16(textBox6.Text) < 0)
                    {
                        throw new MyException("вес должен быть больше нуля");
                    }
                    flag3 = true;
                    tmp3 = product.calculate(goods.Price, Convert.ToDouble(textBox6.Text));
                    InfoProduct();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //убираем продукт с прилавка
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (product == null )
                {
                    throw new MyException("вам нечего убрать с прилавка");
                }
                flag4 = true;
                product.ClearProduct();
                InfoProduct();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //сбор полной информации о продукте
        void InfoProduct()
        {
            if (product != null)
            {
                
                Info2.Text = "на прилавках появился продукт категории: " + goods.Category + "\nназвание: "+product.Name + " с ценой: " + goods.Price + "\nв количестве: "+ product.Weight + "\n\n";

                if (flag3)
                {
                    Info2.Text += "стоимость вашего количества продукта: "  + tmp3 + "\n\n";

                }

                if (flag4)
                {
                    Info2.Text += "продукт был убран с прилавка" + "\nтекущее кол-во продукта: " + product.Weight;

                }

            }

        }

        private void textBox8_MouseClick(object sender, MouseEventArgs e)
        {
            textBox8.Clear();
        }

        private void textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            textBox7.Clear();
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            textBox6.Clear();
        }
        #endregion

        

        #region кнопки для работы с классом игрушка

        double tmp5;
        bool flag5 = false, flag6 = false, tmp6=false;
        

        //завоз игрушек на склад
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                if (goods != null)
                {
                    if (textBox9.Text == "" || textBox10.Text == "")
                    {
                        throw new MyException("вы заполнили не все поля");
                    }
                    flag5 = false; flag6 = false;
                    toy = new Toy(goods.Category, goods.Price, Convert.ToInt32(textBox10.Text), textBox9.Text);
                    addToDel(toy);
                    InfoToy();
                }
                else
                {
                    throw new MyException("сначала нужно произвести товар");
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //скидка
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (toy != null)
                {
                    if (textBox5.Text == "")
                    {
                        throw new MyException("вы не заполнили поле- скидка");
                    }
                    if (Convert.ToInt16(textBox5.Text) < 0)
                    {
                        throw new MyException("скидка должна быть больше нуля");
                    }
                    flag5 = true;
                    tmp5 = toy.Sale(goods.Price, Convert.ToInt32(textBox5.Text));
                    InfoToy();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //проверка на возраст игрушки и ребенка
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox11.Text == "")
                {
                    throw new MyException("вы не заполнили поле возраст");

                }
                if (Convert.ToInt16(textBox11.Text) <= 0)
                {
                    throw new MyException("возраст должен быть больше 0");
                }
                flag6 = true;
                tmp6 = toy.YearToy(Convert.ToInt32(textBox11.Text));
                InfoToy();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void InfoToy()
        {
            if (toy != null)
            {

                Info3.Text = "на склад завезли игрушку с ценой: " + goods.Price + "\nвозрастными ограничениями: " + toy.Year + "\n" + "из материала: " + toy.Material +"\n\n";

                if (flag5)
                {
                    Info3.Text += "стоимость вашей игрушки со скидкой: " + tmp5 + "\n\n";

                }

                if (flag6)
                {
                    if (tmp6==true)
                    {
                        Info3.Text += "игрушка вам подходит";
                    }
                    else
                    {
                        Info3.Text += "игрушка вам не подходит";
                    }
                    

                }

            }
        }

        private void textBox10_MouseClick(object sender, MouseEventArgs e)
        {
            textBox10.Clear();
        }

        private void textBox9_MouseClick(object sender, MouseEventArgs e)
        {
            textBox9.Clear();
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.Clear();
        }

        private void textBox11_MouseClick(object sender, MouseEventArgs e)
        {
            textBox11.Clear();
        }
        #endregion


        #region работа с классом молочный продукт
        bool tmp7;double tmp8;
        bool flag7 = false, flag8 = false;


        //создание молочного продукта
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {

                if (goods != null && product != null)
                {
                    if (textBox15.Text=="" || textBox14.Text=="")
                    {
                        throw new MyException("вы заполнили не все поля");
                    }
                    flag7 = false; flag8 = false;
                    milk = new MilkProduct(goods.Category, goods.Price, product.Name, product.Weight, Convert.ToDouble(textBox15.Text), Convert.ToDateTime(textBox14.Text));
                    InfoMilk();
                }
                else
                {
                    throw new MyException("сначала нужно произвести товар и выложить продукт");
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //проверка на годность
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (milk != null)
                {
                    if (textBox13.Text == "")
                    {
                        throw new MyException("вы не заполнили поле - текущая дата");
                    }
                    flag7 = true;
                    tmp7 = milk.DateYear(Convert.ToDateTime(textBox13.Text));
                    InfoMilk();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //повышение жирности
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox12.Text == "")
                {
                    throw new MyException("вы не заполнили поле - на сколько?");
                }
                if (Convert.ToDouble(textBox12.Text) < 0)
                {
                    throw new MyException("жирность не может быть отрицательной");
                }
                flag8 = true;
                tmp8 = milk.FatnessAdd(Convert.ToDouble(textBox12.Text));
                InfoMilk();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        void InfoMilk()
        {
            if (milk != null)
            {

                Info4.Text = "вы преобрели продукт: " + product.Name + "\nс жирностью: " + milk.Fatness + "\n" + "и сроком годности: " + milk.Date + "\n\n";

                if (flag7)
                {
                    if (tmp7 == true)
                    {
                        Info4.Text += "ваш продукт не испорчен " + "\n\n";
                    }
                    else
                    {
                        Info4.Text += "ваш продукт испорчен" + "\n\n";
                    }

                }

                if (flag8)
                {
                    Info4.Text += "жирность вашего продукта повышена на " + textBox12.Text + "% \nи составляет: " + milk.Fatness + "%\n\n";
                }

            }
        }

        private void textBox15_MouseClick(object sender, MouseEventArgs e)
        {
            textBox15.Clear();
        }

        private void textBox14_MouseClick(object sender, MouseEventArgs e)
        {
            textBox14.Clear();
        }

        private void textBox13_MouseClick(object sender, MouseEventArgs e)
        {
            textBox13.Clear();
        }

        private void textBox12_MouseClick(object sender, MouseEventArgs e)
        {
            textBox12.Clear();
        }
        #endregion

        //полиморфизм(ляпаем штрих-коды),интерфейс1
        private void button13_Click(object sender, EventArgs e)
        {

            if (goods != null || toy != null || product != null)
            {
                if (checkBox1.Checked == true && goods != null)
                {
                    Info.Text += "вы наляпали штрих-код: " + Convert.ToString(goods.Barcode()) + "\n\n";
                }
                
                if (checkBox2.Checked == true && product!=null)
                {
                    Info2.Text += "вы наляпали штрих-код: " + product.Barcode() + "\n\n";
                }
                if (checkBox3.Checked == true && toy!=null)
                {
                    Info3.Text += "вы наляпали штрих-код: " + toy.Barcode() + "\n\n";
                }
            }
            else
            {
                MessageBox.Show("не на что ляпать");
            }
        }

        //месим простоквашу (интерфейс2)
        private void button14_Click(object sender, EventArgs e)
        {
            if (milk != null && checkBox4.Checked == true)
            {
                milk.Clabber();
                Info4.Text += "вы замесили простоквашу\n\n";

            }

        }

        //наляпываем штрих-код везде сразу (делегат)
        private void button15_Click(object sender, EventArgs e)
        {
            if (deleg != null)
            {
                deleg();
            }
        }

       

        











    }
}
