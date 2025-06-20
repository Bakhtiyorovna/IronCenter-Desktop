﻿using IronCenter.Service.DataAccess.Interfaces;
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
using System.Windows.Controls;
using System.Xml.Linq;
using IronCenter.Desktop.Controllers;
using Azure;
using Microsoft.Win32;
using System.IO;

namespace IronCenter.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProductUpdateWindow : Window
    {
        public string destinationFolder = "D:\\Proekts\\DotNet\\IronCenter-Desktop\\IronCenter.Desktop\\Assets\\Images\\ProductImages\\";
        public string imagePath = "";
        public long Id;
        public Product product;
        public ProductController productController;
        public ProductUpdateWindow(ProductController controller,Product product)
        {
            InitializeComponent();
            LoadAsync();
            this.Id = product.Id;
            this.product = product;
            this.productController = controller;
        }

        private async Task LoadAsync()
        {
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var categories = await dbContext.Categories.ToListAsync();
                    if (categories.Count > 0)
                    {
                        txtProductName.Text= product.Name;
                        cmbCategory.ItemsSource = categories;
                        cmbCategory.SelectedItem = product.CategoryName;
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
        public async Task Updateasync()
        { 
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var product = await dbContext.Products
                        .Where(ea => ea.Id == Id)
                        .AsNoTracking() 
                        .FirstOrDefaultAsync();

                    if (product != null)
                    {
                        if (imagePath != "")
                        {
                            product.ImagePath = imagePath;
                        }
                        product.Name = txtProductName.Text;
                        if (cmbCategory.SelectedValue is not null){
                             product.CategoryId = (long)cmbCategory.SelectedValue;
                             long CategoryId = (long)cmbCategory.SelectedValue;
                             var category = dbContext.Categories.Find(CategoryId);
                             product.CategoryName = category.Name;
                             product.UpdatedAt = DateTime.Now.ToUniversalTime();
                        }

                        dbContext.Products.Update(product);  
                        var result = await dbContext.SaveChangesAsync() > 0;  

                        if (result)
                        {
                            MessageBox.Show("Mahsulot muvaffaqiyatli yangilandi!");

                            productController.SetData(product);
                            this.Close();
                        }
                        else
                            MessageBox.Show("Xatolik: Mahsulot yangilanmadi.");
                    }
                    else
                    {
                        MessageBox.Show("Mahsulot topilmadi!");
                    }
                }
            }
            catch (Exception e) { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Updateasync();
        }

        private void BtnPictureName_Click(object sender, RoutedEventArgs e)
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