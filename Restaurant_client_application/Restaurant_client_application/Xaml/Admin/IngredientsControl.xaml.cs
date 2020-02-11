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
    /// Interaction logic for IngredientsControl.xaml
    /// </summary>
    public partial class IngredientsControl : UserControl
    {
        public static int selected;
        private static Ingredients selectedIngredient;
        bool notUsed;
        TcpClient _client;

        public IngredientsControl(TcpClient client)
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
            ListBoxIngredients.Items.Add(buttonAdd);
            ButtonEdit.Visibility = Visibility.Hidden;
            ButtonRemove.Visibility = Visibility.Hidden;

            
            foreach (Ingredients item in StorageData.IngredientsList)
            {
                Button button = new Button();
                button.Height = 150;
                button.Width = 150;
                button.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
                button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD304155"));
                button. Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDD4BE75"));
                button.FontFamily = new FontFamily("Gabriola");
                button.FontSize = 26;

                button.Content = new TextBlock
                {
                    Text = item.Name,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                };
                ListBoxIngredients.Items.Add(button);
            }
            foreach (Button selected in ListBoxIngredients.Items)
            {
                selected.Click += Selected_Click;
            }
            _client.Refresh += new TcpClient.RefreshDelegate(Refresh);
        }

        private void Selected_Click(object sender, RoutedEventArgs e)
        {
            selected = 0;
            foreach (Button buttons in ListBoxIngredients.Items)
            {
                if (buttons.Equals((sender as Button))) break;
                selected++;
            }
            if (selected > 0)
            {
                selectedIngredient = StorageData.IngredientsList.ElementAt(selected - 1);
                ButtonEdit.Visibility = Visibility.Visible;
                ButtonRemove.Visibility = Visibility.Visible;

                if (StorageData.MenuItemsIngredientsList.Any(item => item.IngredientsId.Equals(selected)))
                {
                    notUsed = false;
                    ButtonRemove.Content = "Usunąć można tylko nieużywany składnik";
                    ButtonRemove.Width = 400;
                }
                else
                {
                    notUsed = true;
                    ButtonRemove.Content = "Usuń";
                    ButtonRemove.Width = 270;
                }
            }
            else
            {
                Grid ParentGrid = (Grid)this.Parent;
                ParentGrid.Children.Add(new IngredientNew(_client));
                ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new IngredientEdit(_client, selectedIngredient));
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
                        _client.SendMessage("0024<EOP>" + selectedIngredient.Id.ToString() + "<EOP>");
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
            ListBoxIngredients.SelectedItem = null;
            ListBoxIngredients.Items.Clear();

            Button buttonAdd = new Button();
            buttonAdd.Height = 150;
            buttonAdd.Width = 150;
            buttonAdd.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
            buttonAdd.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD304155"));
            buttonAdd.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDD4BE75"));
            buttonAdd.FontFamily = new FontFamily("Gabriola");
            buttonAdd.FontSize = 26;
            buttonAdd.Content = "<Dodaj Nowy>";
            ListBoxIngredients.Items.Add(buttonAdd);
            ButtonEdit.Visibility = Visibility.Hidden;
            ButtonRemove.Visibility = Visibility.Hidden;
            foreach (Ingredients item in StorageData.IngredientsList)
            {
                Button button = new Button();
                button.Height = 150;
                button.Width = 150;
                button.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD2568AC"));
                button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DD304155"));
                button.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DDD4BE75"));
                button.FontFamily = new FontFamily("Gabriola");
                button.FontSize = 26;

                button.Content = new TextBlock
                {
                    Text = item.Name,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                };
                ListBoxIngredients.Items.Add(button);
            }
            foreach (Button selected in ListBoxIngredients.Items)
            {
                selected.Click += Selected_Click;
            }
        }
    }
}
