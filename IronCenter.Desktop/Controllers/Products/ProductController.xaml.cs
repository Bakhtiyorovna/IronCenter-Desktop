using IronCenter.Desktop.DbContexts;
using IronCenter.Desktop.Pages.Dashboard.Products;
using IronCenter.Desktop.Windows.Products;
using IronCenter.Service.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            using (var dbContext = new AppDbContext())
            {
                var storage =  dbContext.Storages
                           .Where(ea => ea.ProductId == product.Id)
                           .AsNoTracking()
                           .FirstOrDefault();
                if (storage != null) { txbValue.Text = product.Value.ToString() +" "+ storage.Unitary.ToString()+" mavjud"; }
            }
            
            txbId.Text = product.Id.ToString();
            txbCategoryId.Text = product.CategoryId.ToString();
            Image.ImageSource = new BitmapImage(new Uri(product.ImagePath));
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
