using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("Tables", Schema = "Server")]
    public class Tables
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public short Size { get; set; }
        public double ReservationCost { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<ReservationsDetails> ReservationsDetails { get; set; }
    }
}
