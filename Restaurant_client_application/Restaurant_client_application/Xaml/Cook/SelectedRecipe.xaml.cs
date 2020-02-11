using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Cook
{
    /// <summary>
    /// Interaction logic for SelectedRecipe.xaml
    /// </summary>
    public partial class SelectedRecipe : UserControl
    {
        TcpClient _client;
        int selectedMenuItemsId;
        public SelectedRecipe(TcpClient client, int menuId)
        {
            InitializeComponent();
            _client = client;
            selectedMenuItemsId = menuId;
            TextBlockName.Text = "Przepis na " + StorageData.MenuItemsList.AsEnumerable().Single(m => m.Id == selectedMenuItemsId).Name;
            var recipe = StorageData.RecipesList.Single(r => r.MenuItemsId == selectedMenuItemsId);
            ListBoxMenuItem.Items.Add(recipe);
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new RecipesControl(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
    }
}
