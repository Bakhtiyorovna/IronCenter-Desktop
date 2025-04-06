using IronCenter.Desktop.Controllers;
using IronCenter.Desktop.Windows.Products;
using IronCenter.Desktop.Windows.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        public async Task Refresh()
        {
            ProductController productController = new ProductController();
            wrpCourses.Children.Add(productController);
            ProductController productController1 = new ProductController();
            wrpCourses.Children.Add(productController1);
            ProductController productController2 = new ProductController();
            wrpCourses.Children.Add(productController2);
            ProductController productController3 = new ProductController();
            wrpCourses.Children.Add(productController3);
            ProductController productController4 = new ProductController();
            wrpCourses.Children.Add(productController4);
            ProductController productController5 = new ProductController();
            wrpCourses.Children.Add(productController5);
            ProductController productController6 = new ProductController();
            wrpCourses.Children.Add(productController6);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductCreateWindow window = new ProductCreateWindow();
            window.Show();
        }
    }
}
