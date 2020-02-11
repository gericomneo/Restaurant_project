using System.ComponentModel.DataAnnotations.Schema;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("Users", Schema = "Server")]
    public class Users
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string AddressCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Phone { get; set; }
        public int UsersTypesId { get; set; }
        public double Salary { get; set; }

        public virtual UsersTypes UsersTypes { get; set; }

    }
}
