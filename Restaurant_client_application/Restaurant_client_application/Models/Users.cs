namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
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

        public Users(int id, string login, string password, string email, string firstName, string name, string city, string addressCode, 
            string street, string houseNumber, string phone, int usersTypes, double salary)
        {
            Id = id;
            Login = login;
            Password = password;
            Email = email;
            FirstName = firstName;
            Name = name;
            City = city;
            AddressCode = addressCode;
            Street = street;
            HouseNumber = houseNumber;
            Phone = phone;
            UsersTypesId = usersTypes;
            Salary = salary;
        }
    }
}
