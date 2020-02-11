using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using Microsoft.Win32;
using System;
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
    public partial class MenuNew : UserControl
    {
        string ImageString;
        TcpClient _client;
        public MenuNew(TcpClient client)
        {
            InitializeComponent();
            _client = client;

            ListBoxIngredients.ItemsSource = StorageData.IngredientsList;
            ListBoxIngredients.DisplayMemberPath = "Name";
            ComboBoxMenuItemsCategoriesId.ItemsSource = StorageData.MenuItemsCategoriesList;
            ComboBoxMenuItemsCategoriesId.DisplayMemberPath = "Name";
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBoxMenuItemsCategoriesId.SelectedIndex > -1 && TextBoxName.Text != "" && TextBoxPrice.Text != "")
                {
                    string message = "0031<EOP>" + TextBoxName.Text + "<EOP>" + TextBoxPrice.Text + "<EOP>"
                          + StorageData.MenuItemsCategoriesList.ElementAt(ComboBoxMenuItemsCategoriesId.SelectedIndex).Id.ToString()
                         + "<EOP>" + TextBoxDescription.Text + "<EOP>" + ImageString + "<EOP>";


                    foreach (Ingredients ingredients in ListBoxIngredients.SelectedItems)
                    {
                        message += ingredients.Id + "<EOP>";
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
                else
                {
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 2);
                    myMessageBox.ShowDialog();
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
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Wybierz zdjęcie";
                op.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    ImageMenuItem.Source = new BitmapImage(new Uri(op.FileName));

                    byte[] imageBytes = File.ReadAllBytes(op.FileName);
                    ImageString = Convert.ToBase64String(imageBytes);
                    MessageBox.Show(ImageString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
