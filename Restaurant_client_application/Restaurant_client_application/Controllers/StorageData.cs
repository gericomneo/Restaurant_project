using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System.Collections.Generic;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Controllers
{
    public static class StorageData
    {
        public static int selectedItemType;
        public static List<Ingredients> IngredientsList = new List<Ingredients>();
        public static List<MenuItemsCategories> MenuItemsCategoriesList = new List<MenuItemsCategories> ();
        public static List<MenuItemsIngredients> MenuItemsIngredientsList = new List<MenuItemsIngredients>();
        public static List<MenuItems> MenuItemsList = new List<MenuItems>();
        public static List<OrdersDetails> OrdersDetailsList = new List<OrdersDetails>();
        public static List<OrdersStates> OrdersStatesList = new List<OrdersStates>();
        public static List<Orders> OrdersList = new List<Orders>();
        public static List<Recipes> RecipesList = new List<Recipes>();
        public static List<Reservations> ReservationsList = new List<Reservations>();
        public static List<ReservationsDetails> ReservationsDetailsList = new List<ReservationsDetails>();
        public static List<ReservationsStates> ReservationsStatesList = new List<ReservationsStates>();
        public static List<Tables> TablesList = new List<Tables>();
        public static List<Users> UsersList = new List<Users>();
        public static List<UsersTypesId> UsersTypesIdList = new List<UsersTypesId>();
    }
}
