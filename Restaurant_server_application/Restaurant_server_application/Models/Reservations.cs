using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("Reservations", Schema = "Server")]
    public class Reservations
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateOn { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ReservationsStatesId { get; set; }

        public virtual ReservationsStates ReservationsStates { get; set; }
    }
}
