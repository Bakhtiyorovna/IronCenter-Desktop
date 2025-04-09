using IronCenter.Desktop.Pages.Calculator;
using IronCenter.Desktop.Pages.Dashboard;
using IronCenter.Desktop.Pages.Dashboard.Products;
using IronCenter.Desktop.Pages.Sales;
using IronCenter.Desktop.Pages.Storages;
using IronCenter.Service;
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

namespace IronCenter.Desktop
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        bool IsMaximized = false;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    IsMaximized = false;
                }

                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }

            }
        }

        private void btnMinimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximized_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else WindowState = WindowState.Maximized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AsosiyButton_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = "Qoldiq mahsulotlar";

            Ostatka ostatka = new Ostatka();
            FrameFilter.Content = (ostatka);
        }

        private void MahsulotlarButton_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = "Mahsulotlar";

            Products product =new Products();
            FrameFilter.Content= product;

        }

        private void KalkulatorButton_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = " Metall Kalkulator";

            CalculatorPage page = new CalculatorPage(this);
            FrameFilter.Content =(page);
        }

        public async Task  UpdatePage()
        {
            // Page'ni yangilash uchun kerakli amalni bajarish
            var page = this.Content as Products;
            if (page != null)
            {
               await page.Refresh();
            }
        }

        private void OmborButton_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = "Ombor";

            Storages storage = new Storages();
            FrameFilter.Content=(storage);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = "Asosiy";

            Dashboard dashboard = new Dashboard();
            FrameFilter.Content = dashboard;
        }

        private void SaleButton_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = "Sotuvlar ro'yxati";

            SalePage sale = new SalePage();
            FrameFilter.Content =(sale);
        }
    }
}