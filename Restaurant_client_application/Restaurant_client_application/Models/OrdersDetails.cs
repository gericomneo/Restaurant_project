namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class OrdersDetails
    {
        public int OrdersId { get; set; }
        public int MenuItemsId { get; set; }
        public short Amount { get; set; }

        public OrdersDetails(int id, int menuItemId, short amount)
        {
            OrdersId = id;
            MenuItemsId = menuItemId;
            Amount = amount;
        }
    }
}
