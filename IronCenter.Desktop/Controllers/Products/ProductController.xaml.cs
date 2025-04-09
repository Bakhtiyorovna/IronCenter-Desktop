using IronCenter.Desktop.DbContexts;
using IronCenter.Desktop.Pages.Dashboard.Products;
using IronCenter.Desktop.Windows.Products;
using IronCenter.Service.Domain.Products;
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

namespace IronCenter.Desktop.Controllers
{
    /// <summary>
    /// Interaction logic for ProductController.xaml
    /// </summary>
    public partial class ProductController : UserControl
    {
        public Products products;
        public  ProductController(Products product)
        {
            InitializeComponent();
            this.products = products;
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
                        this.Width = 940;
                    }
                    else
                    {
                        this.Width = 865;
                    }
                };
            }
        }

        public Func<Task> Refresh { get; set; }
        public void SetData(Product product)
        {
            txbName.Text = product.Name;
            txbCategoryName.Text = product.CategoryName;
            txbValue.Text = product.Value.ToString()+" dona mavjud";
            txbId.Text = product.Id.ToString();
            txbCategoryId.Text = product.CategoryId.ToString();
        }

        public async Task DeleteAsync(){
           
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Id = Convert.ToInt64(txbId.Text);
            product.Name = txbName.Text;
            product.CategoryId = Convert.ToInt64(txbCategoryId.Text);
            product.CategoryName = txbCategoryName.Text;
            ProductDeleteWindow productWindow = new ProductDeleteWindow(this, product);
            productWindow.ShowDialog();
            productWindow.Closed += (s, e) => Refresh();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Id = Convert.ToInt64(txbId.Text);
            product.Name = txbName.Text;
            product.CategoryId = Convert.ToInt64(txbCategoryId.Text);
            product.CategoryName = txbCategoryName.Text;
            ProductUpdateWindow productWindow = new ProductUpdateWindow(this,product);  
            productWindow.ShowDialog();
            productWindow.Closed += (s, e) => Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
