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
using IronCenter.Desktop.Controllers.Categories;
using SkiaSharp;

namespace IronCenter.Desktop.Windows.Categories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CategoryUpdateWindow : Window
    {
        public CategoryController CategoryController { get; set; }
        public Category Category { get; set; } 
        public CategoryUpdateWindow(Category category, CategoryController controller)
        {
            InitializeComponent();
            this.CategoryController = controller;
            this.Category = category;
            LoadAsync();
        }
       

        public async Task LoadAsync()
        {
            txbDescription.Text = Category.Description;
            txtcategoryName.Text = Category.Name;
        }
        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {      
                if (string.IsNullOrWhiteSpace(txtcategoryName.Text) || txbDescription.Text == null)
                {
                    MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            long Id = Category.Id;
            using (var dbContext = new AppDbContext())
            {
                var category = await dbContext.Categories
                           .Where(ea => ea.Id == Id)
                           .AsNoTracking()
                           .FirstOrDefaultAsync();

                category = new Category
                {
                    Name = txtcategoryName.Text,
                    Description = txbDescription.Text,
                    UpdatedAt = DateTime.Now.ToUniversalTime()
                };

                dbContext.Update(category);
                var result = dbContext.SaveChanges();
                if (result > 0)
                {
                    MessageBox.Show("Kategoriya muvaffaqiyatli yangilandi!");
                    CategoryController.SetData(category);
                    this.Close();
                }

            }
        }
    }
}