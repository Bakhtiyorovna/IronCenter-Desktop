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
using IronCenter.Service.Domain.Sales;
using IronCenter.Service.Domain.Storages;
using Microsoft.EntityFrameworkCore.Diagnostics;
using IronCenter.Service.Enums;
using IronCenter.Desktop.Controllers;

namespace IronCenter.Desktop.Windows.Storages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StorageSaleWindow : Window
    {
        public Storage _storage;
        public StorageController _storageController;
        public StorageSaleWindow(StorageController storageController, Storage storage)
        {
            InitializeComponent();
            this._storage = storage;
            this._storageController = storageController;
        }


        private void btnCreateWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtProductPrice.Text) || txtProductQuintitty.Text == null)
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Sale sale = new Sale();
                sale.StorageId = _storage.Id;
                sale.Сurrency = (Currency)cmbCorency.SelectedItem;
                sale.Unitary = _storage.Unitary;
                sale.Quantity = Convert.ToInt32(txtProductQuintitty.Text);
                if (sale.Quantity > _storage.Quantity)
                {
                    MessageBox.Show("Mahsulot yetarli emas!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Show();
                }
                sale.UnitPrice = Convert.ToInt32(txtProductPrice.Text);
                sale.Income = sale.Quantity* sale.UnitPrice;
                sale.SaleDate = sale.CreatedAt  = DateTime.Now.ToUniversalTime();
                sale.UpdatedAt = DateTime.Now.ToUniversalTime();

                using (var dbContext = new AppDbContext())
                {
                    dbContext.Sales.Add(sale);

                    _storage.PeresentValue = _storage.Quantity - sale.Quantity;
                    dbContext.Storages.Update(_storage);
                    int result = dbContext.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Sotuv muvaffaqiyatli saqlandi!");

                        _storageController.SetData(_storage,"");
                        this.Close();
                    }
                }
            }
            catch(Exception ex) { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var enumValues1 = Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList();
            cmbCorency.ItemsSource = enumValues1;
        }
    }
}