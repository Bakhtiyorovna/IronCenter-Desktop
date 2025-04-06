using IronCenter.Service.Domain.Common;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Domain.Sales
{
    public class Sale:Auditable
    {

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } 
        public decimal TotalPrice { get; set; }
        public Currency Сurrency { get; set; }
    }
}
