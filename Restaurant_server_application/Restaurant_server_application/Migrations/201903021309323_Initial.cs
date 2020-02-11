namespace pl.edu.wat.wcy.pz.restaurant_server_application.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Server.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Server.MenuItemIngredients",
                c => new
                    {
                        MenuItemsId = c.Int(nullable: false),
                        IngredientsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuItemsId, t.IngredientsId })
                .ForeignKey("Server.Ingredients", t => t.IngredientsId, cascadeDelete: true)
                .ForeignKey("Server.MenuItems", t => t.MenuItemsId, cascadeDelete: true)
                .Index(t => t.MenuItemsId)
                .Index(t => t.IngredientsId);
            
            CreateTable(
                "Server.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        MenuItemsCategoriesId = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Server.MenuItemsCategories", t => t.MenuItemsCategoriesId, cascadeDelete: true)
                .Index(t => t.MenuItemsCategoriesId);
            
            CreateTable(
                "Server.MenuItemsCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Server.OrdersDetails",
                c => new
                    {
                        OrdersId = c.Int(nullable: false),
                        MenuItemsId = c.Int(nullable: false),
                        Amount = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrdersId, t.MenuItemsId })
                .ForeignKey("Server.MenuItems", t => t.MenuItemsId, cascadeDelete: true)
                .ForeignKey("Server.Orders", t => t.OrdersId, cascadeDelete: true)
                .Index(t => t.OrdersId)
                .Index(t => t.MenuItemsId);
            
            CreateTable(
                "Server.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        TablesId = c.Int(nullable: false),
                        OrdersStatesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Server.OrdersStates", t => t.OrdersStatesId, cascadeDelete: true)
                .ForeignKey("Server.Tables", t => t.TablesId, cascadeDelete: true)
                .Index(t => t.TablesId)
                .Index(t => t.OrdersStatesId);
            
            CreateTable(
                "Server.OrdersStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Server.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Size = c.Short(nullable: false),
                        ReservationCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Server.ReservationsDetails",
                c => new
                    {
                        ReservationsId = c.Int(nullable: false),
                        TablesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationsId, t.TablesId })
                .ForeignKey("Server.Reservations", t => t.ReservationsId, cascadeDelete: true)
                .ForeignKey("Server.Tables", t => t.TablesId, cascadeDelete: true)
                .Index(t => t.ReservationsId)
                .Index(t => t.TablesId);
            
            CreateTable(
                "Server.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DateOn = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        Name = c.String(),
                        Phone = c.String(),
                        ReservationsStatesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Server.ReservationsStates", t => t.ReservationsStatesId, cascadeDelete: true)
                .Index(t => t.ReservationsStatesId);
            
            CreateTable(
                "Server.ReservationsStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Server.Recipes",
                c => new
                    {
                        MenuItemsId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.MenuItemsId)
                .ForeignKey("Server.MenuItems", t => t.MenuItemsId)
                .Index(t => t.MenuItemsId);
            
            CreateTable(
                "Server.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        FirstName = c.String(),
                        Name = c.String(),
                        City = c.String(),
                        AddressCode = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        Phone = c.String(),
                        UsersTypesId = c.Int(nullable: false),
                        Salary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Server.UsersTypes", t => t.UsersTypesId, cascadeDelete: true)
                .Index(t => t.UsersTypesId);
            
            CreateTable(
                "Server.UsersTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Server.Users", "UsersTypesId", "Server.UsersTypes");
            DropForeignKey("Server.Recipes", "MenuItemsId", "Server.MenuItems");
            DropForeignKey("Server.ReservationsDetails", "TablesId", "Server.Tables");
            DropForeignKey("Server.ReservationsDetails", "ReservationsId", "Server.Reservations");
            DropForeignKey("Server.Reservations", "ReservationsStatesId", "Server.ReservationsStates");
            DropForeignKey("Server.Orders", "TablesId", "Server.Tables");
            DropForeignKey("Server.Orders", "OrdersStatesId", "Server.OrdersStates");
            DropForeignKey("Server.OrdersDetails", "OrdersId", "Server.Orders");
            DropForeignKey("Server.OrdersDetails", "MenuItemsId", "Server.MenuItems");
            DropForeignKey("Server.MenuItemIngredients", "MenuItemsId", "Server.MenuItems");
            DropForeignKey("Server.MenuItems", "MenuItemsCategoriesId", "Server.MenuItemsCategories");
            DropForeignKey("Server.MenuItemIngredients", "IngredientsId", "Server.Ingredients");
            DropIndex("Server.Users", new[] { "UsersTypesId" });
            DropIndex("Server.Recipes", new[] { "MenuItemsId" });
            DropIndex("Server.Reservations", new[] { "ReservationsStatesId" });
            DropIndex("Server.ReservationsDetails", new[] { "TablesId" });
            DropIndex("Server.ReservationsDetails", new[] { "ReservationsId" });
            DropIndex("Server.Orders", new[] { "OrdersStatesId" });
            DropIndex("Server.Orders", new[] { "TablesId" });
            DropIndex("Server.OrdersDetails", new[] { "MenuItemsId" });
            DropIndex("Server.OrdersDetails", new[] { "OrdersId" });
            DropIndex("Server.MenuItems", new[] { "MenuItemsCategoriesId" });
            DropIndex("Server.MenuItemIngredients", new[] { "IngredientsId" });
            DropIndex("Server.MenuItemIngredients", new[] { "MenuItemsId" });
            DropTable("Server.UsersTypes");
            DropTable("Server.Users");
            DropTable("Server.Recipes");
            DropTable("Server.ReservationsStates");
            DropTable("Server.Reservations");
            DropTable("Server.ReservationsDetails");
            DropTable("Server.Tables");
            DropTable("Server.OrdersStates");
            DropTable("Server.Orders");
            DropTable("Server.OrdersDetails");
            DropTable("Server.MenuItemsCategories");
            DropTable("Server.MenuItems");
            DropTable("Server.MenuItemIngredients");
            DropTable("Server.Ingredients");
        }
    }
}
