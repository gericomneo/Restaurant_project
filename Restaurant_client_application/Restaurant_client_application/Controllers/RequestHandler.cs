using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Controllers
{
    public class RequestHandler
    {
        public delegate void ResponseDelegate(int requestStatus);
        public event ResponseDelegate Response;
        public delegate void UpdateDelegate();
        public event UpdateDelegate Update;
        string[] msg;
        public string RequestRecognition(string message)
        {
            string command = message.Substring(0, 4);
            switch (command)
            {
                case "0001":
                    return Login(message.Substring(9));
                case "0003":
                    Response(1);
                    return "0";
                case "0004":
                    Response(4);
                    return "0";
                case "0005":
                    Response(5);
                    return "0";
                case "0006":
                    Response(6);
                    return "0";
                case "0011":
                    return AddUser(message.Substring(9));
                case "0012":
                    return UpdateUser(message.Substring(9));
                case "0013":
                    return RemoveUser(message.Substring(9));
                case "0015":
                    return AddUsersType(message.Substring(9));
                case "0021":
                    return AddIngredient(message.Substring(9));
                case "0022":
                    return UpdateIngredient(message.Substring(9));
                case "0023":
                    return UpdateIngredientAvailability(message.Substring(9));
                case "0024":
                    return RemoveIngredient(message.Substring(9));
                case "0031":
                    return AddMenuItem(message.Substring(9));
                case "0032":
                    return UpdateMenuItem(message.Substring(9));
                case "0033":
                    return RemoveMenuItem(message.Substring(9));
                case "0035":
                    return AddMenuItemIngredient(message.Substring(9));
                case "0036":
                    return RemoveMenuItemIngredient(message.Substring(9));
                case "0038":
                    return AddMenuItemsMenuItemsCategoriesId(message.Substring(9));
                case "0041":
                    return AddTable(message.Substring(9));
                case "0042":
                    return UpdateTable(message.Substring(9));
                case "0043":
                    return RemoveTable(message.Substring(9));
                case "0051":
                    return AddRecipe(message.Substring(9));
                case "0052":
                    return UpdateRecipe(message.Substring(9));
                case "0061":
                    return AddOrder(message.Substring(9));
                case "0062":
                    return AddOrdersState(message.Substring(9));
                case "0063":
                    return UpdateOrderState(message.Substring(9));
                case "0065":
                    return RemoveOrder(message.Substring(9));
                case "0067":
                    return AddOrderDetail(message.Substring(9));
                case "0071":
                    return AddReservation(message.Substring(9));
                case "0072":
                    return UpdateReservation(message.Substring(9));
                case "0073":
                    return RemoveReservation(message.Substring(9));
                case "0075":
                    return AddReservationsState(message.Substring(9));
                case "0076":
                    return AddReservationDetail(message.Substring(9));
                case "0077":
                    return RemoveReservationDetail(message.Substring(9));
                default:
                    MessageBox.Show(message);
                    return message;
            }
        }
        public string Login(string message)
        {
            return message;
        }

        public string AddUser(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.UsersList.Add(new Users(int.Parse(msg[0]), msg[1], "", msg[2], msg[3], msg[4], msg[5],
                    msg[6], msg[7], msg[8], msg[9], int.Parse(msg[10]), double.Parse(msg[11])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string UpdateUser(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var user = StorageData.UsersList.Single(u => u.Id == int.Parse(msg[0]));
                user.Login = msg[1];
                user.Email = msg[2];
                user.FirstName = msg[3];
                user.Name = msg[4];
                user.City = msg[5];
                user.AddressCode = msg[6];
                user.Street = msg[7];
                user.HouseNumber = msg[8];
                user.Phone = msg[9];
                user.UsersTypesId = int.Parse(msg[10]);
                user.Salary = double.Parse(msg[11]);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }


        public string RemoveUser(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.UsersList.Remove(StorageData.UsersList.Single(u => u.Id == int.Parse(msg[0])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddUsersType(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.UsersTypesIdList.Add(new UsersTypesId(int.Parse(msg[0]), msg[1]));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }
        public string AddIngredient(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.IngredientsList.Add(new Ingredients(int.Parse(msg[0]), msg[1], bool.Parse(msg[2])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string UpdateIngredient(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var ingredient = StorageData.IngredientsList.Single(u => u.Id == int.Parse(msg[0]));
                ingredient.Name = msg[1];
                ingredient.Available = bool.Parse(msg[2]);
                int i = 3;
                while (i < msg.Length - 1)
                {
                    StorageData.MenuItemsList.Single(m => m.Id == int.Parse(msg[i])).Available = bool.Parse(msg[i + 1]);
                    i += 2;
                }
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string UpdateIngredientAvailability(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var ingredient = StorageData.IngredientsList.Single(u => u.Id == int.Parse(msg[0]));
                ingredient.Available = bool.Parse(msg[1]);
                int i = 2;
                while (i < msg.Length -1)
                {
                    StorageData.MenuItemsList.Single(m => m.Id == int.Parse(msg[i])).Available = bool.Parse(msg[i + 1]);
                    i+= 2;
                }
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string RemoveIngredient(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.IngredientsList.Remove(StorageData.IngredientsList.Single(u => u.Id == int.Parse(msg[0])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddMenuItem(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.MenuItemsList.Add(new MenuItems(int.Parse(msg[0]), msg[1], double.Parse(msg[2]), int.Parse(msg[3]), bool.Parse(msg[4]), msg[5], msg[6]));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string UpdateMenuItem(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var menuItem = StorageData.MenuItemsList.Single(u => u.Id == int.Parse(msg[0]));
                menuItem.Name = msg[1];
                menuItem.Price = double.Parse(msg[2]);
                menuItem.MenuItemsCategoriesId = int.Parse(msg[3]);
                menuItem.Description = msg[4];
                menuItem.Image = msg[5];
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string RemoveMenuItem(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.RecipesList.Remove(StorageData.RecipesList.Single(r => r.MenuItemsId == int.Parse(msg[0])));
                var menuItemIngredientsCount = StorageData.MenuItemsIngredientsList.Where(mi => mi.MenuItemsId == int.Parse(msg[0])).Count();
                if (menuItemIngredientsCount > 0)
                {
                        int i = 0;
                        while (i < menuItemIngredientsCount - 1)
                        {
                            StorageData.MenuItemsIngredientsList.Remove(StorageData.MenuItemsIngredientsList.First(mi => mi.MenuItemsId == int.Parse(msg[0])));
                            i++;
                        }
                   
                }
                StorageData.MenuItemsList.Remove(StorageData.MenuItemsList.Single(u => u.Id == int.Parse(msg[0])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddMenuItemIngredient(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.MenuItemsIngredientsList.Add(new MenuItemsIngredients(int.Parse(msg[0]), int.Parse(msg[1])));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string RemoveMenuItemIngredient(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.MenuItemsIngredientsList.Remove(StorageData.MenuItemsIngredientsList.Single(u => u.MenuItemsId == int.Parse(msg[0]) && u.IngredientsId == int.Parse(msg[1])));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddMenuItemsMenuItemsCategoriesId(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.MenuItemsCategoriesList.Add(new MenuItemsCategories(int.Parse(msg[0]), msg[1]));
            }
            catch (Exception)
            {
                return "0";
            }
            return "0";
        }

        public string AddTable(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.TablesList.Add(new Tables(int.Parse(msg[0]), int.Parse(msg[1]), short.Parse(msg[2]), double.Parse(msg[3])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }
        public string UpdateTable(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var table = StorageData.TablesList.Single(u => u.Id == int.Parse(msg[0]));
                table.Number = int.Parse(msg[1]);
                table.Size = short.Parse(msg[2]);
                table.ReservationCost = double.Parse(msg[3]);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string RemoveTable(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.TablesList.Remove(StorageData.TablesList.Single(u => u.Id == int.Parse(msg[0])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddRecipe(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.RecipesList.Add(new Recipes(int.Parse(msg[0]), msg[1]));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string UpdateRecipe(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var recipe = StorageData.RecipesList.Single(u => u.MenuItemsId == int.Parse(msg[0]));
                recipe.Description = msg[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddOrder(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.OrdersList.Add(new Orders(int.Parse(msg[0]), DateTime.Parse(msg[1]), int.Parse(msg[2]), int.Parse(msg[3])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddOrdersState(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.OrdersStatesList.Add(new OrdersStates(int.Parse(msg[0]), msg[1]));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string UpdateOrderState(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var order = StorageData.OrdersList.Single(a => a.Id == int.Parse(msg[0]));
                order.OrderStateId = int.Parse(msg[1]);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string RemoveOrder(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var orderDetailsCount = StorageData.OrdersDetailsList.Where(o => o.OrdersId == int.Parse(msg[0])).Count();
                int i = 0;
                while ( i < orderDetailsCount)
                {
                    StorageData.OrdersDetailsList.Remove(StorageData.OrdersDetailsList.First(o => o.OrdersId == int.Parse(msg[0])));
                    i++;
                }
                StorageData.OrdersList.Remove(StorageData.OrdersList.Single(a => a.Id == int.Parse(msg[0])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddOrderDetail(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.OrdersDetailsList.Add(new OrdersDetails(int.Parse(msg[0]), int.Parse(msg[1]), short.Parse(msg[2])));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddReservation(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
                try
                {
                    StorageData.ReservationsList.Add(new Reservations(int.Parse(msg[0]), DateTime.Parse(msg[1]), DateTime.Parse(msg[2]),
                        msg[3], msg[4], msg[5], int.Parse(msg[6])));
                Update();
            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return "0";
                }
            return "0";
        }
        public string UpdateReservation(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var reservation = StorageData.ReservationsList.Single(u => u.Id == int.Parse(msg[0]));
                reservation.Date = DateTime.Parse(msg[1]);
                reservation.DateOn = DateTime.Parse(msg[2]);
                reservation.FirstName = msg[3];
                reservation.Name = msg[4];
                reservation.Phone = msg[5];
                reservation.ReservationsStatesId = int.Parse(msg[6]);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string RemoveReservation(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                var reservationDetailsCount = StorageData.ReservationsDetailsList.Where(o => o.ReservationsId == int.Parse(msg[0])).Count();
                int i = 0;
                while (i < reservationDetailsCount)
                {
                    StorageData.ReservationsDetailsList.Remove(StorageData.ReservationsDetailsList.First(o => o.ReservationsId == int.Parse(msg[0])));
                    i++;
                }
                StorageData.ReservationsList.Remove(StorageData.ReservationsList.Single(r => r.Id == int.Parse(msg[0])));
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddReservationsState(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.ReservationsStatesList.Add(new ReservationsStates(int.Parse(msg[0]), msg[1]));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string AddReservationDetail(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.ReservationsDetailsList.Add(new ReservationsDetails(int.Parse(msg[0]), int.Parse(msg[1])));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }

        public string RemoveReservationDetail(string message)
        {
            msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            try
            {
                StorageData.ReservationsDetailsList.Remove(StorageData.ReservationsDetailsList.Single(u => u.ReservationsId == int.Parse(msg[0]) && u.TablesId == int.Parse(msg[1])));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "0";
            }
            return "0";
        }
    }
}
