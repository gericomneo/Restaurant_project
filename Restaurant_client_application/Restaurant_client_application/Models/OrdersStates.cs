namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class OrdersStates
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OrdersStates(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
