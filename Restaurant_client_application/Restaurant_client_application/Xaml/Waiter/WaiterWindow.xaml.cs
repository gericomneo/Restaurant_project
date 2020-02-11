using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Waiter
{
    /// <summary>
    /// Interaction logic for WaiterWindow.xaml
    /// </summary>
    public partial class WaiterWindow : Window
    {
        public static int selectedItemType;
        private static string selected = null;
        TcpClient _client;
        Label MainPage;
        public WaiterWindow(TcpClient client)
        {
            InitializeComponent();
            _client = client;
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
                case "Menu":
                    usc = new Menu(_client);
                    GridMain.Children.Add(usc);
                    break;
                case "OrderNew":
                    usc = new OrderNew(_client);
                    GridMain.Children.Add(usc);
                    break;
                case "Orders":
                   usc = new OrdersControl(_client);
                   GridMain.Children.Add(usc);
                    break;
                case "BillClose":
                    usc = new BillClose(_client);
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
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