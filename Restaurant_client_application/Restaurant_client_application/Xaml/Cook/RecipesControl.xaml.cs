using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System.Linq;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Cook
{
    /// <summary>
    /// Interaction logic for RecipesControl.xaml
    /// </summary>
    public partial class RecipesControl : UserControl
    {
        public static int selectedMenuItemsId;
        TcpClient _client;
        public RecipesControl(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            foreach (MenuItems menuItem in StorageData.MenuItemsList)
            {
                ListBoxMenuItems.Items.Add(menuItem);
                ListBoxMenuItems.DisplayMemberPath = "Name";

            }
            _client.Refresh += new TcpClient.RefreshDelegate(Refresh);
        }

        private void ListBoxMenuItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new SelectedRecipe(_client, StorageData.MenuItemsList.ElementAt(ListBoxMenuItems.SelectedIndex).Id));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }

        private delegate void RefreshDelegate();
        private void Refresh()
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(new RefreshDelegate(Refresh));
                return;
            }
            ListBoxMenuItems.Items.Clear();
            foreach (MenuItems menuItem in StorageData.MenuItemsList)
            {
                ListBoxMenuItems.Items.Add(menuItem);
                ListBoxMenuItems.DisplayMemberPath = "Name";
            }
        }
    }
}
