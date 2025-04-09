using IronCenter.Desktop.Controllers;
using IronCenter.Desktop.DbContexts;
using IronCenter.Desktop.Windows.Products;
using IronCenter.Desktop.Windows.Storages;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using IronCenter.Service.Domain.Storages;
using Microsoft.Identity.Client.Extensions.Msal;

namespace IronCenter.Desktop.Pages.Storages
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Storages : Page
    {
        public Storages()
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
                wrpCourses.Children.Clear();

                var storagies = await dbContext.Storages.ToListAsync();
                long count = storagies.Count;
                foreach (var storage in storagies.AsEnumerable().Reverse())
                {
                    txbProductCount.Text = count.ToString() + " turdagi mahsulot mavjud";
                    StorageController Controller = new StorageController(this, storage);
                    Controller.SetData(storage);
                    wrpCourses.Children.Add(Controller);
                }
            }
            ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageCreateWindow storagecreateWindow = new StorageCreateWindow();
            storagecreateWindow.Show();
            storagecreateWindow.Closed += (s, e) => Refresh();
        }

        private async void TextBoxSearch_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
          //  await SearchAsync();
        }

        private async void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
           await SearchAsync();
        }

        public async Task SearchAsync()
        { 
            try
            {
                var searchQuery = TextBoxSearch.Text;
                using (var dbContext = new AppDbContext())
                {
                         
                         var products = await dbContext.Products
                        .Where(p => EF.Functions.ILike(p.Name, $"%{searchQuery}%")
                                 || EF.Functions.ILike(p.CategoryName, $"%{searchQuery}%"))
                        .ToListAsync();


                    wrpCourses.Children.Clear();
                    int count = 0;
                    foreach (var product in products.AsEnumerable().Reverse())
                    {
                        var storages = await dbContext.Storages
                                                .Where(s => s.ProductId == product.Id)
                                                .ToListAsync();
                        foreach (var storage in storages)
                        {

                            StorageController Controller = new StorageController(this, storage);
                            Controller.SetData(storage);
                            wrpCourses.Children.Add(Controller);
                            count++;
                        }
                    }
                    txbProductCount.Text = count.ToString() + " natija mavjud";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
