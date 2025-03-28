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
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IronCenter.Desktop.Pages.Calculator
{
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public double Square;
        public double Weight;

        public double a;
        public double b;
        public double t;
        public double value;

        
        public double density = 0.007848107;

        public ListPage()
        {
            InitializeComponent();
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            a = Convert.ToDouble(aTxt.Text);
            b = Convert.ToDouble(bTxt.Text);
            t= Convert.ToDouble(tTxt.Text);
            value=Convert.ToDouble(miqdorTxt.Text);

            Square = a * b * value;
            Weight = a* b * t * density * value/1000;

            if(Square>0 & Weight > 0)
            {
                YuzaLbl.Content="Yuza: "+Math.Round(Square,3)+ "m2";
                YuzaLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4"));

                OgirlikLbl.Content = "Og'irlik: " + Math.Round(Weight, 3) + "kg";
                OgirlikLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4"));
            }
            else
            {
                OgirlikLbl.Content = "Ma'lumotlarni e'tibor bilan kiriting!";
                OgirlikLbl.Foreground = new SolidColorBrush(Colors.Red);

                YuzaLbl.Content = "Ma'lumotlarni e'tibor bilan kiriting!";
                YuzaLbl.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
