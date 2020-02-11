using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml
{
    /// <summary>
    /// Interaction logic for MenuMenuItemsCategoriesId.xaml
    /// </summary>
    public partial class MenuMenuItemsCategoriesId : UserControl
    {
        TcpClient _client;
        int selectedMenuItemsCategoriesId;
        public MenuMenuItemsCategoriesId(TcpClient client, int category)
        {
            InitializeComponent();
            _client = client;
            selectedMenuItemsCategoriesId = category;
            foreach (MenuItems item in StorageData.MenuItemsList.Where(m => m.MenuItemsCategoriesId == selectedMenuItemsCategoriesId && m.Available == true))
            {
                Button button = new Button();
                button.Height = 470;
                button.Width = 650;
                button.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
                try
                {
                    byte[] imageBytess = Convert.FromBase64String(item.Image);
                    using (MemoryStream stream = new MemoryStream(imageBytess))
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        button.Background = brush;
                        button.Background.Opacity = 0.7;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                button.Foreground = new SolidColorBrush(Colors.White);
                button.FontFamily = new FontFamily("Gabriola");
                button.FontSize = 48;

                button.Content = new TextBlock
                {
                    Text = item.Name + Environment.NewLine + "Cena: " + item.Price + Environment.NewLine + " Opis: " + item.Description,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center

                };
                ListBoxMenu.Items.Add(button);
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new Menu(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
    }
}
