using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("Orders", Schema = "Server")]
    public class Orders
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TablesId { get; set; }
        public int OrdersStatesId { get; set; }
        public virtual Tables Tables { get; set; }
        public virtual OrdersStates OrdersStates { get; set; }
        public virtual ICollection<OrdersDetails> OrdersDetails { get; set; }
    }
}
