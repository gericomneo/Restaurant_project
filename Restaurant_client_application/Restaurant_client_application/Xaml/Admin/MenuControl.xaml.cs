using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public static int selected;
        private static MenuItems selectedMenuItem;
        TcpClient _client;
        public MenuControl(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            Button buttonAdd = new Button();
            buttonAdd.Height = 200;
            buttonAdd.Width = 200;
            buttonAdd.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
            buttonAdd.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD304155"));
            buttonAdd. Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDD4BE75"));
            buttonAdd.FontFamily = new FontFamily("Gabriola");
            buttonAdd.FontSize = 26;
            buttonAdd.Content = "<Dodaj Nowy>";
            ListBoxMenu.Items.Add(buttonAdd);
            ButtonEdit.Visibility = Visibility.Hidden;
            ButtonRemove.Visibility = Visibility.Hidden;

            foreach (MenuItems item in StorageData.MenuItemsList)
            {
                try
                {
                    Button button = new Button();
                    button.Height = 200;
                    button.Width = 200;
                    button.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
                    try
                    {
                        byte[] imageBytess = Convert.FromBase64String(item.Image);
                        using (MemoryStream stream = new MemoryStream(imageBytess))
                        {
                            var brush = new ImageBrush();
                            brush.ImageSource = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                            button.Background = brush;
                            button.Background.Opacity = 0.8;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    button. Foreground = new SolidColorBrush(Colors.White);
                    button.FontFamily = new FontFamily("Gabriola");
                    button.FontSize = 26;

                    button.Content = new TextBlock
                    {
                        Text = item.Name,
                        TextWrapping = TextWrapping.Wrap,
                        TextAlignment = TextAlignment.Center

                    };
                    ListBoxMenu.Items.Add(button);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            foreach (Button selected in ListBoxMenu.Items)
            {
                selected.Click += Selected_Click;
            }
            _client.Refresh += new TcpClient.RefreshDelegate(Refresh);
        }

        private void Selected_Click(object sender, RoutedEventArgs e)
        {
            selected = 0;
            foreach (Button buttons in ListBoxMenu.Items)
            {
                if (buttons.Equals((sender as Button))) break;
                selected++;
            }
            if (selected > 0)
            {
                selectedMenuItem = StorageData.MenuItemsList.ElementAt(selected - 1);
                ButtonEdit.Visibility = Visibility.Visible;
                ButtonRemove.Visibility = Visibility.Visible;
            }
            else
            {
                Grid ParentGrid = (Grid)this.Parent;
                ParentGrid.Children.Add(new MenuNew(_client));
                ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuEdit(_client, selectedMenuItem));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _client.requestStatus = 0;
                MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                myMessageBox.ShowDialog();

                if (_client.requestStatus == 1)
                {
                    _client.SendMessage("0033<EOP>" + selectedMenuItem.Id.ToString() + "<EOP>");
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
            ListBoxMenu.SelectedItem = null;
            ListBoxMenu.Items.Clear();
            Button buttonAdd = new Button();
            buttonAdd.Height = 200;
            buttonAdd.Width = 200;
            buttonAdd.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
            buttonAdd.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD304155"));
            buttonAdd.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDD4BE75"));
            buttonAdd.FontFamily = new FontFamily("Gabriola");
            buttonAdd.FontSize = 26;
            buttonAdd.Content = "<Dodaj Nowy>";
            ListBoxMenu.Items.Add(buttonAdd);
            ButtonEdit.Visibility = Visibility.Hidden;
            ButtonRemove.Visibility = Visibility.Hidden;

            foreach (MenuItems item in StorageData.MenuItemsList)
            {
                try
                {
                    Button button = new Button();
                    button.Height = 200;
                    button.Width = 200;
                    button.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
                    try
                    {
                        byte[] imageBytess = Convert.FromBase64String(item.Image);
                        using (MemoryStream stream = new MemoryStream(imageBytess))
                        {
                            var brush = new ImageBrush();
                            brush.ImageSource = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                            button.Background = brush;
                            button.Background.Opacity = 0.8;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    button.Foreground = new SolidColorBrush(Colors.White);
                    button.FontFamily = new FontFamily("Gabriola");
                    button.FontSize = 26;

                    button.Content = new TextBlock
                    {
                        Text = item.Name,
                        TextWrapping = TextWrapping.Wrap,
                        TextAlignment = TextAlignment.Center

                    };
                    ListBoxMenu.Items.Add(button);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            foreach (Button selected in ListBoxMenu.Items)
            {
                selected.Click += Selected_Click;
            }
        }
    }
}
