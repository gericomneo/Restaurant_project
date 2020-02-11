using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private static string selected = null;
        TcpClient _client;
        Label MainPage;
        public AdminWindow(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl-PL");
            Language = XmlLanguage.GetLanguage("pl-PL");
            MainPage = LabelMainPage;
            _client.updating = true;
            Update();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = ((ListViewItem)((ListView)sender).SelectedItem).Name;
        }
        private void ListViewMenu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            
            switch (selected)
            {
                case "Home":
                    GridMain.Children.Add(MainPage);
                    break;
                case "Accounts":
                    usc = new UsersControl(_client);
                    GridMain.Children.Add(usc);
                    break;
                case "Ingredients":
                    Update();
                    usc = new IngredientsControl(_client);
                    GridMain.Children.Add(usc);
                    break;
                case "Menu":
                    usc = new MenuControl(_client);
                    GridMain.Children.Add(usc);
                    break;
                case "Orders":
                    usc = new OrdersControl(_client);
                    GridMain.Children.Add(usc);
                    break;
                case "Reservations":
                    usc = new ReservationsControl(_client);
                    GridMain.Children.Add(usc);
                    break;
                case "Tables":
                    usc = new TablesControl(_client);
                    GridMain.Children.Add(usc);
                    break;
                case "Recipes":
                    usc = new RecipesControl(_client);
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            _client.SendMessage("0000<EOP>");
            MainWindow newWindow = new MainWindow();
            Close();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            _client.SendMessage("0000<EOP>");
            Close();
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private async void Update()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    string message = string.Empty;
                    _client.ReceiveMessage();
                }
            });

        }
        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

            if (WindowState == WindowState.Maximized)
            {
                Maximize.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowRestore;
            }
            else
            {
                Maximize.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowRestore;
                Maximize.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
            }
        }
    }
}