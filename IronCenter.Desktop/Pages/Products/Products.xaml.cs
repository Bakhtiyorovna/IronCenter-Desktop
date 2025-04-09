using IronCenter.Desktop.Controllers;
using IronCenter.Desktop.DbContexts;
using IronCenter.Desktop.Windows.Products;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IronCenter.Desktop.Pages.Dashboard.Products
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public Products()
        {
            InitializeComponent();
            Refresh();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        public async Task  Refresh()
        {
            await using (var dbContext = new AppDbContext())
            {
                wrpCourses.Children.Clear();

                var products = await dbContext.Products.ToListAsync();
                long count = products.Count;
                foreach (var product in products.AsEnumerable().Reverse())
                { 
                    txbProductCount.Text = count.ToString()+" turdagi mahsulot mavjud";
                    ProductController productController = new ProductController(this);
                    productController.SetData(product);
                    wrpCourses.Children.Add(productController);
                }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var productCreateWindow = new ProductCreateWindow();
            productCreateWindow.Show();
            productCreateWindow.Closed += (s, e) => Refresh();
        }

        private async void TextBoxSearch_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            await SearchAsync();
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
                    if (products.Count > 0)
                    {
                        long count = products.Count;
                        foreach (var product in products.AsEnumerable().Reverse())
                        {
                            txbProductCount.Text = count.ToString() + " turdagi mahsulot mavjud";
                            ProductController productController = new ProductController(this);
                            productController.SetData(product);
                            wrpCourses.Children.Add(productController);
                        }
                        loader.Foreground = new SolidColorBrush(Colors.Transparent);
                    }
                    else loader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4"));
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
