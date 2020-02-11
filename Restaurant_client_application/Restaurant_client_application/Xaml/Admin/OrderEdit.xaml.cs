using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : UserControl
    {
        TcpClient _client;
        Orders selectedOrder;
        public OrderEdit(TcpClient client, Orders selected)
        {
            InitializeComponent();
            _client = client;
            selectedOrder = selected;
            foreach (OrdersStates orderStates in StorageData.OrdersStatesList)
            {
                ComboBoxStates.Items.Add(orderStates);
            }
            ComboBoxStates.DisplayMemberPath = "Name";

            foreach (OrdersDetails orderDetail in StorageData.OrdersDetailsList.Where(o => o.OrdersId == selectedOrder.Id))
            {
                var menuItem = StorageData.MenuItemsList.Single(m => m.Id == orderDetail.MenuItemsId);
                ListBoxDetails.Items.Add(menuItem.Name + " x" + orderDetail.Amount);
            }
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBoxStates.SelectedIndex + 1 > -1)
                {
                    _client.requestStatus = 0;
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                    myMessageBox.ShowDialog();

                    if (_client.requestStatus == 1)
                    {
                        _client.SendMessage("0063<EOP>" + selectedOrder.Id.ToString() + "<EOP>" + (ComboBoxStates.SelectedIndex + 1).ToString() + "<EOP>");
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
                        ParentGrid.Children.Add(new OrdersControl(_client));
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
            ParentGrid.Children.Add(new OrdersControl(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
    }
}
