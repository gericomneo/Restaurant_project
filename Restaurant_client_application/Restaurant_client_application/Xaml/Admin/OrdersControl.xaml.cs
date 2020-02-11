using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for OrdersControl.xaml
    /// </summary>
    public partial class OrdersControl : UserControl
    {
        private List<Orders> orderLists = new List<Orders>();
        private static Orders selectedOrder;
        TcpClient _client;
        public OrdersControl(TcpClient client)
        {
            InitializeComponent();
            _client = client;

            ComboBoxStates.Items.Add(new OrdersStates(0, "Wszystkie"));
            foreach (OrdersStates orderState in StorageData.OrdersStatesList)
            {
                ComboBoxStates.Items.Add(orderState);
            }

            foreach (Orders order in StorageData.OrdersList)
            {
                ListBoxOrders.Items.Add("Id: " + order.Id.ToString() + "\tData: " + order.Date.ToString() + "\tStatus: " + StorageData.OrdersStatesList.Single(o => o.Id == order.OrderStateId).Name);
            }

            ComboBoxStates.DisplayMemberPath = "Name";
            ComboBoxStates.SelectedIndex = 0;
            ButtonRemove.Visibility = Visibility.Hidden;
            ButtonEdit.Visibility = Visibility.Hidden;
            _client.Refresh += new TcpClient.RefreshDelegate(Refresh);
        }

        private void ComboBoxStates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxOrders.Items.Clear();
            if (ComboBoxStates.SelectedIndex == 0)
            {
                foreach (Orders order in StorageData.OrdersList)
                {
                    ListBoxOrders.Items.Add("Id: " + order.Id.ToString() + "\tData: " + order.Date.ToString() + "\tStatus: " + StorageData.OrdersStatesList.Single(o => o.Id == order.OrderStateId).Name);
                }
            }
            else
            {
                foreach (Orders order in StorageData.OrdersList.Where(o => o.OrderStateId == ComboBoxStates.SelectedIndex))
                {
                    ListBoxOrders.Items.Add("Id: " + order.Id.ToString() + "\tData: " + order.Date.ToString() + "\tStatus: " + StorageData.OrdersStatesList.Single(o => o.Id == order.OrderStateId).Name);
                }
            }

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
                    _client.SendMessage("0065<EOP>" + selectedOrder.Id.ToString() + "<EOP>");
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

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid ParentGrid = (Grid)this.Parent;
                ParentGrid.Children.Add(new OrderEdit(_client, selectedOrder));
                ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
            }
            catch (Exception)
            {

            }
        }
        private void ListBoxOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxOrders.SelectedIndex > -1)
            {
                selectedOrder = StorageData.OrdersList.ElementAt(ListBoxOrders.SelectedIndex);
                ButtonRemove.Visibility = Visibility.Visible;
                ButtonEdit.Visibility = Visibility.Visible;
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
            ListBoxOrders.SelectedItem = null;
            ListBoxOrders.Items.Clear();

            foreach (Orders order in StorageData.OrdersList)
            {
                ListBoxOrders.Items.Add("Id: " + order.Id.ToString() + "\tData: " + order.Date.ToString() + "\tStatus: " + StorageData.OrdersStatesList.Single(o => o.Id == order.OrderStateId).Name);
            }

            ComboBoxStates.DisplayMemberPath = "Name";
            ComboBoxStates.SelectedIndex = 0;
            ButtonRemove.Visibility = Visibility.Hidden;
            ButtonEdit.Visibility = Visibility.Hidden;
        }
    }
}
