using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("OrdersStates", Schema = "Server")]
    public class OrdersStates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
