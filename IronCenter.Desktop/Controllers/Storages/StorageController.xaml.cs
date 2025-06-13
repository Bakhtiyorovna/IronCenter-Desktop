using IronCenter.Desktop.DbContexts;
using IronCenter.Desktop.Pages.Dashboard.Products;
using IronCenter.Desktop.Pages.Storages;
using IronCenter.Desktop.Windows.Products;
using IronCenter.Desktop.Windows.Storages;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Domain.Storages;
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

namespace IronCenter.Desktop.Controllers
{
    public partial class StorageController : UserControl
    {
        private Storages storages;
        public Storage _storage;

        public  StorageController(Storages storage, Storage storage1)
        {
            InitializeComponent();
            SizeAsync();
            this.storages = storage;
            this._storage = storage1;
        }

        public async Task SizeAsync()
        {
            var mainWindow =  Application.Current.MainWindow;

          

            var window = Window.GetWindow(mainWindow);
            if (window != null)
            {
                window.StateChanged += (s, e) =>
                {
                    var currentState = window.WindowState;
                     if(currentState == WindowState.Maximized)
                    {
                        this.Width = 950;
                    }
                    else
                    {
                        this.Width = 865;
                    }
                };
            }
        }

        public void SetData(IronCenter.Service.Domain.Storages.Storage storage)
        {
            var product = new Product();

            using (var dbContext = new AppDbContext())
            {
                product = dbContext.Products.Find(storage.ProductId);
            }

            txbProductName.Text = product.Name;
            txbCategoryName.Text = product.CategoryName;
            if (storage.Unitary.ToString() == "dona")
            {
                txbUnitaryCurrencyQuintity.Text = "1 " + storage.Unitary.ToString() + "si " + storage.UnitPrice.ToString() +" "+ storage.Сurrency.ToString() + " dan " + storage.Quantity + " " + storage.Unitary;
            }
            else
                txbUnitaryCurrencyQuintity.Text = "1 " + storage.Unitary.ToString() + "i " + storage.UnitPrice.ToString() +" "+ storage.Сurrency.ToString() + " dan " + storage.Quantity + " " + storage.Unitary;
            txbDate.Text = storage.ArrivalDate.ToString("dd.MM.yyyy")+" sanada kelgan";
            txbPresentQuintity.Text = storage.PeresentValue.ToString()+" "+storage.Unitary+" mavjud";
            txbSaleQuintity.Text = (storage.Quantity - storage.PeresentValue).ToString()+ " " + storage.Unitary + " sotilgan";
            txbId.Text = storage.Id.ToString();
            Image.ImageSource = new BitmapImage(new Uri(storage.Product.ImagePath));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void SaleButton_Click(object sender, RoutedEventArgs e)
        {
            StorageSaleWindow storageSaleWindow = new StorageSaleWindow(this,_storage);
            storageSaleWindow.Show();
            storageSaleWindow.Closed += (s, e) => SetData(_storage);
        }
    }
}
