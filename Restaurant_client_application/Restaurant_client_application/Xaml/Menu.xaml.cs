using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        TcpClient _client;
        public Menu(TcpClient client)
        {
            InitializeComponent();
            _client = client;
        }
        private void ButtonBreakfast_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuMenuItemsCategoriesId(_client, 1));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
        private void ButtonDinner_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuMenuItemsCategoriesId(_client, 2));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
        private void ButtonSupper_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuMenuItemsCategoriesId(_client, 3));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
        private void ButtonSalad_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuMenuItemsCategoriesId(_client, 4));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
        private void ButtonSoftDrink_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuMenuItemsCategoriesId(_client, 5));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
        private void ButtonHotDrink_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuMenuItemsCategoriesId(_client, 6));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
        private void ButtonDessert_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuMenuItemsCategoriesId(_client, 7));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
        private void ButtonAlcohol_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuMenuItemsCategoriesId(_client, 8));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
    }
}
