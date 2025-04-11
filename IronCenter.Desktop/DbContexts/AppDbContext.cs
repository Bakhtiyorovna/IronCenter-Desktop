using IronCenter.Service.Domain.Categories;
using IronCenter.Service.Domain.Employers;
using IronCenter.Service.Domain.Employes;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Domain.Sales;
using IronCenter.Service.Domain.Storages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Desktop.DbContexts
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;;Database=IronCenter-Db;Username=postgres;Password=1111");


        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
