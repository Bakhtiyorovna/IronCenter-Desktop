using IronCenter.Desktop.DbContexts;
using IronCenter.Desktop.Pages.Dashboard.Products;
using IronCenter.Desktop.Windows.Products;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Domain.Sales;
using IronCenter.Service.Domain.Storages;
using IronCenter.Service.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IronCenter.Desktop.Controllers.Sale
{
    /// <summary>
    /// Interaction logic for ProductController.xaml
    /// </summary>
    public partial class SaleController : UserControl
    {
        public IronCenter.Service.Domain.Sales.Sale _sale;
        public decimal income;
        public decimal benefit;
        public decimal USD;

        public SaleController()
        {
            InitializeComponent();
             SizeAsync();
        }

        public async Task SizeAsync()
        {
          var mainWindow = Application.Current.MainWindow;



            var window = Window.GetWindow(mainWindow);
            if (window != null)
            {
                window.StateChanged += (s, e) =>
                {
                    var currentState = window.WindowState;
                    if (currentState == WindowState.Maximized)
                    {
                        this.Width = 945;
                    }
                    else
                    {
                        this.Width = 865;
                    }
                };
            }
        }

        public async void SetData(IronCenter.Service.Domain.Sales.Sale sale, int number)
        {
            USD = await IronCenter.Desktop.Helpers.GetUsdRate.GetUsdRateFromCbuAsync();
            var storage = new Storage();
            var product = new Product();
            using (var dbContext = new AppDbContext())
            {
                 storage = await dbContext.Storages
                                  .FirstOrDefaultAsync(s => s.Id == sale.StorageId);
                 product = await dbContext.Products
                                 .FirstOrDefaultAsync(s => s.Id == storage.ProductId);
                
            }

            txbNo.Text = number.ToString();
            txbproductName.Text = product.Name;  
            txbArrivilDate.Text = sale.CreatedAt.ToString("dd.MM.yyyy");
            txbArrivalPrice.Text = storage.UnitPrice.ToString() + " " + storage.Сurrency.ToString();  // kelgan narxi , kelgan narx birligi
            txbSaleQuintity.Text = sale.Quantity.ToString()+" "+ storage.Unitary.ToString();          // sotilgan soni
            txbSalePrice.Text = sale.UnitPrice.ToString()+" " + sale.Сurrency.ToString();     //         sotilgan narxi, sotilgan narx birligi
            decimal arrivalprice = storage.UnitPrice; 
            decimal saleprice = sale.UnitPrice;
            if (storage.Сurrency == Currency.UZS ) // So'mda bo'lsa
            {
                arrivalprice = arrivalprice  / USD;
            }
            if(sale.Сurrency == Currency.UZS)
            {
                saleprice = saleprice  / USD;
            }

            income = Math.Round( saleprice * sale.Quantity, 2); // daromad
            benefit = Math.Round(income - sale.Quantity * arrivalprice,2);




            txbSaleincome.Text = income.ToString()+" USD";

            if (benefit < 0)
            {
                txbSalebenefit.Foreground = new SolidColorBrush(Colors.Red);
                txbSalebenefit.Text = benefit.ToString() + " USD";
            }
            else
            {
                txbSalebenefit.Text = benefit.ToString() + " USD";
            }
        }

        public async Task DeleteAsync(){
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
