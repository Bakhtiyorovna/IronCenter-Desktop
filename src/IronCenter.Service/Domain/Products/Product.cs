using IronCenter.Service.Domain.Common;
using IronCenter.Service.Domain.Sales;
using IronCenter.Service.Domain.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Domain.Products
{
    public class Product :Auditable
    {
        public string Name { get; set; }
        public long CategoryId  { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int Value { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public List<Storage> Storages { get; set; }
    }
}
