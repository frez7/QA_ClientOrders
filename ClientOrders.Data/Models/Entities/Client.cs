using ClientOrders.Data.Models.Common;

namespace ClientOrders.Data.Models.Entities
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int PhoneNum { get; set; }
        public int OrderAmount { get; set; }
        public DateTime DateAdd { get; set; }
        public Client()
        {
            DateAdd= DateTime.UtcNow;
        }
    }
}
