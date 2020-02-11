using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient _client;
        public static string MenuItemList;
        bool connected = false;

        public MainWindow()
        {
            InitializeComponent();
            StorageData.IngredientsList.Clear();
            StorageData.MenuItemsCategoriesList.Clear();
            StorageData.MenuItemsIngredientsList.Clear();
            StorageData.MenuItemsList.Clear();
            StorageData.OrdersDetailsList.Clear();
            StorageData.OrdersStatesList.Clear();
            StorageData.OrdersList.Clear();
            StorageData.RecipesList.Clear();
            StorageData.ReservationsDetailsList.Clear();
            StorageData.ReservationsList.Clear();
            StorageData.TablesList.Clear();
            StorageData.UsersList.Clear();
            StorageData.UsersTypesIdList.Clear();
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            ButtonLogin.IsEnabled = false;
            if (TextBlockLogin.Text != "" && PasswordBoxPassword.Password != "")
            {

                _client = new TcpClient();

                connected = _client.Connect();

                Login(TextBoxLogin.Text, PasswordBoxPassword.Password);
                _client.requestStatus = 0;
            }
            else
            {
                MyMessageBox myMessageBox = new MyMessageBox(_client, 2);
                myMessageBox.ShowDialog();
                ButtonLogin.IsEnabled = true;
            }
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (connected) _client.SendMessage("0000<EOP>");
            Close();
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private async void Login(string login, string password)
        {
            _client.SendMessage("0008<EOP>" + login + "<EOP>" + PasswordHash.GenerateSHA512String(password) + "<EOP>");
            string type = "0";
            bool succeed = true;
            if (connected)
            {
                await Task.Run(() =>
                {

                    type = _client.ReceiveMessage();
                    switch (type)
                    {
                        case "1":
                            _client.SendMessage("0010<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0014<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0020<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0030<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0034<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0037<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0040<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0050<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0060<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0062<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0066<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0070<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0074<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0075<EOP>");
                            _client.ReceiveMessage();
                            break;
                        case "2":
                            _client.SendMessage("0020<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0030<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0034<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0037<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0050<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0060<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0062<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0066<EOP>");
                            _client.ReceiveMessage();
                            break;
                        case "3":
                            _client.SendMessage("0030<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0034<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0037<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0040<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0070<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0074<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0075<EOP>");
                            _client.ReceiveMessage();
                            break;
                        case "4":
                            _client.SendMessage("0030<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0034<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0037<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0040<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0060<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0062<EOP>");
                            _client.ReceiveMessage();
                            _client.SendMessage("0066<EOP>");
                            _client.ReceiveMessage();
                            break;
                        default:
                            break;
                    }
                });
            }
            else
            {
                StorageData.IngredientsList.Clear();
                StorageData.MenuItemsCategoriesList.Clear();
                StorageData.MenuItemsIngredientsList.Clear();
                StorageData.MenuItemsList.Clear();
                StorageData.OrdersDetailsList.Clear();
                StorageData.OrdersStatesList.Clear();
                StorageData.OrdersList.Clear();
                StorageData.RecipesList.Clear();
                StorageData.ReservationsDetailsList.Clear();
                StorageData.ReservationsList.Clear();
                StorageData.TablesList.Clear();
                StorageData.UsersList.Clear();
                StorageData.UsersTypesIdList.Clear();
                MyMessageBox myMessageBox = new MyMessageBox(_client, 3);
                myMessageBox.ShowDialog();
                ButtonLogin.IsEnabled = true;
                return;
            }

            if (_client.requestStatus == 3)
            {
                MyMessageBox myMessageBox = new MyMessageBox(_client, 3);
                myMessageBox.ShowDialog();
                ButtonLogin.IsEnabled = true;
                return;
            }
            switch (type)
            {
                case "1":
                    new Admin.AdminWindow(_client);
                    Close();
                    break;
                case "2":
                    new Cook.CookWindow(_client);
                    Close();
                    break;
                case "3":
                    new Receptionist.ReceptionistWindow(_client);
                    Close();
                    break;
                case "4":
                    new Waiter.WaiterWindow(_client);
                    Close();
                    break;
                default:
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 5);
                    myMessageBox.ShowDialog();
                    ButtonLogin.IsEnabled = true;
                    break;
            }
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
