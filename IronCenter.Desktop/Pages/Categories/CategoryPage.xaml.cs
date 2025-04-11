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
using System.Windows.Media;
using IronCenter.Desktop.Windows.Categories;
using IronCenter.Desktop.Controllers.Categories;

namespace IronCenter.Desktop.Pages.Categories
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        public CategoryPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

       
        public async Task Refresh()
        {
            List<string> pictures = new List<string> { "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\CategoryImages\\maishiy.jpg",
                "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\CategoryImages\\metallurgiya.jpg",
                "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\CategoryImages\\elektronika.jpg" };


            loader.Foreground = new SolidColorBrush(Colors.Transparent);
            await using (var dbContext = new AppDbContext())
            {
                wrpCourses.Children.Clear();

                var categories = await dbContext.Categories.ToListAsync();
                long count = categories.Count;
                int i = 0;
                foreach (var category in categories.AsEnumerable().Reverse())
                {
                    category.ImagePath = pictures[i];
                    i++;
                    txbProductCount.Text = count.ToString() + " ta kategoriya mavjud";
                    CategoryController Controller = new CategoryController(category);
                    Controller.SetData(category );
                    wrpCourses.Children.Add(Controller);
                }
            }
            ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CategoryCreateWindow createWindow = new CategoryCreateWindow();
            createWindow.Show();
            createWindow.Closed += (s, e) => Refresh();
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

                    var categories = await dbContext.Categories
                   .Where(p => EF.Functions.ILike(p.Name, $"%{searchQuery}%")
                            || EF.Functions.ILike(p.Description, $"%{searchQuery}%"))
                   .ToListAsync();


                    wrpCourses.Children.Clear();
                    foreach (var category in categories)
                    {
                        category.ImagePath = "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\CategoryImages\\metallurgiya.jpg";

                        CategoryController Controller = new CategoryController(category);
                        Controller.SetData(category);
                        wrpCourses.Children.Add(Controller);
                    }
                    int count = categories.Count;
                    if(count == 0)
                    {
                        loader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4"));
                    }
                    else
                    {
                        loader.Foreground = new SolidColorBrush(Colors.Transparent);
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
