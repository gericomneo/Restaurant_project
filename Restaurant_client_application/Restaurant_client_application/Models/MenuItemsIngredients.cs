namespace pl.edu.wat.wcy.pz.restaurant_client_application.Models
{
    public class MenuItemsIngredients
    {
        public int MenuItemsId { get; set; }
        public int IngredientsId { get; set; }

        public MenuItemsIngredients(int menuId, int ingredientId)
        {
            MenuItemsId = menuId;
            IngredientsId = ingredientId;
        }
    }
}
