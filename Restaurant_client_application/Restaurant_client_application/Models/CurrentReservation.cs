namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class CurrentReservation
    {
        public int TablesId { get; set; }

        public CurrentReservation(int tableId)
        {
            TablesId = tableId;
        }
    }
}
