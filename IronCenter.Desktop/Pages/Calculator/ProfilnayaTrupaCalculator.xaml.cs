using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IronCenter.Desktop.Pages.Calculator
{
    /// <summary>
    /// Interaction logic for ProfilnayaTrupaCalculator.xaml
    /// </summary>
    public partial class ProfilnayaTrupaCalculator : Page
    {

        public double Length = 0;
        public double Weight = 0;

        public double a;
        public double b;
        public double t;
        public double info;
        public double density = 0.00766666;

        public double result = 0;
        public ProfilnayaTrupaCalculator()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            Title_Txb.Text = "Turtburchak quvur uzunligini hisoblash";

            Length = 1;
            Weight = 0;

            infoTxt.Text ="";
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            Title_Txb.Text = "Tor'tburchak quvur og'irligini hisoblash";

            Length = 0;
            Weight = 1;

            infoTxt.Text = "";

        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            a=Convert.ToDouble(aTxt.Text);
            b=Convert.ToDouble(bTxt.Text);
            t=Convert.ToDouble(tTxt.Text);

            info=Convert.ToDouble(infoTxt.Text);

            if(Weight==1 & a>0 & b>0 & t>0 || Length != 1 & a > 0 & b > 0 & t > 0)
            {
                result = (b * a - (b - 2*t) * (a -2* t)) * density * info;
                if (result > 0) 
                {
                ResultLbl.Content = "Og'irlik: " + Math.Round(result, 3) + "kg";
                ResultLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4")); 
                }
                else
                {
                    ResultLbl.Content = "Ma'lumotlarni e'tibor bilan kiriting!";
                    ResultLbl.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
            else if (Length == 1 & a > 0 & b > 0 & t > 0 || Weight != 1 & a > 0 & b > 0 & t > 0)
            {
                result =info /( (b * a - (b - 2*t) * (a - 2*t)) * density );
                if (result > 0)
                {
                    ResultLbl.Content = "Uzunlik: " + Math.Round(result, 3) + "m";
                    ResultLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4"));
                }
                else
                {
                    ResultLbl.Content = "Ma'lumotlarni e'tibor bilan kiriting!";
                    ResultLbl.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
            else
            {
                ResultLbl.Content = "Ma'lumotlarni e'tibor bilan kiriting!";
                ResultLbl.Foreground = new SolidColorBrush(Colors.Red);
            }

        }

        private void aTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
