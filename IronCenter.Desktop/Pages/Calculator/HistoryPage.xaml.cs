using IronCenter.Desktop.Controllers.Calculator;
using IronCenter.Service.CalculaturServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        private Window _mainWindow;
        string filePath = "D:\\Proekts\\DotNet\\IronCenter-Desktop\\src\\IronCenter.Service\\CalculaturServices\\CalculatorHistoryJson.json";
        public HistoryPage(Window mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow; // Oynani saqlaymiz
            Loaded += Page2_Loaded;
            _mainWindow.StateChanged += MainWindow_StateChanged;
        }

        private void Page2_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSize();
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            if (_mainWindow.WindowState == WindowState.Maximized)
            {
                this.Width = 748;
                this.Height = 590;
            }
            else
            {
                this.Width = 668;
                this.Height = 520;
            }
        }
    

        public async Task Refresh()
        {
            wrpCourses.Children.Clear();

            string value = File.ReadAllText("D:\\Proekts\\DotNet\\IronCenter-Desktop\\src\\IronCenter.Service\\CalculaturServices\\CalculatorHistoryJson.json");

            // JSON ma'lumotlarini C# obyektiga aylantirish
            var values = JsonConvert.DeserializeObject<List<CalculatorHistory>>(value);

            if (values != null)
            {
                foreach (var v in values)
                {
                    CalculatorHistoryControl UserControl = new CalculatorHistoryControl();
                    UserControl.SetData(v);
                    wrpCourses.Children.Add(UserControl);
                }
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        private void page_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void MyPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (_mainWindow.WindowState == WindowState.Maximized)
            {
                this.Width = 400;
                this.Height = 400;
            }
            else
            {
                this.Width = 300;
                this.Height = 300;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(filePath, "[]");
            wrpCourses.Children.Clear();
        }
    }
}
