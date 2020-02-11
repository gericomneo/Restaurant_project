using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Cook
{
    /// <summary>
    /// Interaction logic for IngredientsAvailability.xaml
    /// </summary>
    public partial class IngredientsAvailability : UserControl
    {
        List<Ingredients> availabilityChangedIngredientsList = new List<Ingredients>();
        TcpClient _client;
        public IngredientsAvailability(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            foreach (Ingredients ingredients in StorageData.IngredientsList)
            {
                ListBoxIngredients.Items.Add(ingredients);
                ListBoxIngredients.DisplayMemberPath = "Name";
            }
            ButtonChangeAvailability.Visibility = Visibility.Hidden;
            _client.Refresh += new TcpClient.RefreshDelegate(Refresh);
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListBoxIngredients.Items.Clear();
            if (TextBoxSearch.Text != "")
            {
                foreach (Ingredients ingredients in StorageData.IngredientsList)
                {

                    if (ingredients.Name.Contains(TextBoxSearch.Text))
                    {
                        ListBoxIngredients.Items.Add(ingredients);
                        ListBoxIngredients.DisplayMemberPath = "Name";
                    }
                }
            }
            else
            {
                foreach (Ingredients ingredients in StorageData.IngredientsList)
                {
                    ListBoxIngredients.Items.Add(ingredients);
                    ListBoxIngredients.DisplayMemberPath = "Name";
                }
            }
        }

        private void ButtonChangeAvailability_Click(object sender, RoutedEventArgs e)
        {
            var selectedIngredient = (Ingredients)ListBoxIngredients.SelectedItem;
            if (selectedIngredient.Available) ButtonChangeAvailability.Content = "Zmień na niedostepne";
            else ButtonChangeAvailability.Content = "Zmień na dostępne";
            _client.requestStatus = 0;
            MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
            myMessageBox.ShowDialog();

            if (_client.requestStatus == 1)
            {
                _client.SendMessage("0023<EOP>" + selectedIngredient.Id + "<EOP>");
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

        private void ListBoxIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxIngredients.SelectedIndex >= 0)
            {
                ButtonChangeAvailability.Visibility = Visibility.Visible;
                if (((Ingredients)ListBoxIngredients.SelectedItem).Available) ButtonChangeAvailability.Content = "Zmień na niedostepne";
                else ButtonChangeAvailability.Content = "Zmień na dostępne";
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
            if (TextBoxSearch.Text != "")
            {
                foreach (Ingredients ingredients in StorageData.IngredientsList)
                {

                    if (ingredients.Name.Contains(TextBoxSearch.Text))
                    {
                        ListBoxIngredients.Items.Add(ingredients);
                        ListBoxIngredients.DisplayMemberPath = "Name";
                    }
                }
            }
            else
            {
                foreach (Ingredients ingredients in StorageData.IngredientsList)
                {
                    ListBoxIngredients.Items.Add(ingredients);
                    ListBoxIngredients.DisplayMemberPath = "Name";
                }
            }
            ButtonChangeAvailability.Visibility = Visibility.Hidden;
        }
    }
}
