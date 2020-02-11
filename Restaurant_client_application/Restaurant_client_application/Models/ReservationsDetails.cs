namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class ReservationsDetails
    {
        public int ReservationsId { get; set; }
        public int TablesId { get; set; }

        public ReservationsDetails(int reservationsId, int tableId)
        {
            ReservationsId = reservationsId;
            TablesId = tableId;
        }
    }
}
