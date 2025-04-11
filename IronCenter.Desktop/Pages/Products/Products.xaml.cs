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
using static System.Net.Mime.MediaTypeNames;

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
            List<string> pictures = new List<string> {
           "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\ProductImages\\ArmaturaPicture.jpg",
           "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\ProductImages\\Uzaytirgich.jpg",
           "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\ProductImages\\List.jpg",
           "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\ProductImages\\Dquvur.jpg",
           "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\ProductImages\\Uzaytirgich.jpg"
                };
            await using (var dbContext = new AppDbContext())
            {
                wrpCourses.Children.Clear();

                var products = await dbContext.Products.ToListAsync();
                long count = products.Count;
                int i= 0;
                foreach (var product in products.AsEnumerable().Reverse())
                {
                    product.ImagePath = pictures[i];
                    i++;
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
                    
                        long count = products.Count;
                        foreach (var product in products.AsEnumerable().Reverse())
                        {
                        product.ImagePath =
       "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\ProductImages\\ArmaturaPicture.jpg";
                            txbProductCount.Text = count.ToString() + " ta natija mavjud";
                            ProductController productController = new ProductController(this);
                            productController.SetData(product);
                            wrpCourses.Children.Add(productController);
                        }
                        loader.Foreground = new SolidColorBrush(Colors.Transparent);
                    if (products.Count == 0)
                    {
                        loader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4"));
                    
                    }
                    else
                    {
                        loader.Foreground = new SolidColorBrush(Colors.Transparent);
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
