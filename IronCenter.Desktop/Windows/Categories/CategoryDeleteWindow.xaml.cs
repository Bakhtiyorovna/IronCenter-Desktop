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
using IronCenter.Desktop.Controllers.Categories;

namespace IronCenter.Desktop.Windows.Categories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CategoryDeleteWindow : Window
    {
        public readonly Category _category;
        public CategoryController Controller;
        public CategoryDeleteWindow(CategoryController controller,Category category)
        {
            InitializeComponent();
            this.Controller = controller;
            this._category = category;
            Load();
        }

        private async Task Load()
        {

            txb.Content = _category.Name + " kategoriyasi o'chirilsinmi?";
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
                    var category = await dbContext.Categories
                           .Where(ea => ea.Id == _category.Id)
                           .AsNoTracking()
                           .FirstOrDefaultAsync();

                    if (category is null)
                        MessageBox.Show("Mahsulot mavjud emas");

                    var entity = await dbContext.Categories.FirstOrDefaultAsync(e => e.Id.Equals(_category.Id));
                    dbContext.Categories.Remove(entity);
                    var result = await dbContext.SaveChangesAsync() > 0;


                    if (result)
                    {
                        MessageBox.Show("Mahsulot muvaffaqiyatli o'chirildi!");

                        var parent = Controller.Parent as Panel;
                        if (parent != null)
                        {
                            parent.Children.Remove(Controller);
                        }

                        var frame = FindParent<Frame>(Controller);
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