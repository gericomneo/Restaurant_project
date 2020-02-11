using System;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TablesId { get; set; }
        public int OrderStateId { get; set; }

        public Orders(int id, DateTime date, int tableId, int orderStateId)
        {
            Id = id;
            Date = date;
            TablesId = tableId;
            OrderStateId = orderStateId;
        }
    }
}