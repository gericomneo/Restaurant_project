namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class MenuItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int MenuItemsCategoriesId { get; set; }
        public bool Available { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }


        public MenuItems(int id, string name, double price, int category, bool available, string description, string image)
        {
            Id = id;
            Name = name;
            Price = price;
            MenuItemsCategoriesId = category;
            Available = available;
            Description = description;
            Image = image;
        }
    }
}
