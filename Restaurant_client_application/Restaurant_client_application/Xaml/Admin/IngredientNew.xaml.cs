using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for IngredientNew.xaml
    /// </summary>
    public partial class IngredientNew : UserControl
    {
        TcpClient _client;
        public IngredientNew(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            ComboBoxAvailable.Items.Add("Tak");
            ComboBoxAvailable.Items.Add("Nie");
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new IngredientsControl(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextBoxName.Text != "" && ComboBoxAvailable.SelectedIndex > -1)
                {

                    _client.requestStatus = 0;
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                    myMessageBox.ShowDialog();

                    if (_client.requestStatus == 1)
                    {
                        _client.SendMessage("0021<EOP>" + TextBoxName.Text + "<EOP>" + ComboBoxAvailable.SelectedIndex.ToString() + "<EOP>");
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
                        ParentGrid.Children.Add(new IngredientsControl(_client));
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
    }
}
