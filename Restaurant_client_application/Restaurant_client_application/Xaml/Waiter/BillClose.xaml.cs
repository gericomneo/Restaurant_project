using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Waiter
{
    /// <summary>
    /// Interaction logic for BillClose.xaml
    /// </summary>
    public partial class BillClose : UserControl
    {
        TcpClient _client;
        public BillClose(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            foreach (Tables table in StorageData.TablesList)
            {
                ComboBoxTables.Items.Add(table);
                ComboBoxTables.DisplayMemberPath = "Number";
                TextBlockPrice.Text = "0";
            }
            _client.Refresh += new TcpClient.RefreshDelegate(Refresh);
        }

        private void ComboBoxTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTables.SelectedIndex > -1)
            {
                ListBoxBillDetails.Items.Clear();
                if (!StorageData.OrdersList.Exists(o => o.TablesId == ((Tables)ComboBoxTables.SelectedItem).Id && o.OrderStateId <= 3))
                {
                    ListBoxBillDetails.Items.Add("Brak zamówień na ten rachunek");
                }
                else if (StorageData.OrdersList.Exists(o => o.OrderStateId < 3 && o.TablesId == ((Tables)ComboBoxTables.SelectedItem).Id))
                {
                    ListBoxBillDetails.Items.Add("Zamówienia na ten rachunek są jeszcze w trakcie realizacji.");
                }
                else
                {
                    foreach (OrdersDetails orderDetails in StorageData.OrdersDetailsList.Where(odl => StorageData.OrdersList.Exists(o => o.OrderStateId == 3 && o.TablesId == ((Tables)ComboBoxTables.SelectedItem).Id && o.Id == odl.OrdersId)))
                    {
                        var price = (StorageData.MenuItemsList.Single(m => m.Id == orderDetails.MenuItemsId).Price * orderDetails.Amount);
                        ListBoxBillDetails.Items.Add(StorageData.MenuItemsList.Single(m => m.Id == orderDetails.MenuItemsId).Name + "\tx" + orderDetails.Amount.ToString() + "\t"
                            + price.ToString() + "zł");
                        TextBlockPrice.Text = (double.Parse(TextBlockPrice.Text) + price).ToString();
                    }
                }
            }
        }

        private void ButtonBillClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!StorageData.OrdersList.Exists(o => o.TablesId == ((Tables)ComboBoxTables.SelectedItem).Id && o.OrderStateId < 3))
                {
                    string message = "0067<EOP>";
                    foreach (Orders order in StorageData.OrdersList.Where(o => o.TablesId == ((Tables)ComboBoxTables.SelectedItem).Id && o.OrderStateId == 3))
                    {
                        message += order.Id + "<EOP>";
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
                    }
                    else
                    {
                        return;
                    }

                    ListBoxBillDetails.Items.Clear();
                    ComboBoxTables.SelectedIndex = -1;
                    TextBlockPrice.Text = "0,00";
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
            var selected = (Tables)ComboBoxTables.SelectedItem;
            ComboBoxTables.Items.Clear();
            foreach (Tables table in StorageData.TablesList)
            {
                ComboBoxTables.Items.Add(table);
                ComboBoxTables.DisplayMemberPath = "Number";
                TextBlockPrice.Text = "0";
            }
            ComboBoxTables.SelectedItem = selected;
        }
    }
}
