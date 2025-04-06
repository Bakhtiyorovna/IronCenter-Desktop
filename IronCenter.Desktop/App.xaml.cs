namespace IronCenter.Desktop
{
    public partial class App : Application
    {
       // public static ServiceProvider ServiceProvider { get; private set; }

            //protected override void OnStartup(StartupEventArgs e)
            //{
            //    base.OnStartup(e);

            //    // Dependency Injection konteynerini yaratish
            //    var serviceCollection = new ServiceCollection();

            //    // ApplicationDbContext va ProductService'ni DI konteyneriga qo'shish
            //    serviceCollection.AddDbContext<DbContext>(options =>
            //        options.UseNpgsql("Host=localhost;Port=5432;Database=IronCenterDb;Username=postgres;Password=1111"));

            //    // ICategoryService va Repository xizmatlarini DI'ga qo'shish
            //    serviceCollection.AddScoped<ICategoryService, CategoryService>();
            //    serviceCollection.AddScoped<IRepository<Category>, Repository<Category>>();

            //    // MainWindow ni DI konteyneriga qo'shish
            //    serviceCollection.AddTransient<MainWindow>();

            //    // DI konteyneridan ServiceProvider yaratish
            //    ServiceProvider = serviceCollection.BuildServiceProvider();

            //    // MainWindow ni DI orqali yaratish va ko'rsatish
            //    var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            //    mainWindow.Show();
            //}
    }
}
