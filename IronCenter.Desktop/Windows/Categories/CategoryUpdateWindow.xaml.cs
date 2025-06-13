using IronCenter.Service.Domain.Categories;
using IronCenter.Desktop.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using IronCenter.Desktop.Controllers.Categories;
using System.Drawing;
using Microsoft.Win32;
using System.IO;

namespace IronCenter.Desktop.Windows.Categories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CategoryUpdateWindow : Window
    {
        public CategoryController CategoryController { get; set; }
        public Category Category { get; set; }
        public string imagePath = "";
        public string destinationFolder = "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\CategoryImages\\";
        public string deleteImagePath;
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
                if (category != null)
                {
                    if (imagePath == "")
                    {
                        imagePath = category.ImagePath;
                    }
                    else
                    {
                        deleteImagePath = category.ImagePath;
                    }

                    category.ImagePath = imagePath;
                    category.Name = txtcategoryName.Text;
                    category.Description = txbDescription.Text;
                    category.UpdatedAt = DateTime.Now.ToUniversalTime();

                    dbContext.Update(category);
                    var result = dbContext.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Kategoriya muvaffaqiyatli yangilandi!");
                        CategoryController.SetData(category);
                        this.Close();
                  //      File.Delete(deleteImagePath);
                    }
                }
            }
        }

        private void BtnPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string imgPath = openFileDialog.FileName;

                string extension = Path.GetExtension(imgPath);

                string newFileName = Guid.NewGuid().ToString() + "_categoryPicture" + extension;

                string destinationPath = Path.Combine(destinationFolder, newFileName);

                File.Copy(imgPath, destinationPath, overwrite: true);


                imagePath = destinationPath;
                BtnPitureName.Content = "Rasm o'zgartirildi";
            }
        }
    }
}