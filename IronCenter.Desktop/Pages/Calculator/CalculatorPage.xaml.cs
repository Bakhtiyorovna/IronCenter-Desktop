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
    /// Interaction logic for CalculatorPage.xaml
    /// </summary>
    public partial class CalculatorPage : Page
    {
        private Window _mainWindow;
        public CalculatorPage(Window mainWindow)
        {
            InitializeComponent();
            _mainWindow= mainWindow;
        }

        private void Radiobutton1_Click(object sender, RoutedEventArgs e)
        {
            ArmaturaCalculator calculator = new ArmaturaCalculator();
            FrameFilter.Content=calculator;

        }

        private void Radiobutton2_Click(object sender, RoutedEventArgs e)
        {
            KruglayaTrubaCalculator calculator = new KruglayaTrubaCalculator();
            FrameFilter.Content=calculator;
        }

        private void Radiobutton3_Click(object sender, RoutedEventArgs e)
        {
            ProfilnayaTrupaCalculator profilnayaTrupaCalculator = new ProfilnayaTrupaCalculator();
            FrameFilter.Content = profilnayaTrupaCalculator;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ArmaturaCalculator calculator = new ArmaturaCalculator();
            FrameFilter.Content = calculator;
        }

        private void Radiobutton4_Click(object sender, RoutedEventArgs e)
        {
            ListPage listPage = new ListPage();
            FrameFilter.Content=listPage;
        }

        private void Radiobutton6_Click(object sender, RoutedEventArgs e)
        {
            HistoryPage historyPage = new HistoryPage(_mainWindow);
            FrameFilter.Content=historyPage;
        }
    }
}
