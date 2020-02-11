using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("MenuItems", Schema = "Server")]
    public class MenuItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int MenuItemsCategoriesId { get; set; }
        public bool Available { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual MenuItemsCategories MenuItemsCategories { get; set; }
        public virtual ICollection<MenuItemsIngredients> MenuItemsIngredients { get; set; }
        public virtual ICollection<OrdersDetails> OrdersDetails { get; set; }
    }
}