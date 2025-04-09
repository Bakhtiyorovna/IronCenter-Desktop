using IronCenter.Service.Domain.Common;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Domain.Sales;
using IronCenter.Service.Enums;

namespace IronCenter.Service.Domain.Storages
{
    public class Storage:Auditable
    {
        public long ProductId { get; set; }       
        public Product Product { get; set; }

        public DateTime ArrivalDate { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int PeresentValue { get; set; }
        public Currency Сurrency { get; set; }
        public Unitary Unitary { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
