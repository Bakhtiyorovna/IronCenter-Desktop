using IronCenter.Desktop.DbContexts;
using IronCenter.Desktop.Windows.Products;
using IronCenter.Service.DataAccess.Interfaces;
using IronCenter.Service.DataAccess.Repositories;
using IronCenter.Service.Domain.Categories;
using IronCenter.Service.Services.Interfaces;
using IronCenter.Service.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace IronCenter.Desktop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            //ConfigureServices(serviceCollection);

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);

            //// Initialize DI container
            //var serviceProvider = ConfigureServices();

            //// Resolve MainWindow with dependencies
            //var mainWindow = serviceProvider.GetRequiredService<MainWindow>();

            //// Show MainWindow
            //mainWindow.Show();
            //base.OnStartup(e);

        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);
            services.AddSingleton<Controller>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IProductService, ProductService>();
           
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // Register services


            // Register MainWindow (optional if using implicit registration)
            services.AddTransient<MainWindow>();

            // Build service provider
            return services.BuildServiceProvider();
        }
        private void ConfigureServicesss(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddTransient<MainWindow>();

        }
    }
    
}
