namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class UsersTypesId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UsersTypesId(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
