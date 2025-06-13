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
using Microsoft.Win32;
using System.IO;

namespace IronCenter.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProductCreateWindow : Window
    {
        public string destinationFolder = "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\ProductImages\\";
        public string imagePath = "";
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

            if (string.IsNullOrWhiteSpace(txtProductName.Text) || cmbCategory.SelectedItem == null|| imagePath=="")
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
                    ImagePath = imagePath,
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

        private void BtnPicture_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string imgPath = openFileDialog.FileName;

                string extension = Path.GetExtension(imgPath);

                string newFileName = Guid.NewGuid().ToString() + "_productPicture" + extension;

                string destinationPath = Path.Combine(destinationFolder, newFileName);

                File.Copy(imgPath, destinationPath, overwrite: true);

                imagePath = destinationPath;
                BtnPictureName.Content = "Rasm tanlandi";
            }
        }
    }
}