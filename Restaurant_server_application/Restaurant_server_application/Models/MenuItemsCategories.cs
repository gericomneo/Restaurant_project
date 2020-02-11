using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("MenuItemsCategories", Schema = "Server")]
    public class MenuItemsCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MenuItems> MenuItems { get; set; }
    }
}
