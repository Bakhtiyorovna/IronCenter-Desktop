using IronCenter.Service.Domain.Sales;
using IronCenter.Service.Domain.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Domain.Products
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int CategoryId  { get; set; }
        public int Value { get; set; }

        public List<Storage> Storages { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
