using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pl.edu.wat.wcy.pz.restaurant_server_application.Models
{
    [Table("ReservationsDetails", Schema = "Server")]
    public partial class ReservationsDetails
    {
        [Key, Column(Order = 0)]
        public int ReservationsId { get; set; }
        [Key, Column(Order = 1)]
        public int TablesId { get; set; }

        public virtual Reservations Reservations { get; set; }
        public virtual Tables Tables { get; set; }
    }
}
