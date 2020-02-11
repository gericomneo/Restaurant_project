using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("OrdersDetails", Schema = "Server")]
    public partial class OrdersDetails
    {
        [Key, Column(Order = 0)]
        public int OrdersId { get; set; }
        [Key, Column(Order = 1)]
        public int MenuItemsId { get; set; }
        public short Amount { get; set; }

        public virtual Orders Orders { get; set; }
        public virtual MenuItems MenuItems { get; set; }
    }
}
