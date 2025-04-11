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

namespace IronCenter.Desktop.Windows.Categories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CategoryCreateWindow : Window
    {
        public CategoryCreateWindow()
        {
            InitializeComponent();
        }

       
        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtcategoryName.Text) || txbDescription.Text == null)
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var newCategory = new Category
                {
                    Name = txtcategoryName.Text,
                    Description = txbDescription.Text,
                    CreatedAt  = DateTime.Now.ToUniversalTime(),
                    UpdatedAt = DateTime.Now.ToUniversalTime()
                };
                
                using (var dbContext = new AppDbContext())
                {
                    dbContext.Add(newCategory);
                    var result = dbContext.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Kategoriya muvaffaqiyatli saqlandi!");
                        this.Close();
                    }
                }
            }
            catch(Exception ex) { }
        }
    }
}