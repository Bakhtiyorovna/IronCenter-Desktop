using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using SkiaSharp;
using System.ComponentModel;
using LiveCharts;
using IronCenter.Desktop.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Runtime.CompilerServices; // Rasm va ranglar uchun

namespace IronCenter.Desktop.Pages.Dashboard
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private ChartValues<int> _sotuvlar;
        public ChartValues<int> Sotuvlar
        {
            get { return _sotuvlar; }
            set
            {
                _sotuvlar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sotuvlar)));
            }
        }

        private List<string> _sanalar;
        public List<string> Sanalar
        {
            get { return _sanalar; }
            set
            {
                _sanalar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sanalar)));
            }
        }

        public Dashboard()
        {
            InitializeComponent();
            this.DataContext = this; 
            GenerateChartData();
            LoadPieChartData();
        }

        private void GenerateChartData()
        {
            Sotuvlar = new ChartValues<int>();
            Sanalar = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                DateTime sana = DateTime.Today.AddDays(i);
                int sotuvlarSoni = new Random().Next(10, 100);

                Sotuvlar.Add(sotuvlarSoni);
                Sanalar.Add(sana.ToString("dd.MM"));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Info();
        }

        public async void Info()
        {
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var products = await dbContext.Products.ToListAsync();
                    var categorys = await dbContext.Categories.ToListAsync();
                    int productValue = 0;
                    for (int i = 0; i < products.Count; i++)
                    {
                        productValue += products[i].Value;
                    }
                    if (products.Count > 0)
                    {
                        txbProductCCount.Text = products.Count.ToString();
                    }
                    if(productValue > 0)
                    {
                        TxbProductValue.Text = productValue.ToString();
                    }
                    if(categorys.Count > 0)
                    {
                        TxbCategoryValue.Text = categorys.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task LoadPieChartData()
        {
            using (var dbContext = new AppDbContext())
            {
                var products = await dbContext.Products.ToListAsync();

                Dictionary<string, int> dict = new Dictionary<string, int>();

                foreach (var product in products)
                {
                    var storages = await dbContext.Storages.Where(ea => ea.ProductId == product.Id).ToListAsync();
                    int salevalue = 0;
                    if (storages != null)
                    {
                        
                        for (int i = 0; i < storages.Count; i++)
                        {
                            salevalue += storages[i].Quantity - storages[i].PeresentValue;
                        }
                    }
                    dict.Add(product.Name, salevalue);
                }

                var valueOne = dict.OrderByDescending(x => x.Value).First();
                LblValeuOne.Values = new ChartValues<double> { Convert.ToDouble(valueOne.Value) };
                TxbValueone.Text = LblValeuOne.Title = $"{valueOne.Key}";
                dict.Remove(valueOne.Key);

                var valueTwo = dict.OrderByDescending (x => x.Value).First();
                LblValueTwo.Values = new ChartValues<double> { Convert.ToDouble(valueTwo.Value) };
                TxbValueTwo.Text = LblValueTwo.Title = $"{valueTwo.Key}";
                dict.Remove(valueTwo.Key);

                var valueThree = dict.OrderByDescending (x => x.Value).First();
                LblValueThree.Values = new ChartValues<double> { Convert.ToDouble(valueThree.Value) };
                TxbValueThree.Text = LblValueThree.Title = $"{valueThree.Key}";
                dict.Remove(valueThree.Key);

                var valueFour = dict.OrderByDescending(x => x.Value).First();
                LblValueFour.Values = new ChartValues<double> { Convert.ToDouble(valueFour.Value) };
                TxbValueFour.Text = LblValueFour.Title = $"{valueFour.Key}";
                dict.Remove(valueFour.Key);

                DataContext = this; 
            }
            
        }
    }
}