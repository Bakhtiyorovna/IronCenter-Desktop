using IronCenter.Service.Data;
using IronCenter.Service.Domain.Categories;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Services.Services;
using System.Threading.Tasks;
using System.Windows;

namespace IronCenter.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProductCreateWindow : Window
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        public ProductCreateWindow()
        {
            InitializeComponent();
            LoadCategories();
            _categoryService = new CategoryService();
        }

        private async Task LoadCategories()
        {
            var initialCategories = new List<Category>
            {
               new Category { Name = "Elektronika", Description = "Elektronika mahsulotlari", Created = DateTime.Now, Updated = DateTime.Now },
               new Category { Name = "Kitoblar", Description = "Kitoblar va adabiyotlar", Created = DateTime.Now, Updated = DateTime.Now },
               new Category { Name = "Kiyimlar", Description = "Kiyim-kechaklar", Created = DateTime.Now, Updated = DateTime.Now }
            };

            foreach (var category in initialCategories)
            {
                await _categoryService.AddAsync(category);
            }

            var categories = await _categoryService.GetAllAsync();
            cmbCategory.ItemsSource = categories;

        }

        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            ProductCreateWindow window = new ProductCreateWindow();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtProductName.Text) || cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Yangi Product yaratamiz
            var newProduct = new Product
            {
                Name = txtProductName.Text,
                Value = 0,
                CategoryId = (int)cmbCategory.SelectedValue
            };

            _productService.AddAsync(newProduct); 

            MessageBox.Show("Mahsulot muvaffaqiyatli qo‘shildi!", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

            txtProductName.Clear();
            cmbCategory.SelectedIndex = -1;
        }
    }
}