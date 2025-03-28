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

        public HistoryPage()
        {
            InitializeComponent();
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
    }
}
