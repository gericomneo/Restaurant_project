using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for TablesControl.xaml
    /// </summary>
    public partial class TablesControl : UserControl
    {
        public static int selected;
        bool notUsed = true;
        TcpClient _client;
        public TablesControl(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            Button buttonAdd = new Button();
            buttonAdd.Height = 150;
            buttonAdd.Width = 150;
            buttonAdd.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
            buttonAdd.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD304155"));
            buttonAdd. Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDD4BE75"));
            buttonAdd.FontFamily = new FontFamily("Gabriola");
            buttonAdd.FontSize = 26;
            buttonAdd.Content = "<Dodaj Nowy>";
            ListBoxTables.Items.Add(buttonAdd);
            ButtonEdit.Visibility = Visibility.Hidden;
            ButtonRemove.Visibility = Visibility.Hidden;
            foreach (Tables table in StorageData.TablesList)
            {
                Button button = new Button();
                button.Height = 150;
                button.Width = 150;
                button.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
                button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD304155"));
                button. Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDD4BE75"));
                button.FontFamily = new FontFamily("Gabriola");
                button.FontSize = 26;

                button.Content = "Stół nr:  " + table.Number;
                ListBoxTables.Items.Add(button);
            }

            foreach(Button selected in ListBoxTables.Items)
            {
                selected.Click += Selected_Click;
            }
            _client.Refresh += new TcpClient.RefreshDelegate(Refresh);
        }

        private void Selected_Click(object sender, RoutedEventArgs e)
        {
            selected = 0;
            foreach (Button buttons in ListBoxTables.Items)
            {
                if (buttons.Equals((sender as Button))) break;
                selected++;
            }
            if (selected > 0)
            {
                ButtonEdit.Visibility = Visibility.Visible;
                ButtonRemove.Visibility = Visibility.Visible;
                if (selected < StorageData.TablesList.Count - 1)
                {
                    notUsed = false;
                    ButtonRemove.Content = "Usunąć można tylko ostatni stolik";
                    ButtonRemove.Width = 400;
                }
                else
                {
                    if (StorageData.OrdersDetailsList.Any(o => o.OrdersId == StorageData.TablesList.ElementAt(selected - 1).Id))
                    {
                        notUsed = false;
                        ButtonRemove.Content = "Nie można usunąć stolika z zamówieniem";
                        ButtonRemove.Width = 400;
                    }
                    if (StorageData.ReservationsDetailsList.Any(r => r.TablesId == StorageData.TablesList.ElementAt(selected - 1).Id))
                    {
                        notUsed = false;
                        ButtonRemove.Content = "Nie można usunąć stolika z rezerwacją";
                        ButtonRemove.Width = 400;
                    }
                }
            }
            else
            {
                Grid ParentGrid = (Grid)this.Parent;
                ParentGrid.Children.Add(new TableNew(_client));
                ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
            }
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new TableEdit(_client, selected - 1));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (notUsed)
                {
                    _client.requestStatus = 0;
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                    myMessageBox.ShowDialog();

                    if (_client.requestStatus == 1)
                    {
                        _client.SendMessage("0043<EOP>" + StorageData.TablesList.ElementAt(selected - 1).Id + "<EOP>");
                        _client.requestStatus = 0;
                        int time = DateTime.Now.Second;
                        while (_client.requestStatus == 0)
                        {
                            if (time - DateTime.Now.Second > 60)
                            {
                                myMessageBox = new MyMessageBox(_client, 3);
                                myMessageBox.ShowDialog();
                                return;
                            }
                        }
                        myMessageBox = new MyMessageBox(_client, _client.requestStatus);
                        myMessageBox.ShowDialog();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private delegate void RefreshDelegate();
        private void Refresh()
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(new RefreshDelegate(Refresh));
                return;
            }

            ListBoxTables.SelectedItem = null;
            ListBoxTables.Items.Clear();
            Button buttonAdd = new Button();
            buttonAdd.Height = 150;
            buttonAdd.Width = 150;
            buttonAdd.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
            buttonAdd.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD304155"));
            buttonAdd.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDD4BE75"));
            buttonAdd.FontFamily = new FontFamily("Gabriola");
            buttonAdd.FontSize = 26;
            buttonAdd.Content = "<Dodaj Nowy>";
            ListBoxTables.Items.Add(buttonAdd);
            ButtonEdit.Visibility = Visibility.Hidden;
            ButtonRemove.Visibility = Visibility.Hidden;
            foreach (Tables table in StorageData.TablesList)
            {
                Button button = new Button();
                button.Height = 150;
                button.Width = 150;
                button.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
                button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD304155"));
                button.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDD4BE75"));
                button.FontFamily = new FontFamily("Gabriola");
                button.FontSize = 26;

                button.Content = "Stół nr:  " + table.Number;
                ListBoxTables.Items.Add(button);
            }

            foreach (Button selected in ListBoxTables.Items)
            {
                selected.Click += Selected_Click;
            }

        }
    }
}
