using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("Ingredients", Schema = "Server")]
    public class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }
        public virtual ICollection<MenuItemsIngredients> MenuItemsIngredients { get; set; }
    }
}
