using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using System.Windows;
using System.Windows.Input;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MyMessageBox : Window
    {
        TcpClient _client;
        public MyMessageBox(TcpClient client, int messageType)
        {
            InitializeComponent();
            _client = client;
            switch (messageType)
            {
                case 0:
                    TextBlockMessage.Text = "Na pewno chcesz kontynuować?";
                    ButtonYes.Visibility = Visibility.Visible;
                    ButtonCancel.Visibility = Visibility.Visible;
                    break;
                case 1:
                    TextBlockMessage.Text = "Operacja powiodła się.";
                    ButtonOk.Visibility = Visibility.Visible;
                    break;
                case 2:
                    TextBlockMessage.Text = "Uzupełnij wszystkie pola.";
                    ButtonOk.Visibility = Visibility.Visible;
                    break;
                case 3:
                    TextBlockMessage.Text = "Bład: Serwer nie odpowiada.";
                    ButtonOk.Visibility = Visibility.Visible;
                    break;
                case 4:
                    TextBlockMessage.Text = "Błąd: Operacja nie powiodła się.";
                    ButtonOk.Visibility = Visibility.Visible;
                    break;
                case 5:
                    TextBlockMessage.Text = "Bład: Wprowadzony login lub hasło jest nieprawidłowe.";
                    ButtonOk.Visibility = Visibility.Visible;
                    break;
                case 6:
                    TextBlockMessage.Text = "Bład: Podany login jest już zajęty.";
                    ButtonOk.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            _client.requestStatus = 1;
            Close();
        }
    }
}
