using IronCenter.Service.DataAccess.Interfaces;
using IronCenter.Service.DataAccess.Repositories;
using IronCenter.Service.Domain.Categories;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Services.Interfaces;
using IronCenter.Service.Services.Services;
using IronCenter.Desktop.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using IronCenter.Desktop.Controllers;
using System.Windows.Controls;
using System.Windows.Media;

namespace IronCenter.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProductDeleteWindow : Window
    {
        public readonly Product _product;
        public ProductController productController;
        public ProductDeleteWindow(ProductController controller,Product product)
        {
            InitializeComponent();
            this.productController = controller;
            this._product = product;
            Load();
        }

        private async Task Load()
        {

            txb.Content = _product.Name + " mahsuloti o'chirilsinmi?";
        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             DeleteAsync();
        }

        public async Task DeleteAsync()
        {
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var product = await dbContext.Products
                           .Where(ea => ea.Id == _product.Id)
                           .AsNoTracking()
                           .FirstOrDefaultAsync();

                    if (product is null)
                        MessageBox.Show("Mahsulot mavjud emas");

                    var entity = await dbContext.Products.FirstOrDefaultAsync(e => e.Id.Equals(_product.Id));
                    dbContext.Products.Remove(entity);
                    var result = await dbContext.SaveChangesAsync() > 0;


                    if (result)
                    {
                        MessageBox.Show("Mahsulot muvaffaqiyatli o'chirildi!");

                        var parent = productController.Parent as Panel;
                        if (parent != null)
                        {
                            parent.Children.Remove(productController);
                        }

                        var frame = FindParent<Frame>(productController);
                        if (frame != null)
                        {
                            var currentPage = frame.Content as Page;
                            if (currentPage != null)
                            {
                                // Page'ni qayta yuklaymiz
                                frame.Navigate(new Uri(currentPage.GetType().FullName, UriKind.Relative));
                            }
                        }

                        this.Close();
                    }
                    else
                        MessageBox.Show("Xatolik: Mahsulot o'chirilmadi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik: {ex.Message}");
            }
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
    }
}