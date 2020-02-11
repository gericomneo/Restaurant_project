using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Receptionist
{
    /// <summary>
    /// Interaction logic for ReservationNew.xaml
    /// </summary>
    public partial class ReservationNew : UserControl
    {
        public List<CurrentReservation> reservationDetailsList = new List<CurrentReservation>();
        int peopleCount = 0;
        double cost = 0;
        TcpClient _client;
        public ReservationNew(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            TextBlockPeopleCount.IsEnabled = false;
            DateTimePickerDate.Minimum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }
        private void DateTimePickerDate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ListBoxDetails.Items.Clear();
            TextBlockPeopleCount.Text = "";
            TextBlockPeopleCount.IsEnabled = true;
            TextBlockCost.Text = "0,00";
            cost = 0;
            DateTimePickerDate.Value = DateTimePickerDate.Value.GetValueOrDefault().AddSeconds(-DateTimePickerDate.Value.GetValueOrDefault().Second);
        }
        private void TextBlockPeopleCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBoxTables.Items.Clear();
            try
            {
                if (!TextBlockPeopleCount.Text.Equals(""))
                {
                    var busy = StorageData.TablesList.Where(t => StorageData.ReservationsList.Any(rl => rl.DateOn.AddHours(-1) < DateTimePickerDate.Value && rl.DateOn.AddHours(1) > DateTimePickerDate.Value
                    && StorageData.ReservationsDetailsList.Exists(rd => t.Id == rd.TablesId && rl.Id == rd.ReservationsId)));
                    var notBusy = StorageData.TablesList.Except(busy);
                    foreach (Tables table in notBusy)
                    {
                        if (table.Size == int.Parse(TextBlockPeopleCount.Text))
                        {
                            ComboBoxTables.Items.Add(table);
                            ComboBoxTables.DisplayMemberPath = "Number";
                        }
                    }
                }
            }
            catch (FormatException)
            {
                TextBlockPeopleCount.Text = null;
            }
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBoxTables.SelectedIndex >= 0 && int.Parse(TextBlockPeopleCount.Text) > 0)
                {
                    reservationDetailsList.Add(new CurrentReservation(((Tables)ComboBoxTables.SelectedItem).Id));
                    ListBoxDetails.Items.Add("Stół nr: " + ((Tables)ComboBoxTables.SelectedItem).Number + "\tLiczba osób: " + ((Tables)ComboBoxTables.SelectedItem).Size);
                    cost += ((Tables)ComboBoxTables.SelectedItem).ReservationCost;
                    peopleCount += int.Parse(TextBlockPeopleCount.Text);

                    TextBlockCost.Text = cost.ToString() + " zł";
                    ComboBoxTables.Items.RemoveAt(ComboBoxTables.SelectedIndex);
                }
            }
            catch (FormatException)
            {
                TextBlockPeopleCount.Text = null;
            }
        }

        private void ButtonRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxDetails.SelectedIndex > -1)
            {
                peopleCount -= StorageData.TablesList.Single(t => t.Id == reservationDetailsList.ElementAt(ListBoxDetails.SelectedIndex).TablesId).Size;
                cost -= StorageData.TablesList.Single(t => t.Id == reservationDetailsList.ElementAt(ListBoxDetails.SelectedIndex).TablesId).ReservationCost;
                TextBlockCost.Text = cost.ToString() + " zł";
                ComboBoxTables.Items.Add(StorageData.TablesList.Single(t => t.Id == reservationDetailsList.ElementAt(ListBoxDetails.SelectedIndex).TablesId));
                reservationDetailsList.RemoveAt(ListBoxDetails.SelectedIndex);
                ListBoxDetails.Items.RemoveAt(ListBoxDetails.SelectedIndex);
            }
        }
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListBoxDetails.Items.Count > 0 && TextBlockFirstName.Text != "" && TextBlockName.Text != "" && TextBlockPhone.Text != "")
                {
                    string message = "0071<EOP>" + DateTime.Today.ToString() + "<EOP>" + DateTimePickerDate.Value.GetValueOrDefault().ToString()
                        + "<EOP>" + TextBlockFirstName.Text + "<EOP>" + TextBlockName.Text + "<EOP>" + TextBlockPhone.Text + "<EOP>";

                    foreach (CurrentReservation currentReservation in reservationDetailsList)
                    {
                        message += currentReservation.TablesId.ToString() + "<EOP>";
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
                        ParentGrid.Children.Add(new ReservationsControl(_client));
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
