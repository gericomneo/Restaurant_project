using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Waiter
{
    /// <summary>
    /// Interaction logic for OrdersControl.xaml
    /// </summary>
    public partial class OrdersControl : UserControl
    {
        TcpClient _client;
        Orders selectedOrder;
        public OrdersControl(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            foreach (Orders order in StorageData.OrdersList.Where(o => o.OrderStateId == 2))
            {
                ListBoxOrders.Items.Add(order);
            }

            _client.Refresh += new TcpClient.RefreshDelegate(Refresh);
        }
        private void ChangeOrderState_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListBoxOrders.SelectedItem != null)
                {
                    _client.requestStatus = 0;
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                    myMessageBox.ShowDialog();

                    if (_client.requestStatus == 1)
                    {
                        _client.SendMessage("0064<EOP>" + selectedOrder.Id + "<EOP>");
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
                        ListBoxOrders.SelectedItem = null;
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

        private void ListBoxOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedOrder = (Orders)ListBoxOrders.SelectedItem;
        }

        private delegate void RefreshDelegate();
        private void Refresh()
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(new RefreshDelegate(Refresh));
                return;
            }
            ListBoxOrders.Items.Clear();
            foreach (Orders order in StorageData.OrdersList.Where(o => o.OrderStateId == 2))
            {
                ListBoxOrders.Items.Add(order);
            }
        }
    }
}
