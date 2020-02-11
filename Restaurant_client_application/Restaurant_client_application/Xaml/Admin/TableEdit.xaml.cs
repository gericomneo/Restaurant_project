using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for TableEdit.xaml
    /// </summary>
    public partial class TableEdit : UserControl
    {
        private int number;
        private Tables selectedTable;
        TcpClient _client;
        public TableEdit(TcpClient client, int selected)
        {
            InitializeComponent();
            _client = client;
            selectedTable = StorageData.TablesList.ElementAt(selected);
            TextBoxCost.Text = selectedTable.ReservationCost.ToString();
            TextBoxSize.Text = selectedTable.Size.ToString();
            number = selectedTable.Number;
            TextBoxNumber.Text = selectedTable.Number.ToString();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new TablesControl(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextBoxSize.Text != "" && TextBoxCost.Text != "")
                {
                    _client.requestStatus = 0;
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                    myMessageBox.ShowDialog();

                    if (_client.requestStatus == 1)
                    {
                        _client.SendMessage("0042<EOP>" + selectedTable.Id + "<EOP>" + TextBoxNumber.Text + "<EOP>" + TextBoxSize.Text + "<EOP>" + TextBoxCost.Text + "<EOP>");
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
                        ParentGrid.Children.Add(new TablesControl(_client));
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
    }
}
