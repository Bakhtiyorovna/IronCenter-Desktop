using IronCenter.Service.DataAccess.Interfaces;
using IronCenter.Service.DataAccess.Repositories;
using IronCenter.Service.Domain.Categories;
using IronCenter.Service.Domain.Storages;
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
using IronCenter.Service.Enums;
using Microsoft.Identity.Client.Extensions.Msal;
using IronCenter.Service.Domain.Products;

namespace IronCenter.Desktop.Windows.Storages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StorageCreateWindow : Window
    {
        public long Id;
        public Product product;
        public ProductController productController;
        public StorageCreateWindow()
        {
            InitializeComponent();
            LoadInfo();
           
        }

        private async Task LoadInfo()
        {
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var products = await dbContext.Products.ToListAsync();
                    var enumValues1 = Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList();
                    var enumValues2 = Enum.GetValues(typeof (Unitary)).Cast<Unitary>().ToList();
                    if (products.Count >= 0)
                    {
                        cmbProduct.ItemsSource = products;
                        cmbCorency.ItemsSource = enumValues1;
                        cmbQuintityUnitary.ItemsSource = enumValues2;
                        cmbProduct.DisplayMemberPath = "Name";  // ComboBoxda ko'rsatiladigan maydon
                        cmbProduct.SelectedValuePath = "Id";   // ComboBoxda tanlangan qiymatning id'si
                    }
                    else
                    {
                        MessageBox.Show("Not found Info");
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
            CreatAsync();
        }

        public async Task CreatAsync()
        {
            if(cmbProduct.SelectedItem == null || cmbCorency.SelectedItem == null || cmbQuintityUnitary.SelectedItem==null || txbQuintity.Text==null || txbProductPrice.Text==null)
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Service.Domain.Storages.Storage storage = new Service.Domain.Storages.Storage();
                storage.Quantity = Convert.ToInt32(txbQuintity.Text);
                storage.ProductId =(long)cmbProduct.SelectedValue;
                storage.UnitPrice = Convert.ToInt32(txbProductPrice.Text);
                storage.Сurrency =  (Currency)cmbCorency.SelectedItem;
                storage.Unitary = (Unitary)cmbQuintityUnitary.SelectedItem;
                storage.ArrivalDate =((DateTime)dtmArrivaldate.SelectedDate).ToUniversalTime();
                storage.TotalPrice = storage.UnitPrice * storage.Quantity;
                storage.PeresentValue = storage.Quantity;
                storage.CreatedAt = DateTime.Now.ToUniversalTime();
                storage.UpdatedAt = DateTime.Now.ToUniversalTime();

                using (var dbContext = new AppDbContext())
                {
                    long Id = (long)cmbProduct.SelectedValue;
                    var product = dbContext.Products.Find(Id);

                    storage.Product = product;

                    dbContext.Add(storage);
                    var result = dbContext.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Mahsulot muvaffaqiyatli saqlandi!");
                        this.Close();
                    }
                }


            }
            catch(Exception e)
            {
                MessageBox.Show("Xatolik yuzaga keldi.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}