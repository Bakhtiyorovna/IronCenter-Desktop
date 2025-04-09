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

namespace IronCenter.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProductCreateWindow : Window
    {
        public ProductCreateWindow()
        {
            InitializeComponent();
            LoadCategories();
        }

        private async Task LoadCategories()
        {
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var categories = await dbContext.Categories.ToListAsync();
                    if (categories.Count > 0)
                    {
                        cmbCategory.ItemsSource = categories;
                        cmbCategory.DisplayMemberPath = "Name";  // ComboBoxda ko'rsatiladigan maydon
                        cmbCategory.SelectedValuePath = "Id";   // ComboBoxda tanlangan qiymatning id'si
                    }
                    else
                    {
                        MessageBox.Show("Not found categories");
                    }
                }
            }
            catch (Exception ex) { }

        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtProductName.Text) || cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var newProduct = new Product
                {
                    Name = txtProductName.Text,
                    Value = 0,
                    CategoryId =(long)cmbCategory.SelectedValue
                };
                
                using (var dbContext = new AppDbContext())
                {
                    long Id = (long)cmbCategory.SelectedValue;
                    var category =  dbContext.Categories.Find(Id);

                    newProduct.CategoryName = category.Name;

                    dbContext.Add(newProduct);
                    var result = dbContext.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Mahsulot muvaffaqiyatli saqlandi!");
                        this.Close();
                    }
                }
            }
            catch(Exception ex) { }
        }
    }
}