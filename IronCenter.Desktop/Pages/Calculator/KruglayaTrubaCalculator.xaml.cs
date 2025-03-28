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
    /// Interaction logic for KruglayaTrubaCalculator.xaml
    /// </summary>
    public partial class KruglayaTrubaCalculator : Page
    {
        public double Length = 0;
        public double Weight = 0;
        public double d;
        public double t;
        public double info;
        public double density = 0.007848107;

        public double result = 0;

        public KruglayaTrubaCalculator()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            Title_Txb.Text = "Dumaloq quvur uzunligini hisoblash";

            Length = 1;
            Weight = 0;

            infoTxt.Text = "";

        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            Title_Txb.Text = "Dumaloq quvur og'irligini hisoblash";

            Length = 0;
            Weight = 1;

            infoTxt.Text = "";

        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            d=Convert.ToDouble(diometrTxt.Text);
            t=Convert.ToDouble(tTxt.Text);
            info=Convert.ToDouble(infoTxt.Text);

            if(Weight==1 && d>0 && t>0 && info > 0 & d/2>t||Length !=1 && d > 0 && t > 0 && info > 0 & d / 2 > t)
            {
                result = (Math.Pow((d/2),2)*Math.PI-Math.Pow((d/2-t),2)*Math.PI)*info*density;

                if (result > 0)
                {
                       ResultLbl.Content = "og'irlik: "+Math.Round(result,3)+"kg";
                       ResultLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4"));
                }
                else
                {
                    ResultLbl.Content = "Ma'lumotlarni e'tibor bilan kiriting!";
                    ResultLbl.Foreground = new SolidColorBrush(Colors.Red);
                }

            }
            else if(Length==1 && d > 0 && t > 0 && info > 0 & d / 2 > t || Weight !=1 && d > 0 && t > 0 && info > 0 & d / 2 > t)
            {
                result = info / ((Math.Pow((d / 2), 2) * Math.PI - Math.Pow((d / 2 - t), 2) * Math.PI) * density);
                if (result > 0)
                {
                    ResultLbl.Content = "uzunlik: " + Math.Round(result, 3) + "m";
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
    }
}
