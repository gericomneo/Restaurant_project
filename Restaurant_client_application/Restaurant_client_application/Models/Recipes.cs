namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class Recipes
    {
        public int MenuItemsId { get; set; }
        public string Description { get; set; }

        public Recipes(int id, string description)
        {
            MenuItemsId = id;
            Description = description;
        }
    }
}
