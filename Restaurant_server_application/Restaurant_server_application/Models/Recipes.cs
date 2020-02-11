using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("Recipes", Schema = "Server")]
    public partial class Recipes
    {
        [Key, ForeignKey("MenuItems"), Column(Order = 0)]
        public int MenuItemsId { get; set; }
        public string Description { get; set; }
        public virtual MenuItems MenuItems { get; set; }
    }
}
