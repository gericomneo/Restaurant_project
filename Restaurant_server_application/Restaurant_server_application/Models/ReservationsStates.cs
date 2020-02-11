using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("ReservationsStates", Schema = "Server")]
    public class ReservationsStates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
