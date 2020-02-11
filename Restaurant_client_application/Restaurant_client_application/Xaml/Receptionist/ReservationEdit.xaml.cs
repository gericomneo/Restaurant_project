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
    /// Interaction logic for ReservationEdit.xaml
    /// </summary>
    public partial class ReservationEdit : UserControl
    {
        List<ReservationsDetails> reservationDetailsList = new List<ReservationsDetails>();
        List<ReservationsDetails> oldReservationDetailsList = new List<ReservationsDetails>();
        List<Tables> reservationTables = new List<Tables>();
        List<Tables> reservationUncheckedTables = new List<Tables>();
        Reservations selectedReservation;
        int peopleCount = 0;
        double cost = 0;
        TcpClient _client;
        public ReservationEdit(TcpClient client, Reservations selected)
        {
            InitializeComponent();
            _client = client;
            selectedReservation = selected;
            DateTimePickerDate.Value = selectedReservation.DateOn;
            ComboBoxTables.IsEnabled = false;
            TextBlockFirstName.Text = selectedReservation.FirstName;
            TextBlockName.Text = selectedReservation.Name;
            TextBlockPhone.Text = selectedReservation.Phone;

            foreach (ReservationsStates reservationsStates in StorageData.ReservationsStatesList)
            {
                ComboBoxStates.Items.Add(reservationsStates);
            }
            ComboBoxStates.DisplayMemberPath = "Name";
            ComboBoxStates.SelectedItem = StorageData.ReservationsStatesList.Single(r => r.Id == selectedReservation.ReservationsStatesId);
        }
        private void DateTimePickerDate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DateTimePickerDate.Value = DateTimePickerDate.Value.GetValueOrDefault().AddSeconds(-DateTimePickerDate.Value.GetValueOrDefault().Second);
            ListBoxDetails.Items.Clear();
            reservationDetailsList.Clear();
            reservationTables.Clear();
            TextBlockPeopleCount.Text = "";
            TextBlockPeopleCount.IsEnabled = true;
            TextBlockCost.Text = "0,00";
            cost = 0;
            if (DateTimePickerDate.Value == selectedReservation.DateOn)
            {
                oldReservationDetailsList = StorageData.ReservationsDetailsList.Where(r => r.ReservationsId == selectedReservation.Id).ToList();
                reservationDetailsList = StorageData.ReservationsDetailsList.Where(r => r.ReservationsId == selectedReservation.Id).ToList();
                var reservationTables = StorageData.TablesList.Where(t => reservationDetailsList.Exists(r => t.Id == r.TablesId));
                foreach (Tables table in reservationTables)
                {
                    peopleCount += table.Size;
                    cost += table.ReservationCost;
                    ListBoxDetails.Items.Add("Stół nr: " + table.Number + "\tLiczba osób: " + table.Size);
                }
                TextBlockCost.Text = cost.ToString() + " zł";
            }
        }
        private void TextBlockPeopleCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBoxTables.Items.Clear();
            ComboBoxTables.IsEnabled = true;
            try
            {
                if (!TextBlockPeopleCount.Text.Equals(""))
                {
                    var busy = StorageData.TablesList.Where(t => StorageData.ReservationsList.Exists(rl => rl.DateOn.AddHours(-1) < DateTimePickerDate.Value && rl.DateOn.AddHours(1) > DateTimePickerDate.Value
                    && StorageData.ReservationsDetailsList.Exists(rd => t.Id == rd.TablesId)));
                    var notBusy = StorageData.TablesList.Except(busy);
                    notBusy = notBusy.Except(reservationTables);
                    notBusy = notBusy.Concat(reservationUncheckedTables);
                    notBusy = notBusy.Distinct();
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
                TextBlockPeopleCount.Text = "";
            }
        }

        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBoxTables.SelectedIndex >= 0 && int.Parse(TextBlockPeopleCount.Text) > 0)
                {
                    reservationDetailsList.Add(new ReservationsDetails(selectedReservation.Id, ((Tables)ComboBoxTables.SelectedItem).Id));
                    reservationTables.Add((Tables)ComboBoxTables.SelectedItem);
                    reservationUncheckedTables.Remove((Tables)ComboBoxTables.SelectedItem);
                    ListBoxDetails.Items.Add("Stół nr: " + ((Tables)ComboBoxTables.SelectedItem).Number + "\tLiczba osób: " + ((Tables)ComboBoxTables.SelectedItem).Size);
                    cost += ((Tables)ComboBoxTables.SelectedItem).ReservationCost;
                    TextBlockCost.Text = cost.ToString() + " zł";
                    TextBlockPeopleCount.Text = "";
                    ComboBoxTables.IsEnabled = false;
                }
            }
            catch (FormatException)
            {
                TextBlockPeopleCount.Text = "";
            }
            catch (ArgumentOutOfRangeException ae)
            {
                MessageBox.Show(ae.ToString());
            }
        }

        private void ButtonRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxDetails.SelectedIndex > -1)
            {
                peopleCount -= StorageData.TablesList.Single(t => t.Id == reservationDetailsList.ElementAt(ListBoxDetails.SelectedIndex).TablesId).Size;
                cost -= StorageData.TablesList.Single(t => t.Id == reservationDetailsList.ElementAt(ListBoxDetails.SelectedIndex).TablesId).ReservationCost;
                TextBlockCost.Text = cost.ToString() + " zł";
                reservationUncheckedTables.Add(StorageData.TablesList.Single(t => t.Id == reservationDetailsList.ElementAt(ListBoxDetails.SelectedIndex).TablesId));
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

                    string message = "0072<EOP>" + selectedReservation.Id.ToString() + "<EOP>" + selectedReservation.Date.ToString()
                        + "<EOP>" + DateTimePickerDate.Value.GetValueOrDefault().ToString() + "<EOP>" + TextBlockFirstName.Text + "<EOP>"
                        + TextBlockName.Text + "<EOP>" + TextBlockPhone.Text + "<EOP>" + ((ReservationsStates)ComboBoxStates.SelectedItem).Id + "<EOP>";

                    if (oldReservationDetailsList.Except(reservationDetailsList).Count() > 0)
                    {
                        message += "0076<EOP>";
                        foreach (ReservationsDetails oldReservationDetails in oldReservationDetailsList.Except(reservationDetailsList))
                        {
                            message += oldReservationDetails.TablesId.ToString() + "<EOP>";
                        }
                    }

                    if (reservationDetailsList.Except(oldReservationDetailsList).Count() > 0)
                    {
                        message += "0075<EOP>";
                        foreach (ReservationsDetails currentReservation in reservationDetailsList.Except(oldReservationDetailsList))
                        {
                            message += currentReservation.TablesId.ToString() + "<EOP>";
                        }
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
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new ReservationsControl(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
    }
}
