using ClientOrders.Data.Models.Common;

namespace ClientOrders.Data.Models.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public int ClientID { get; set; }
        public string Description { get; set; }
        public float OrderPrice { get; set; }
        public DateTime CloseDate { get; set; }
        public Order()
        {
            OrderDate = DateTime.UtcNow;
        }
    }
}
