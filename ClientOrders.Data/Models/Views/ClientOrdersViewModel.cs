using ClientOrders.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientOrders.Data.Models.Views
{
    public class ClientOrdersViewModel
    {
        public Client Client { get; set; }
        public List<Order> Orders { get; set; }
    }
}
