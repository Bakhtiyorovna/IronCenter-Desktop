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
using Microsoft.EntityFrameworkCore; // Rasm va ranglar uchun

namespace IronCenter.Desktop.Pages.Dashboard
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ChartValues<double> TemirValues { get; set; }
        public ChartValues<double> MisValues { get; set; }
        public ChartValues<double> NikelValues { get; set; }
        public ChartValues<double> AlyuminiyValues { get; set; }

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

                    if (products.Count > 0)
                    {
                        txbProductCCount.Text = products.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LoadPieChartData()
        {
            TemirValues = new ChartValues<double> { 40 };
            MisValues = new ChartValues<double> { 30 };
            NikelValues = new ChartValues<double> { 20 };
            AlyuminiyValues = new ChartValues<double> { 10 };

            DataContext = this; 
        }
    }
}