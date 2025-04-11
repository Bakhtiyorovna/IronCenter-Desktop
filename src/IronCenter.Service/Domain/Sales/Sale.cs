using IronCenter.Service.Domain.Common;
using IronCenter.Service.Domain.Storages;
using IronCenter.Service.Enums;

namespace IronCenter.Service.Domain.Sales
{
    public class Sale:Auditable
    {
        public long StorageId { get; set; }
        public Storage Storage { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } 
        public decimal Income { get; set; }
        public decimal Belefit { get; set; }
        public Currency Сurrency { get; set; }
        public Unitary Unitary { get; set; }
    }
}
