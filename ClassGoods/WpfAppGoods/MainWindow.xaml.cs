using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassGoods;

namespace WpfAppGoods
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        double tmp, tmp2;
        bool flag = false, flag1 = false;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            flag = false; flag1 = false;
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    throw new MyException("вы заполнили не все поля");
                }
                goods = new Goods(textBox1.Text, Convert.ToDouble(textBox2.Text));
                addToDel(goods);
                InfoGoods();
            }

            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
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

        private void button3_Click(object sender, RoutedEventArgs e)
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


        void InfoGoods()
        {
            if (goods != null)
            {
                Info.Content = "произведен товар категории: " + goods.Category + "\n" + "с ценой: " + goods.Price + "\n\n";

                if (flag)
                {
                    Info.Content += "на склад прибыл товар в количестве: " + textBox3.Text + " " + "штук. " + "\n" + "всего товара: " + tmp + "\n\n";

                }

                if (flag1)
                {
                    Info.Content += "общая цена за " + textBox4.Text + " " + "штук: " + tmp2 + "\n\n";

                }

            }

        }

        #endregion


        #region кнопки для работы с классом продукт

        bool flag3 = false, flag4 = false;
        double tmp3;

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (goods != null)
                {
                    if (textBox5.Text == "" || textBox6.Text == "")
                    {
                        throw new MyException("вы заполнили не все поля");
                    }
                    flag3 = false; flag4 = false;
                    product = new Product(goods.Category, goods.Price, textBox5.Text, Convert.ToInt32(textBox6.Text));
                    addToDel(product);
                    InfoProduct();
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

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (product != null)
                {
                    if (textBox7.Text == "")
                    {
                        throw new MyException("вы заполнили не все поля");
                    }
                    if (Convert.ToInt16(textBox7.Text) < 0)
                    {
                        throw new MyException("вес должен быть больше нуля");
                    }
                    flag3 = true;
                    tmp3 = product.calculate(goods.Price, Convert.ToDouble(textBox7.Text));
                    InfoProduct();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (product == null)
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

                Info2.Content = "на прилавках появился продукт категории: " + goods.Category + "\nназвание: " + product.Name + " с ценой: " + goods.Price + "\nв количестве: " + product.Weight + "\n\n";

                if (flag3)
                {
                    Info2.Content += "стоимость вашего количества продукта: " + tmp3 + "\n\n";

                }

                if (flag4)
                {
                    Info2.Content += "продукт был убран с прилавка" + "\nтекущее кол-во продукта: " + product.Weight + "\n\n";

                }

            }

        }

        #endregion


        #region кнопки для работы с классом игрушка

        double tmp5;
        bool flag5 = false, flag6 = false, tmp6 = false;

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (goods != null)
                {
                    if (textBox8.Text == "" || textBox9.Text == "")
                    {
                        throw new MyException("вы заполнили не все поля");
                    }
                    flag5 = false; flag6 = false;
                    toy = new Toy(goods.Category, goods.Price, Convert.ToInt32(textBox8.Text), textBox9.Text);
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

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (toy != null)
                {
                    if (textBox10.Text == "")
                    {
                        throw new MyException("вы не заполнили поле- скидка");
                    }
                    if (Convert.ToInt16(textBox10.Text) < 0)
                    {
                        throw new MyException("скидка должна быть больше нуля");
                    }
                    flag5 = true;
                    tmp5 = toy.Sale(goods.Price, Convert.ToInt32(textBox10.Text));
                    InfoToy();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, RoutedEventArgs e)
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

                Info3.Content = "на склад завезли игрушку с ценой: " + goods.Price + "\nвозрастными ограничениями: " + toy.Year + "\n" + "из материала: " + toy.Material + "\n\n";

                if (flag5)
                {
                    Info3.Content += "стоимость вашей игрушки со скидкой: " + tmp5 + "\n\n";

                }

                if (flag6)
                {
                    if (tmp6 == true)
                    {
                        Info3.Content += "игрушка вам подходит\n\n";
                    }
                    else
                    {
                        Info3.Content += "игрушка вам не подходит\n\n";
                    }


                }

            }
        }

        #endregion


        #region работа с классом молочный продукт
        
        bool tmp7; double tmp8;
        bool flag7 = false, flag8 = false;

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (goods != null && product != null)
                {
                    if (textBox12.Text == "" || textBox13.Text == "")
                    {
                        throw new MyException("вы заполнили не все поля");
                    }
                    flag7 = false; flag8 = false;
                    milk = new MilkProduct(goods.Category, goods.Price, product.Name, product.Weight, Convert.ToDouble(textBox12.Text), Convert.ToDateTime(textBox13.Text));
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

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (milk != null)
                {
                    if (textBox14.Text == "")
                    {
                        throw new MyException("вы не заполнили поле - текущая дата");
                    }
                    flag7 = true;
                    tmp7 = milk.DateYear(Convert.ToDateTime(textBox14.Text));
                    InfoMilk();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBox15.Text == "")
                {
                    throw new MyException("вы не заполнили поле - на сколько?");
                }
                if (Convert.ToDouble(textBox15.Text) < 0)
                {
                    throw new MyException("жирность не может быть отрицательной");
                }
                flag8 = true;
                tmp8 = milk.FatnessAdd(Convert.ToDouble(textBox15.Text));
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

                Info4.Content = "вы преобрели продукт: " + product.Name + "\nс жирностью: " + milk.Fatness + "\n" + "и сроком годности: " + milk.Date + "\n\n";

                if (flag7)
                {
                    if (tmp7 == true)
                    {
                        Info4.Content += "ваш продукт не испорчен " + "\n\n";
                    }
                    else
                    {
                        Info4.Content += "ваш продукт испорчен" + "\n\n";
                    }

                }

                if (flag8)
                {
                    Info4.Content += "жирность вашего продукта повышена на " + textBox15.Text + "% \nи составляет: " + milk.Fatness + "%\n\n";
                }

            }
        }

        #endregion

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (goods != null || toy != null || product != null)
            {
                if (checkBox1.IsChecked == true && goods != null)
                {
                    Info.Content += "вы наляпали штрих-код: " + Convert.ToString(goods.Barcode()) + "\n\n";
                }

                if (checkBox2.IsChecked == true && product != null)
                {
                    Info2.Content += "вы наляпали штрих-код: " + product.Barcode() + "\n\n";
                }
                if (checkBox3.IsChecked == true && toy != null)
                {
                    Info3.Content += "вы наляпали штрих-код: " + toy.Barcode() + "\n\n";
                }
            }
            else
            {
                MessageBox.Show("не на что ляпать");
            }
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            if (milk != null && checkBox4.IsChecked == true)
            {
                milk.Clabber();
                Info4.Content += "вы замесили простоквашу\n\n";

            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (deleg != null)
            {
                deleg();
            }
        }

        private void textBox1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox3.Clear();
        }

        private void textBox4_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox4.Clear();
        }

        private void textBox5_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox5.Clear();
        }

        private void textBox6_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox6.Clear();
        }

        private void textBox7_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox7.Clear();
        }

        private void textBox8_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox8.Clear();
        }

        private void textBox9_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox9.Clear();
        }

        private void textBox10_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox10.Clear();
        }

        private void textBox11_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox11.Clear();
        }

        private void textBox12_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox12.Clear();
        }

        private void textBox13_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox13.Clear();
        }

        private void textBox14_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox14.Clear();
        }

        private void textBox15_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox15.Clear();
        }

        
        
        

        
    }
}
