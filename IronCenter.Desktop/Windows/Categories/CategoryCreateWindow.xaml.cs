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

namespace IronCenter.Desktop.Windows.Categories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CategoryCreateWindow : Window
    {
        public string destinationFolder = "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\CategoryImages\\";
        public string imagePath="";
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

            if (string.IsNullOrWhiteSpace(txtcategoryName.Text) || txbDescription.Text == null || imagePath=="" )
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
                    ImagePath = imagePath,
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

        private void BtnPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
            if(openFileDialog.ShowDialog() == true)
            {
                string imgPath = openFileDialog.FileName;

                string extension = Path.GetExtension(imgPath);

                string newFileName = Guid.NewGuid().ToString()+"_categoryPicture" + extension;

                string destinationPath = Path.Combine(destinationFolder, newFileName);

                File.Copy(imgPath, destinationPath, overwrite: true);

                imagePath = destinationPath;
                BtnPitureName.Content = "Rasm tanlandi";
            }
        }
    }
}