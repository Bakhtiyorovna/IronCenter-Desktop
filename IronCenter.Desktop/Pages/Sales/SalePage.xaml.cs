using IronCenter.Desktop.Controllers;
using IronCenter.Desktop.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using IronCenter.Desktop.Controllers.Sale;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Domain.Sales;
using IronCenter.Service.Enums;
using IronCenter.Service.Domain.Storages;

namespace IronCenter.Desktop.Pages.Sales
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        public SalePage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        public async Task Refresh()
        {
            await using (var dbContext = new AppDbContext())
            {
                decimal USD = await IronCenter.Desktop.Helpers.GetUsdRate.GetUsdRateFromCbuAsync();
                decimal income;
                decimal benefit;
                decimal damage;

                decimal totalIncome= 0;
                decimal totalBenefit= 0;
                decimal totalDamage = 0;

                var product = new Product();
                var storage = new Storage();

                var sales = await dbContext.Sales.ToListAsync();

                int no = 1;
                wrpCourses.Children.Clear();

                foreach (var sale in sales.AsEnumerable().Reverse())
                {
                    SaleController Controller = new SaleController();
                    Controller.SetData(sale, no++);
                    wrpCourses.Children.Add(Controller);

                    storage = await dbContext.Storages
                               .FirstOrDefaultAsync(s => s.Id == sale.StorageId);
                    product = await dbContext.Products
                                    .FirstOrDefaultAsync(s => s.Id == storage.ProductId);
                    if (sale.CreatedAt.Month == DateTime.Now.Month)
                    {
                        decimal arrivalprice = storage.UnitPrice;
                        decimal saleprice = sale.UnitPrice;
                        if (storage.Сurrency == Currency.UZS) // So'mda bo'lsa
                        {
                            arrivalprice = arrivalprice / USD;
                        }
                        if (sale.Сurrency == Currency.UZS)
                        {
                            saleprice = saleprice / USD;
                        }

                        income = Math.Round(saleprice * sale.Quantity, 2); // daromad
                        benefit = Math.Round(income - sale.Quantity * arrivalprice, 2);

                        if (benefit < 0)
                        {
                            damage = benefit;
                            totalDamage += damage;
                        }
                        else
                        {
                            totalBenefit += benefit;
                        }

                            totalIncome += income;
                    }
                }

                txbIncome.Text = Math.Round(totalIncome, 2).ToString()+ " USD";
                txbBenefit.Text = Math.Round(totalBenefit, 2).ToString()+" USD";
                txbZarar.Text = Math.Round(totalDamage, 2).ToString()+ " USD";
            }
            ;
        }

      
    }
}
