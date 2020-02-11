using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for MenuEdit.xaml
    /// </summary>
    public partial class MenuEdit : UserControl
    {
        MenuItems selectedMenuItem;
        List<Ingredients> oldMenuItemsIngredientsList = new List<Ingredients>();
        string ImageString;
        TcpClient _client;
        public MenuEdit(TcpClient client, MenuItems selected)
        {
            InitializeComponent();
            _client = client;
            selectedMenuItem = selected;
            foreach (Ingredients ingredient in StorageData.IngredientsList)
            {
                ListBoxIngredients.Items.Add(ingredient);
                if (StorageData.MenuItemsIngredientsList.Exists(already => (already.MenuItemsId == selectedMenuItem.Id) && already.IngredientsId == ingredient.Id))
                {
                    ListBoxIngredients.SelectedItems.Add(ingredient);
                    oldMenuItemsIngredientsList.Add(ingredient);
                }
            }
            ComboBoxMenuItemsCategoriesId.ItemsSource = StorageData.MenuItemsCategoriesList;
            ComboBoxMenuItemsCategoriesId.DisplayMemberPath = "Name";
            ListBoxIngredients.DisplayMemberPath = "Name";
            try
            {
                byte[] imageBytess = Convert.FromBase64String(selectedMenuItem.Image);

                using (MemoryStream stream = new MemoryStream(imageBytess))
                {
                    ImageMenuItem.Source = BitmapFrame.Create(stream,BitmapCreateOptions.None,BitmapCacheOption.OnLoad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            ComboBoxMenuItemsCategoriesId.SelectedIndex = selectedMenuItem.MenuItemsCategoriesId - 1;
            TextBoxName.Text = selectedMenuItem.Name;
            TextBoxDescription.Text = selectedMenuItem.Description;
            TextBoxPrice.Text = selectedMenuItem.Price.ToString();
            ImageString = selectedMenuItem.Image;
        }


    private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBoxMenuItemsCategoriesId.SelectedIndex > -1 && TextBoxDescription.Text != "" && ImageString != "" && TextBoxName.Text != "" && TextBoxPrice.Text != "")
                {
                    string message = "0032<EOP>" + selectedMenuItem.Id.ToString() + "<EOP>" + TextBoxName.Text + "<EOP>" + TextBoxPrice.Text.ToString() + "<EOP>"
                        + StorageData.MenuItemsCategoriesList.ElementAt(ComboBoxMenuItemsCategoriesId.SelectedIndex).Id.ToString()
                        + "<EOP>" + TextBoxDescription.Text + "<EOP>" + ImageString + "<EOP>";


                    var selectedMenuItemsIngredientsList = new List<Ingredients>();
                    foreach (Ingredients ingredient in ListBoxIngredients.SelectedItems)
                    {
                        selectedMenuItemsIngredientsList.Add(ingredient);
                    }

                    if (oldMenuItemsIngredientsList.Except(selectedMenuItemsIngredientsList).Count() > 0)
                    {
                        message += "0036<EOP>";
                        foreach (Ingredients ingredient in oldMenuItemsIngredientsList.Except(selectedMenuItemsIngredientsList))
                        {
                            message += ingredient.Id + "<EOP>";
                        }
                    }
                    if (selectedMenuItemsIngredientsList.Except(oldMenuItemsIngredientsList).Count() > 0)
                    {
                        message += "0035<EOP>";
                        foreach (Ingredients ingredient in selectedMenuItemsIngredientsList.Except(oldMenuItemsIngredientsList))
                        {
                            message += ingredient.Id + "<EOP>";
                        }

                    }
                    _client.requestStatus = 0;
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                    myMessageBox.ShowDialog();

                    if (_client.requestStatus == 1)
                    {
                        _client.SendMessage(message);
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

                        Grid ParentGrid = (Grid)this.Parent;
                        ParentGrid.Children.Add(new MenuControl(_client));
                        ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
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

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new MenuControl(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }

        private void ButtonAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Wybierz zdjęcie";
            op.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ImageMenuItem.Source = new BitmapImage(new Uri(op.FileName));

                byte[] imageBytes = File.ReadAllBytes(op.FileName);
                ImageString = Convert.ToBase64String(imageBytes);
            }
        }
    }
}
