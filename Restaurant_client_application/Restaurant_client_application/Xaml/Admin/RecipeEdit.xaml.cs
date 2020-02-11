using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for RecipeEdit.xaml
    /// </summary>
    public partial class RecipeEdit : UserControl
    {
        Recipes selectedRecipe;
        TcpClient _client;
        public RecipeEdit(TcpClient client, int menuId)
        {
            InitializeComponent();
            _client = client;
            TextBlockName.Text = "Przepis na " + StorageData.MenuItemsList.AsEnumerable().Single(m => m.Id == menuId).Name;
            selectedRecipe = StorageData.RecipesList.AsEnumerable().Single(r => r.MenuItemsId == menuId);
            TextBoxRecipe.Text = selectedRecipe.Description;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new RecipesControl(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _client.requestStatus = 0;
                MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                myMessageBox.ShowDialog();

                if (_client.requestStatus == 1)
                {
                    _client.SendMessage("0052<EOP>" + selectedRecipe.MenuItemsId + "<EOP>" + TextBoxRecipe.Text + "<EOP>");
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
                    ParentGrid.Children.Add(new RecipesControl(_client));
                    ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
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
    }
}
