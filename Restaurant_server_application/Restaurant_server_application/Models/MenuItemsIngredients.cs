using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("MenuItemIngredients", Schema = "Server")]
    public partial class MenuItemsIngredients
    {
        [Key, Column(Order = 0)]
        public int MenuItemsId { get; set; }
        [Key, Column(Order = 1)]
        public int IngredientsId { get; set; }
        public virtual MenuItems MenuItems { get; set; }
        public virtual Ingredients Ingredients { get; set; }
    }
}
