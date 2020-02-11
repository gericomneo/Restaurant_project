namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }

        public Ingredients(int id, string name, bool available)
        {
            Id = id;
            Name = name;
            Available = available;
        }
    }
}
