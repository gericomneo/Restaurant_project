using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Waiter
{
    /// <summary>
    /// Interaction logic for OrderNew.xaml
    /// </summary>
    public partial class OrderNew : UserControl
    {
        public List<CurrentOrder> CurrentOrderList = new List<CurrentOrder>();
        TcpClient _client;
        public OrderNew(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            TextBlockPrice.Text = "0,00";
            ComboBoxMenu.IsEnabled = false;
            TextBlockAmount.IsEnabled = false;
            _client.Refresh += new TcpClient.RefreshDelegate(Refresh);

            foreach (Tables table in StorageData.TablesList)
            {
                ComboBoxTables.Items.Add(table);
                ComboBoxTables.DisplayMemberPath = "Number";
            }

            foreach (MenuItems menuItem in StorageData.MenuItemsList.Where(m => m.Available == true))
            {
                ComboBoxMenu.Items.Add(menuItem);
                ComboBoxMenu.DisplayMemberPath = "Name";
            }
        }
        private void ComboBoxTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxMenu.IsEnabled = true;
        }

        private void ComboBoxMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TextBlockAmount.IsEnabled = true;

        }
        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ComboBoxTables.SelectedIndex >= 0  && int.Parse(TextBlockAmount.Text) > 0)
                {
                    bool alreadyAdded = false;
                    foreach (CurrentOrder element in CurrentOrderList)
                    {
                        if (((MenuItems)ComboBoxMenu.SelectedItem).Id == element.MenuItemsId)
                        {
                            alreadyAdded = true;
                            element.Amount += short.Parse(TextBlockAmount.Text);

                        }
                    }
                    if (!alreadyAdded)
                    {
                        ComboBoxTables.IsEnabled = false;
                        CurrentOrderList.Add(new CurrentOrder(((MenuItems)ComboBoxMenu.SelectedItem).Id, short.Parse(TextBlockAmount.Text)));
                        var price = ((MenuItems)ComboBoxMenu.SelectedItem).Price * int.Parse(TextBlockAmount.Text);
                        ListBoxDetails.Items.Add(((MenuItems)ComboBoxMenu.SelectedItem).Name + "\tx" + CurrentOrderList.ElementAt(CurrentOrderList.Count - 1).Amount.ToString() + "\t" + price.ToString() + "zł");
                        price += double.Parse(TextBlockPrice.Text);
                        TextBlockPrice.Text = price.ToString();
                    }
                    else
                    {
                        ListBoxDetails.Items.Clear();
                        TextBlockPrice.Text = "0,00";
                        foreach (CurrentOrder element in CurrentOrderList)
                        {
                            var elementPrice = ((MenuItems)ComboBoxMenu.SelectedItem).Price * element.Amount;
                            var price = double.Parse(TextBlockPrice.Text) + elementPrice;
                            TextBlockPrice.Text = price.ToString();
                            ListBoxDetails.Items.Add(((MenuItems)ComboBoxMenu.SelectedItem).Name + "\tx" + element.Amount.ToString() + "\t" + elementPrice.ToString() + "zł");
                        }
                    }
                }
            }
            catch (FormatException)
            {
                TextBlockAmount.Text = "0";
            }
        }

        private void ButtonRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var price = double.Parse(TextBlockPrice.Text) - (StorageData.MenuItemsList.Single(m => m.Id == CurrentOrderList.ElementAt(ListBoxDetails.SelectedIndex).MenuItemsId).Price * CurrentOrderList.ElementAt(ListBoxDetails.SelectedIndex).Amount);
            TextBlockPrice.Text = price.ToString();
            CurrentOrderList.RemoveAt(ListBoxDetails.SelectedIndex);
            ListBoxDetails.Items.Remove(ListBoxDetails.SelectedItem);
            if (CurrentOrderList.Count == 0)
            {
                ComboBoxTables.IsEnabled = true;
                ComboBoxTables.SelectedIndex = -1;
                ComboBoxMenu.IsEnabled = false;
                TextBlockAmount.IsEnabled = false;
            }
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = "0061<EOP>" + DateTime.Now.ToString() + "<EOP>" + ((Tables)ComboBoxTables.SelectedItem).Id.ToString() + "<EOP>";

                foreach (CurrentOrder orderDetails in CurrentOrderList)
                {
                    message += orderDetails.MenuItemsId.ToString() + "<EOP>" + orderDetails.Amount.ToString() + "<EOP>";
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
                    ParentGrid.Children.Add(new OrderNew(_client));
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

        private delegate void RefreshDelegate();
        private void Refresh()
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(new RefreshDelegate(Refresh));
                return;
            }
            ComboBoxMenu.Items.Clear();
            foreach (MenuItems menuItem in StorageData.MenuItemsList.Where(m => m.Available == true))
            {
                ComboBoxMenu.Items.Add(menuItem);
                ComboBoxMenu.DisplayMemberPath = "Name";
            }
            if (ComboBoxTables.SelectedItem == null)
            {
                ComboBoxTables.Items.Clear();
                foreach (Tables table in StorageData.TablesList)
                {
                    ComboBoxTables.Items.Add(table);
                    ComboBoxTables.DisplayMemberPath = "Number";
                }
            }
        }
    }
}
