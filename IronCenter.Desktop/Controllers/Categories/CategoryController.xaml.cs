using IronCenter.Desktop.DbContexts;
using IronCenter.Desktop.Pages.Dashboard.Products;
using IronCenter.Desktop.Windows.Categories;
using IronCenter.Desktop.Windows.Products;
using IronCenter.Service.Domain.Categories;
using IronCenter.Service.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IronCenter.Desktop.Controllers.Categories
{
    /// <summary>
    /// Interaction logic for ProductController.xaml
    /// </summary>
    public partial class CategoryController : UserControl
    {
        public Category _category;
        public CategoryController(Category category)
        {
            InitializeComponent();
            this._category= category;
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

        public void SetData(Category category)
        {
            txbDescription.Text = category.Description;
            txbCategoryName.Text = category.Name;
            txbId.Text = category.Id.ToString();
            Image.ImageSource = new BitmapImage(new Uri(category.ImagePath));
        }

        public async Task DeleteAsync(){
           
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category();
            category.Id = Convert.ToInt64(txbId.Text);
            category.Name = txbCategoryName.Text;
            category.Description = txbDescription.Text;
            CategoryDeleteWindow productWindow = new CategoryDeleteWindow(this, category);
            productWindow.ShowDialog();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category();
            category.Id = Convert.ToInt64(txbId.Text);
            category.Name = txbCategoryName.Text;
            category.Description = txbDescription.Text;
            CategoryUpdateWindow productWindow = new CategoryUpdateWindow(category,this );
            productWindow.ShowDialog();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
