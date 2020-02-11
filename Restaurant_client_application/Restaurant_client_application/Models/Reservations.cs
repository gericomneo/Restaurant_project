using System;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class Reservations
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateOn { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ReservationsStatesId{ get; set; }

        public Reservations(int id, DateTime date, DateTime dateOn, string firstName, string name, string phone, int reservationsStatesId)
        {
            Id = id;
            Date = date;
            DateOn = dateOn;
            FirstName = firstName;
            Name = name;
            Phone = phone;
            ReservationsStatesId = reservationsStatesId;
        }
    }
}
