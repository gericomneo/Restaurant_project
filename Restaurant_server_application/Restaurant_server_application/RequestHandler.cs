using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using pl.edu.wat.wcy.pz.restaurant_server_application.Models;

namespace pl.edu.wat.wcy.pz.restaurant_server_application
{
    public class RequestHandler
    {
        readonly int loggedType;
        public static List<DateTimeOffset> QuequeReservations = new List<DateTimeOffset>();
        public static List<DateTimeOffset> QuequeAddOrder = new List<DateTimeOffset>();
        public static List<DateTimeOffset> QuequeChangeOrderState = new List<DateTimeOffset>();
        public static List<DateTimeOffset> QuequeAddMenuItems = new List<DateTimeOffset>();


        public RequestHandler(int type)
        {
            loggedType = type;
        }
        public string RequestRecognition(string message)
        {
            string response;
            string request = message.Substring(0, 4);
            switch (loggedType)
            {
                case 0:
                    switch (request)
                    {
                        case "0008":
                            return Login(message.Substring(9));
                        default:
                            return "0004<EOR>";
                    }

                case 1:
                    switch (request)
                    {
                        case "0000":
                            return "0000<EOP>";
                        case "0010":
                            return GetUsers(message.Substring(9));
                        case "0011":
                            return AddUser(message.Substring(9));
                        case "0012":
                            return EditUser(message.Substring(9));
                        case "0013":
                            return RemoveUser(message.Substring(9));
                        case "0014":
                            return GetUsersTypes(message.Substring(9));
                        case "0020":
                            return GetIngredients(message.Substring(9));
                        case "0021":
                            return AddIngredient(message.Substring(9));
                        case "0022":
                            return EditIngredient(message.Substring(9));
                        case "0023":
                            return EditIngredientAvailability(message.Substring(9));
                        case "0024":
                            return RemoveIngredient(message.Substring(9));
                        case "0030":
                            return GetMenuItems(message.Substring(9));
                        case "0031":
                            Queue(QuequeAddOrder);
                            response = AddMenuItem(message.Substring(9));
                            QuequeAddOrder.RemoveAt(0);
                            return response;
                        case "0032":
                            return EditMenuItem(message.Substring(9));
                        case "0033":
                            return RemoveMenuItem(message.Substring(9));
                        case "0034":
                            return GetMenuItemsIngredients(message.Substring(9));
                        case "0037":
                            return GetMenuItemsCategories(message.Substring(9));
                        case "0040":
                            return GetTables(message.Substring(9));
                        case "0041":
                            return AddTable(message.Substring(9));
                        case "0042":
                            return EditTable(message.Substring(9));
                        case "0043":
                            return RemoveTable(message.Substring(9));
                        case "0050":
                            return GetRecipes(message.Substring(9));
                        case "0052":
                            return EditRecipe(message.Substring(9));
                        case "0060":
                            return GetOrders(message.Substring(9));
                        case "0061":
                            Queue(QuequeAddOrder);
                            response = AddOrder(message.Substring(9));
                            QuequeAddOrder.RemoveAt(0);
                            return response;
                        case "0062":
                            return GetOrdersStates(message.Substring(9));
                        case "0063":
                            Queue(QuequeChangeOrderState);
                            response = ChangeOrderState(message.Substring(9));
                            QuequeChangeOrderState.RemoveAt(0);
                            return response;
                        case "0064":
                            return IncreaseOrderState(message.Substring(9));
                        case "0065":
                            return RemoveOrder(message.Substring(9));
                        case "0066":
                            return GetOrdersDetails(message.Substring(9));
                        case "0067":
                            return CloseBill(message.Substring(9));
                        case "0070":
                            return GetReservations(message.Substring(9));
                        case "0071":
                            Queue(QuequeReservations);
                            response = AddReservation(message.Substring(9));
                            QuequeReservations.RemoveAt(0);
                            return response;
                        case "0072":
                            Queue(QuequeReservations);
                            response = EditReservation(message.Substring(9));
                            QuequeReservations.RemoveAt(0);
                            return response;
                        case "0073":
                            return RemoveReservation(message.Substring(9));
                        case "0074":
                            return GetReservationsStates(message.Substring(9));
                        case "0075":
                            return GetReservationsDetails(message.Substring(9));
                        default:
                            return "0004<EOR>";
                    }

                case 2:
                    switch (request)
                    {
                        case "0000":
                            return "0000<EOP>";
                        case "0020":
                            return GetIngredients(message.Substring(9));
                        case "0023":
                            return EditIngredientAvailability(message.Substring(9));
                        case "0030":
                            return GetMenuItems(message.Substring(9));
                        case "0034":
                            return GetMenuItemsIngredients(message.Substring(9));
                        case "0037":
                            return GetMenuItemsCategories(message.Substring(9));
                        case "0050":
                            return GetRecipes(message.Substring(9));
                        case "0060":
                            return GetOrders(message.Substring(9));
                        case "0062":
                            return GetOrdersStates(message.Substring(9));
                        case "0064":
                            return IncreaseOrderState(message.Substring(9));
                        case "0066":
                            return GetOrdersDetails(message.Substring(9));
                        default:
                            return "0004<EOR>";
                    }

                case 3:
                    switch (request)
                    {
                        case "0000":
                            return "0000<EOP>";
                        case "0030":
                            return GetMenuItems(message.Substring(9));
                        case "0031":
                            return AddMenuItem(message.Substring(9));
                        case "0034":
                            return GetMenuItemsIngredients(message.Substring(9));
                        case "0037":
                            return GetMenuItemsCategories(message.Substring(9));
                        case "0040":
                            return GetTables(message.Substring(9));
                        case "0070":
                            return GetReservations(message.Substring(9));
                        case "0071":
                            return AddReservation(message.Substring(9));
                        case "0072":
                            return EditReservation(message.Substring(9));
                        case "0073":
                            return RemoveReservation(message.Substring(9));
                        case "0074:":
                            return GetReservationsStates(message.Substring(9));
                        case "0075":
                            return GetReservationsDetails(message.Substring(9));
                        default:
                            return "0004<EOR>";
                    }

                case 4:
                    switch (request)
                    {
                        case "0000":
                            return "0000<EOP>";
                        case "0030":
                            return GetMenuItems(message.Substring(9));
                        case "0034":
                            return GetMenuItemsIngredients(message.Substring(9));
                        case "0037":
                            return GetMenuItemsCategories(message.Substring(9));
                        case "0040":
                            return GetTables(message.Substring(9));
                        case "0060":
                            return GetOrders(message.Substring(9));
                        case "0061":
                            return AddOrder(message.Substring(9));
                        case "0062":
                            return GetOrdersStates(message.Substring(9));
                        case "0064":
                            return IncreaseOrderState(message.Substring(9));
                        case "0066":
                            return GetOrdersDetails(message.Substring(9));
                        case "0067":
                            return CloseBill(message.Substring(9));
                        default:
                            return "0004<EOR>";
                    }
                default:
                    return "0004<EOR>";
            }
        }

        private void Queue(List<DateTimeOffset> QueueList)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            QueueList.Add(time);
            while (QueueList.First() != time)
            {
                Thread.Sleep(1000);
            }
        }

        public string Login(string message)
        {
            string response = "0001<EOP>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {   
                    if (context.Users.AsEnumerable().Any(a => a.Login == msg[0]))
                    {
                        var user = context.Users.AsEnumerable().Single(a => a.Login == msg[0]);
                        if (user.Password == msg[1])
                        {
                            response += user.UsersTypesId.ToString() + "<EOR>";
                        }
                        else
                        {
                            return "0005<EOR>";
                        }
                    }
                    else
                    {
                        return "0005<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0005<EOR>";
                }
            }
            return response;
        }
        public string GetUsers(string message)
        {
            string response = "0002<EOP>";

            using (var context = new DatabaseContext())
            {
                try
                {
                    var users = context.Users.AsEnumerable();
                    if (users.Count() > 0)
                    {
                        foreach (Users user in users)
                        {
                            response += "0011<EOP>" + user.Id + "<EOP>" + user.Login + "<EOP>" + user.Email + "<EOP>" + user.FirstName + "<EOP>" + user.Name + "<EOP>" + user.City + "<EOP>"
                        + user.AddressCode + "<EOP>" + user.Street + "<EOP>" + user.HouseNumber.ToString() + "<EOP>" + user.Phone + "<EOP>" + user.UsersTypesId.ToString() + "<EOP>" + user.Salary + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }

            return response;
        }

        public string AddUser(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    if (context.Users.AsEnumerable().Any(u => u.Login == msg[0]))
                    {
                        return "0002<EOR>0006<EOR>";
                    }
                    else
                    {
                        var user = new Users()
                        {
                            Login = msg[0],
                            Password = msg[1],
                            Email = msg[2],
                            FirstName = msg[3],
                            Name = msg[4],
                            City = msg[5],
                            AddressCode = msg[6],
                            Street = msg[7],
                            HouseNumber = msg[8],
                            Phone = msg[9],
                            UsersTypesId = int.Parse(msg[10]),
                            Salary = double.Parse(msg[11])
                        };
                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            using (var context = new DatabaseContext())
            {
                try
                {
                    var user = context.Users.AsEnumerable().Last();
                    response += "#1#" + "0011<EOP>" + user.Id + "<EOP>" + msg[0] + "<EOP>" + msg[2] + "<EOP>" + msg[3] + "<EOP>" + msg[4] + "<EOP>" + 
                        msg[5] + "<EOP>" + msg[6] + "<EOP>" + msg[7] + "<EOP>" + msg[8] + "<EOP>" + msg[9] + "<EOP>" + msg[10] + "<EOP>" + msg[11] + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string EditUser(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    if (context.Users.AsEnumerable().Any(u => u.Login == msg[0]))
                    {
                        return "0006<EOR>";
                    }
                    else
                    {
                        var user = context.Users.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                        user.Login = msg[1];
                        user.Password = msg[2];
                        user.Email = msg[3];
                        user.FirstName = msg[4];
                        user.Name = msg[5];
                        user.City = msg[6];
                        user.AddressCode = msg[7];
                        user.Street = msg[8];
                        user.HouseNumber = msg[9];
                        user.Phone = msg[10];
                        user.UsersTypesId = int.Parse(msg[11]);
                        user.Salary = double.Parse(msg[12]);
                        context.SaveChanges();
                        response += "#1#" + "0012<EOP>" + msg[0] + "<EOP>" + msg[1] + "<EOP>" + msg[3] + "<EOP>" + msg[4] + "<EOP>" + msg[5] + "<EOP>"
                            + msg[6] + "<EOP>" + msg[7] + "<EOP>" + msg[8] + "<EOP>" + msg[9] + "<EOP>" + msg[10] + "<EOP>" + msg[11] + "<EOP>" + msg[12] + "<EOR>";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string RemoveUser(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var users = context.Users;
                    var user = users.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    users.Remove(user);
                    context.SaveChanges();
                    response += "#1#" + "0013<EOP>" + msg[0] + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetUsersTypes(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var usersTypes = context.UsersTypes.AsEnumerable();

                    if (usersTypes.Count() > 0)
                    {
                        foreach (UsersTypes usersType in usersTypes)
                        {
                            response += "0015<EOP>" + usersType.Id + "<EOP>" + usersType.Name + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetIngredients(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var ingredients = context.Ingredients.AsEnumerable();

                    if (ingredients.Count() > 0)
                    {
                        foreach (Ingredients ingredient in ingredients)
                        {
                            response += "0021<EOP>" + ingredient.Id + "<EOP>" + ingredient.Name + "<EOP>" + ingredient.Available.ToString() + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string AddIngredient(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var ingredient = new Ingredients()
                    {
                        Name = msg[0],
                        Available = msg[1].Equals("0")
                    };
                    context.Ingredients.Add(ingredient);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }

            using (var context = new DatabaseContext())
            {
                try
                {
                    var ingredientId = context.Ingredients.AsEnumerable().Last().Id;
                    response += "#12#" + "0021<EOP>" + ingredientId + "<EOP>" + msg[0] + "<EOP>" + msg[1].Equals("0") + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string EditIngredient(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var ingredients = context.Ingredients;
                    var ingredient = ingredients.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    ingredient.Name = msg[1];
                    ingredient.Available = msg[2].Equals("0");
                    context.SaveChanges();
                    response += "#12#" + "0022<EOP>" + msg[0] + "<EOP>" + msg[1] + "<EOP>" + msg[2].Equals("0");

                    var menuItems = context.MenuItems.AsEnumerable().Where(a => context.MenuItemsIngredients.AsEnumerable().Any(b => b.IngredientsId == int.Parse(msg[0]) && b.MenuItemsId == a.Id));
                    foreach (MenuItems menuItem in menuItems)
                    {
                        bool available = true;
                        foreach (MenuItemsIngredients menuItemsIngredients in menuItem.MenuItemsIngredients)
                        {
                            if (!menuItemsIngredients.Ingredients.Available) available = false;
                        }
                        if (menuItem.Available != available)
                        {
                            menuItem.Available = !menuItem.Available;
                            response += "<EOP>" + menuItem.Id + "<EOP>" + menuItem.Available.ToString();
                        }
                    }
                    response += "<EOR>";
                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string EditIngredientAvailability(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var ingredient = context.Ingredients.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    ingredient.Available = !ingredient.Available;
                    response += "#1234#" + "0023<EOP>"  + int.Parse(msg[0]) + "<EOP>" + ingredient.Available.ToString();
;
                    var menuItems = context.MenuItems.AsEnumerable().Where(a => context.MenuItemsIngredients.AsEnumerable().Any(b => b.IngredientsId == int.Parse(msg[0]) && b.MenuItemsId == a.Id));
                    foreach (MenuItems menuItem in menuItems)
                    {
                        bool available = true;
                        foreach (MenuItemsIngredients menuItemsIngredients in menuItem.MenuItemsIngredients)
                        {
                            if (!menuItemsIngredients.Ingredients.Available) available = false;
                        }
                        if (menuItem.Available != available)
                        {
                            menuItem.Available = !menuItem.Available;
                            response += "<EOP>" + menuItem.Id + "<EOP>" + menuItem.Available.ToString();
                        }
                    }
                    response += "<EOR>";
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string RemoveIngredient(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var ingredients = context.Ingredients;
                    var ingredient = ingredients.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    ingredients.Remove(ingredient);
                    context.SaveChanges();
                    response += "#12#" + "0024<EOP>" + msg[0] + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetMenuItems(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var menuItems = context.MenuItems.AsEnumerable();
                    if (menuItems.Count() > 0)
                    {
                        foreach (MenuItems menuItem in menuItems)
                        {
                            response += "0031<EOP>" + menuItem.Id + "<EOP>" + menuItem.Name + "<EOP>" + menuItem.Price.ToString() + "<EOP>" + menuItem.MenuItemsCategoriesId.ToString() + "<EOP>" +
                                menuItem.Available.ToString() + "<EOP>" + menuItem.Description + "<EOP>" + menuItem.Image + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string AddMenuItem(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var menuItem = new MenuItems()
                    {
                        Name = msg[0],
                        Price = double.Parse(msg[1]),
                        MenuItemsCategoriesId = int.Parse(msg[2]),
                        Available = true,
                        Description = msg[3],
                        Image = msg[4]
                    };
                    context.MenuItems.Add(menuItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            using (var context = new DatabaseContext())
            {
                try
                {
                    int menuItemId = context.MenuItems.AsEnumerable().Last().Id;
                    int i = 5;
                    while (i < msg.Length - 1)
                    {
                        var menuItemIngredient = new MenuItemsIngredients()
                        {
                            MenuItemsId = menuItemId,
                            IngredientsId = int.Parse(msg[i])
                        };
                        i++;
                        context.MenuItemsIngredients.Add(menuItemIngredient);
                    }
                    var recipe = new Recipes()
                    {
                        MenuItemsId = menuItemId,
                        Description = "Brak przepisu."
                    };
                    context.Recipes.Add(recipe);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }

            using (var context = new DatabaseContext())
            {
                try
                {
                    var menuItem = context.MenuItems.AsEnumerable().Last();
                    menuItem.Available = context.Ingredients.All(i => i.Available == true && context.MenuItemsIngredients.Any(mi => mi.IngredientsId == i.Id && mi.MenuItemsId == menuItem.Id));
                    context.SaveChanges();
                    response += "#1234#" + "0031<EOP>" + menuItem.Id + "<EOP>" + menuItem.Name + "<EOP>" + menuItem.Price.ToString() + "<EOP>" + menuItem.MenuItemsCategoriesId.ToString() + "<EOP>" +
                    menuItem.Available.ToString() + "<EOP>" + menuItem.Description + "<EOP>" + menuItem.Image + "<EOP>" + "<EOR>";
                    int j = 5;
                    while (j < msg.Length - 1)
                    {
                        response += "0035<EOP>" + menuItem.Id + "<EOP>" + msg[j] + "<EOR>";
                        j++;
                    }
                    response += "0051<EOP>" + menuItem.Id + "<EOP>" + "Brak przepisu.<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string EditMenuItem(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var menuItem = context.MenuItems.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    menuItem.Name = msg[1];
                    menuItem.Price = double.Parse(msg[2]);
                    menuItem.MenuItemsCategoriesId = int.Parse(msg[3]);
                    menuItem.Description = msg[4];
                    menuItem.Image = msg[5];
                    response += "#1234#" + "0032<EOP>" + msg[0] + "<EOP>" + msg[1] + "<EOP>" + msg[2] + "<EOP>" + msg[3] + "<EOP>"  + msg[4] + "<EOP>" + msg[5] + "<EOP> " + "<EOR>";
                    int i = 6;
                    if (i < msg.Length - 1)
                    {
                        if (msg[i] == "0036")
                        {
                            i++;
                            while ((i < msg.Length - 1) && (msg[i] != "0035"))
                            {
                                var menuItemIngredients = context.MenuItemsIngredients;
                                var menuItemIngredient = menuItemIngredients.AsEnumerable().Single(a => a.MenuItemsId == int.Parse(msg[0]) && a.IngredientsId == int.Parse(msg[i]));
                                menuItemIngredients.Remove(menuItemIngredient);
                                response += "0036<EOP>" + msg[0] + "<EOP>" + msg[i] + "<EOR>";
                                i++;
                            }
                        }

                    }
                    if (i < msg.Length - 1)
                    {
                        if (msg[i] == "0035")
                        {
                            i++;
                            while (i < msg.Length - 1)
                            {
                                var menuItemIngredient = new MenuItemsIngredients()
                                {
                                    MenuItemsId = int.Parse(msg[0]),
                                    IngredientsId = int.Parse(msg[i])
                                };
                                response += "0035<EOP>" + msg[0] + "<EOP>" + msg[i] + "<EOR>";
                                context.MenuItemsIngredients.Add(menuItemIngredient);
                                i++;
                            }
                        }
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string RemoveMenuItem(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);

            using (var context = new DatabaseContext())
            {
                try
                {
                    var recipes = context.Recipes;
                    var recipe = recipes.AsEnumerable().Single(a => a.MenuItemsId == int.Parse(msg[0]));
                    recipes.Remove(recipe);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }

            using (var context = new DatabaseContext())
            {
                try
                {
                    var menuItemsIngredients = context.MenuItemsIngredients.AsEnumerable().Where(a => a.MenuItemsId == int.Parse(msg[0]));
                    foreach (MenuItemsIngredients menuItemIngredient in menuItemsIngredients)
                    {
                        context.MenuItemsIngredients.Remove(menuItemIngredient);
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }

            using (var context = new DatabaseContext())
            {
                try
                {
                    var menuItems = context.MenuItems;
                    var menuItem = menuItems.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    menuItems.Remove(menuItem);
                    context.SaveChanges();
                    response += "#1234#" + "0033<EOP>" + msg[0] + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetMenuItemsIngredients(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var menuItemsIngredients = context.MenuItemsIngredients.AsEnumerable();

                    if (menuItemsIngredients.Count() > 0)
                    {
                        foreach (MenuItemsIngredients menuItemIngredient in menuItemsIngredients)
                        {
                            response += "0035<EOP>" + menuItemIngredient.MenuItemsId + "<EOP>" + menuItemIngredient.IngredientsId + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetMenuItemsCategories(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var menuItemsCategories = context.MenuItemsCategories.AsEnumerable();

                    if (menuItemsCategories.Count() > 0)
                    {
                        foreach (MenuItemsCategories menuItemCategory in menuItemsCategories)
                        {
                            response += "0038<EOP>" + menuItemCategory.Id + "<EOP>" + menuItemCategory.Name + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetTables(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var tables = context.Tables.AsEnumerable();

                    if (tables.Count() > 0)
                    {
                        foreach (Tables table in tables)
                        {
                            response += "0041<EOP>" + table.Id + "<EOP>" + table.Number + "<EOP>" + table.Size + "<EOP>" + table.ReservationCost + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string AddTable(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var table = new Tables()
                    {
                        Number = int.Parse(msg[0]),
                        Size = short.Parse(msg[1]),
                        ReservationCost = double.Parse(msg[2])
                    };
                    context.Tables.Add(table);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            using (var context = new DatabaseContext())
            {
                try
                {
                    var table = context.Tables.AsEnumerable().Last();
                    response += "#134#" + "0041<EOP>" + table.Id + "<EOP>" + table.Number.ToString() + "<EOP>" + table.Size.ToString() + "<EOP>" + table.ReservationCost.ToString() + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string EditTable(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var table = context.Tables.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    table.Number = int.Parse(msg[1]);
                    table.Size = short.Parse(msg[2]);
                    table.ReservationCost = double.Parse(msg[3]);
                    context.SaveChanges();
                    response += "#134#" + "0042<EOP>" + msg[0] + "<EOP>" + msg[1] + "<EOP>" + msg[2] + "<EOP>" + msg[3] + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string RemoveTable(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var tables = context.Tables;
                    var table = tables.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    tables.Remove(table);
                    context.SaveChanges();
                    response += "#134#" + "0043<EOP>" + msg[0] + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetRecipes(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var recipes = context.Recipes.AsEnumerable();

                    if (recipes.Count() > 0)
                    {
                        foreach (Recipes recipe in recipes)
                        {
                            response += "0051<EOP>" + recipe.MenuItemsId + "<EOP>" + recipe.Description + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string EditRecipe(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var recipes = context.Recipes;
                    var recipe = recipes.AsEnumerable().Single(a => a.MenuItemsId == int.Parse(msg[0]));
                    if (msg[1] != "")
                    {
                        recipe.Description = msg[1];
                        context.SaveChanges();
                        response += "#12#" + "0052<EOP>" + msg[0] + "<EOP>" + msg[1] + "<EOR>";
                    }
                    else
                    {
                        recipe.Description = "Brak przepisu.";
                        context.SaveChanges();
                        response += "#12#" + "0052<EOP>" + msg[0] + "<EOP>" + "Brak przepisu." + "<EOR>";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetOrders(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var orders = context.Orders.AsEnumerable();
                    if (orders.Count() > 0)
                    {
                        foreach (Orders order in orders)
                        {
                            response += "0061<EOP>" + order.Id.ToString() + "<EOP>" + order.Date.ToString() + "<EOP>" + order.TablesId.ToString() + "<EOP>" + order.OrdersStatesId.ToString() + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string AddOrder(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var order = new Orders()
                    {
                        Date = DateTime.Parse(msg[0]),
                        TablesId = int.Parse(msg[1]),
                        OrdersStatesId = 1
                    };
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            using (var context = new DatabaseContext())
            {
                try
                {
                    var orderId = context.Orders.AsEnumerable().Last().Id;
                    response += "#124#" + "0061<EOP>" + orderId + "<EOP>" + msg[0] + "<EOP>" + msg[1] + "<EOP>" + "1" + "<EOR>";
                    int i = 2;
                    while (i < msg.Length -1)
                    {
                        var orderdetail = new OrdersDetails()
                        {
                            OrdersId = orderId,
                            MenuItemsId = int.Parse(msg[i]),
                            Amount = short.Parse(msg[i + 1])
                        };
                        response += "0067<EOP>" + orderId.ToString() + "<EOP>" + msg[i] + "<EOP>" + msg[i + 1] + "<EOR>";
                        i += 2;
                        context.OrdersDetails.Add(orderdetail);
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetOrdersStates(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var states = context.OrdersStates.AsEnumerable();

                    if (states.Count() > 0)
                    {
                        foreach (OrdersStates state in states)
                        {
                            response += "0062<EOP>" + state.Id.ToString() + "<EOP>" + state.Name + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string ChangeOrderState(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var orders = context.Orders;
                    var order = orders.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    order.OrdersStatesId = int.Parse(msg[1]);
                    context.SaveChanges();
                    response += "#1234#" + "0063<EOP>" + msg[0] + "<EOP>" + msg[1] + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string IncreaseOrderState(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var orders = context.Orders;
                    var order = orders.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    order.OrdersStatesId = order.OrdersStatesId + 1;
                    context.SaveChanges();
                    response += "#1234#" + "0063<EOP>" + msg[0] + "<EOP>" + order.OrdersStatesId.ToString() + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string RemoveOrder(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var orderDetails = context.OrdersDetails.AsEnumerable().Where(a => a.OrdersId == int.Parse(msg[0]));
                    foreach (OrdersDetails orderDetail in orderDetails)
                    {
                        context.OrdersDetails.Remove(orderDetail);
                    }
                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            using (var context = new DatabaseContext())
            {
                try
                {
                    var orders = context.Orders;
                    var order = orders.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    orders.Remove(order);
                    context.SaveChanges();
                    response += "#124#" + "0065<EOP>" + msg[0] + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetOrdersDetails(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var orderdetails = context.OrdersDetails.AsEnumerable();
                    if (orderdetails.Count() > 0)
                    {
                        foreach (OrdersDetails orderdetail in orderdetails)
                        {
                            response += "0067<EOP>" + orderdetail.OrdersId.ToString() + "<EOP>" + orderdetail.MenuItemsId.ToString() + "<EOP>" + orderdetail.Amount.ToString() + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string CloseBill(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    int i = 0;
                    while (i < msg.Length - 1)
                    {
                        var orders = context.Orders;
                        var order = orders.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                        order.OrdersStatesId = order.OrdersStatesId + 1;
                        response += "#1234#" + "0063<EOP>" + msg[0] + "<EOP>" + order.OrdersStatesId.ToString() + "<EOR>";
                        i++;
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetReservations(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var reservations = context.Reservations.AsEnumerable();

                    if (reservations.Count() > 0)
                    {
                        foreach (Reservations reservation in reservations)
                        {
                            response += "0071<EOP>" + reservation.Id.ToString() + "<EOP>" + reservation.Date.ToString() + "<EOP>" + reservation.DateOn.ToString() + "<EOP>" + reservation.FirstName + "<EOP>" +
                                reservation.Name + "<EOP>" + reservation.Phone + "<EOP>" + reservation.ReservationsStatesId.ToString() + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string AddReservation(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            List<int> newReservationTablesId = new List<int>();
            using (var context = new DatabaseContext())
            {
                try
                {
                    int i = 5;
                    while (i < msg.Length - 1)
                    {
                        newReservationTablesId.Add(int.Parse(msg[i]));
                        i++;
                    }

                   foreach (Reservations res in context.Reservations.AsEnumerable().Where(r => r.DateOn == DateTime.Parse(msg[1])))
                    {
                        if (context.ReservationsDetails.Where(rd => newReservationTablesId.Any(nr => nr == rd.TablesId)).Count() > 0)
                        {
                            return "0004<EOR>";
                        }
                    }
                    var reservation = new Reservations()
                    {
                        Date = DateTime.Parse(msg[0]),
                        DateOn = DateTime.Parse(msg[1]),
                        FirstName = msg[2],
                        Name = msg[3],
                        Phone = msg[4],
                        ReservationsStatesId = 1
                    };
                    context.Reservations.Add(reservation);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            using (var context = new DatabaseContext())
            {
                try
                {
                    int reservationId = context.Reservations.AsEnumerable().Last().Id;
                    response += "#13#" + "0071<EOP>" + reservationId.ToString() + "<EOP>" + msg[0] + "<EOP>" + msg[1] + "<EOP>" + msg[2] + "<EOP>" + msg[3] + "<EOP>" + msg[4] + "<EOP>" + "1" + "<EOR>";
                    int i = 5;
                    while (i < msg.Length - 1)
                    {
                        var reservationdetail = new ReservationsDetails()
                        {
                            ReservationsId = reservationId,
                            TablesId = int.Parse(msg[i])
                        };
                        context.ReservationsDetails.Add(reservationdetail);
                        response += "0076<EOP>" + reservationId.ToString() + "<EOP>" + msg[i] + "<EOR>";
                        i++;
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string EditReservation(string message)
        { 
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var reservations = context.Reservations;
                    var reservation = reservations.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    reservation.Date = DateTime.Parse(msg[1]);
                    reservation.DateOn = DateTime.Parse(msg[2]);
                    reservation.FirstName = msg[3];
                    reservation.Name = msg[4];
                    reservation.Phone = msg[5];
                    reservation.ReservationsStatesId = int.Parse(msg[6]);
                    response += "#13#" + "0072<EOP>" + msg[0] + "<EOP>" + msg[1] + "<EOP>" + msg[2] + "<EOP>" + msg[3] + "<EOP>" + msg[4] + "<EOP>" + msg[5] + "<EOP>" + msg[6] + "<EOR>";

                    int i = 7;
                    if (i < msg.Length - 1)
                    {
                        if (msg[i] == "0077")
                        {
                            i++;
                            while ((i < msg.Length - 1) && (msg[i] != "0076"))
                            {
                                var reservationDetails = context.ReservationsDetails;
                                var reservationDetail = reservationDetails.AsEnumerable().Single(a => a.ReservationsId == int.Parse(msg[0]) && a.TablesId == int.Parse(msg[i]));
                                reservationDetails.Remove(reservationDetail);
                                response += "0077<EOP>" + msg[0] + "<EOP>" + msg[i] + "<EOR>";
                                i++;
                            }
                        }

                    }
                    if (i < msg.Length - 1)
                    {
                        if (msg[i] == "0076")
                        {
                            i++;
                            while (i < msg.Length - 1)
                            {
                                var reservationDetail = new ReservationsDetails()
                                {
                                    ReservationsId = int.Parse(msg[0]),
                                    TablesId = int.Parse(msg[i])
                                };
                                context.ReservationsDetails.Add(reservationDetail);
                                response += "0076<EOP>" + msg[0] + "<EOP>" + msg[i] + "<EOR>";
                                i++;
                            }
                        }
                    }
                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string RemoveReservation(string message)
        {
            string response = "0003<EOR>";
            string[] msg = message.Split(new string[] { "<EOP>" }, StringSplitOptions.None);
            using (var context = new DatabaseContext())
            {
                try
                {
                    var reservationDetails = context.ReservationsDetails.AsEnumerable().Where(a => a.ReservationsId == int.Parse(msg[0]));
                    foreach (ReservationsDetails reservationDetail in reservationDetails)
                    {
                        context.ReservationsDetails.Remove(reservationDetail);
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            using (var context = new DatabaseContext())
            {
                try
                {
                    var reservations = context.Reservations;
                    var reservation = context.Reservations.AsEnumerable().Single(a => a.Id == int.Parse(msg[0]));
                    reservations.Remove(reservation);
                    context.SaveChanges();
                    response += "#14#" + "0073<EOP>" + msg[0] + "<EOR>";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetReservationsStates(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var states = context.ReservationsStates.AsEnumerable();

                    if (states.Count() > 0)
                    {
                        foreach (ReservationsStates state in states)
                        {
                            response += "0075<EOP>" + state.Id.ToString() + "<EOP>" + state.Name + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }

        public string GetReservationsDetails(string message)
        {
            string response = "0002<EOP>";
            using (var context = new DatabaseContext())
            {
                try
                {
                    var reservationsDetails = context.ReservationsDetails.AsEnumerable();
                    if (reservationsDetails.Count() > 0)
                    {
                        foreach (ReservationsDetails reservationsDetail in reservationsDetails)
                        {
                            response += "0076<EOP>" + reservationsDetail.ReservationsId.ToString() + "<EOP>" + reservationsDetail.TablesId.ToString() + "<EOR>";
                        }
                    }
                    else
                    {
                        response += "0003<EOR>";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "0004<EOR>";
                }
            }
            return response;
        }
    }
}

