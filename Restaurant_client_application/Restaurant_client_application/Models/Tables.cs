namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class Tables
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public short Size { get; set; }
        public double ReservationCost { get; set; }

        public Tables(int id, int number, short size, double reservationCost)
        {
            Id = id;
            Number = number;
            Size = size;
            ReservationCost = reservationCost;
        }
    }
}
