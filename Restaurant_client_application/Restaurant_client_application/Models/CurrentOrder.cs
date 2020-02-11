namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class CurrentOrder
    {
        public int MenuItemsId { get; set; }
        public short Amount { get; set; }

        public CurrentOrder(int menuItemId, short amount)
        {
            MenuItemsId = menuItemId;
            Amount = amount;
        }
    }
}
