using IronCenter.Service.Domain.Categories;
using IronCenter.Service.Domain.Employers;
using IronCenter.Service.Domain.Employes;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Domain.Sales;
using IronCenter.Service.Domain.Storages;
using Microsoft.EntityFrameworkCore;

namespace IronCenter.Service.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionstring = "Host=localhost;Port=5432;;Database=IronCenterDb;Username=postgres;Password=1111";
            optionsBuilder.UseNpgsql(connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Storages)
                .HasForeignKey(i => i.ProductId);

            modelBuilder.Entity<Sale>()
               .HasOne(i => i.Product)
               .WithMany(p => p.Sales)
               .HasForeignKey(i => i.ProductId);

            modelBuilder.Entity<Salary>()
                .HasOne(i => i.Employee)
                .WithMany(p => p.Salaries)
                .HasForeignKey(i => i.EmployeeId);
        }
    }
}
