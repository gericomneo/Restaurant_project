using pl.edu.wat.wcy.pz.restaurant_server_application.Models;
using System.Data.Entity;

namespace pl.edu.wat.wcy.pz.restaurant_server_application
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base ("name=DatabaseContext")
        { }

        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<MenuItemsCategories> MenuItemsCategories { get; set; }
        public virtual DbSet<MenuItemsIngredients> MenuItemsIngredients { get; set; }
        public virtual DbSet<MenuItems> MenuItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersDetails> OrdersDetails { get; set; }
        public virtual DbSet<OrdersStates> OrdersStates { get; set; }
        public virtual DbSet<Recipes> Recipes { get; set; }
        public virtual DbSet<ReservationsDetails> ReservationsDetails { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<ReservationsStates> ReservationsStates { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersTypes> UsersTypes { get; set; }
    }
}
