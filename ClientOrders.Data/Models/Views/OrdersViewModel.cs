using ClientOrders.Data.Models.Common;
using ClientOrders.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientOrders.Data.Models.Views
{
    public class OrdersViewModel : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public int ClientID { get; set; }
        public string Description { get; set; }
        public float OrderPrice { get; set; }
        public DateTime CloseDate { get; set; }
        public List<Client> ClientList { get; set; }
    }
}
