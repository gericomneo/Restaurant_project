using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for UserEdit.xaml
    /// </summary>
    public partial class UserEdit : UserControl
    {
        Users selectedUser;
        TcpClient _client;
        public UserEdit(TcpClient client, int selected)
        {
            InitializeComponent();
            _client = client;
            
            selectedUser = StorageData.UsersList.ElementAt(selected);

            foreach (UsersTypesId type in StorageData.UsersTypesIdList)
            {
                ComboBoxType.Items.Add(type);
                ComboBoxType.DisplayMemberPath = "Name";
            }
            TextBoxId.Text = selectedUser.Id.ToString();
            TextBoxAddressCode.Text = selectedUser.AddressCode;
            TextBoxCity.Text = selectedUser.City;
            TextBoxEmail.Text = selectedUser.Email;
            TextBoxFirstName.Text = selectedUser.FirstName;
            TextBoxHouse.Text = selectedUser.HouseNumber;
            TextBoxLogin.Text = selectedUser.Login;
            TextBoxName.Text = selectedUser.Name;
            TextBoxPhone.Text = selectedUser.Phone;
            TextBoxSalary.Text = selectedUser.Salary.ToString();
            TextBoxStreet.Text = selectedUser.Street;
            ComboBoxType.SelectedIndex = selectedUser.UsersTypesId - 1;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (TextBoxCity.Text != "" && TextBoxEmail.Text != "" && TextBoxFirstName.Text != "" && TextBoxHouse.Text != "" && TextBoxId.Text != "" && TextBoxLogin.Text != "" && TextBoxName.Text != "" &&
                      PasswordBoxPassword.Password != "" && TextBoxPhone.Text != "" && TextBoxAddressCode.Text != "" && TextBoxStreet.Text != "" && ComboBoxType.SelectedIndex> -1)
                {
                    _client.requestStatus = 0;
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                    myMessageBox.ShowDialog();

                    if (_client.requestStatus == 1)
                    {
                        _client.SendMessage("0012<EOP>" + selectedUser.Id.ToString() + "<EOP>" + TextBoxLogin.Text + "<EOP>" + PasswordHash.GenerateSHA512String(PasswordBoxPassword.Password) + "<EOP>" + TextBoxEmail.Text + "<EOP>" + TextBoxFirstName.Text
                            + "<EOP>" + TextBoxName.Text + "<EOP>" + TextBoxCity.Text + "<EOP>" + TextBoxAddressCode.Text + "<EOP>" + TextBoxStreet.Text + "<EOP>" + TextBoxHouse.Text + "<EOP>" + TextBoxPhone.Text +
                             "<EOP>" + (ComboBoxType.SelectedIndex + 1).ToString() + "<EOP>" + TextBoxSalary.Text + "<EOP>");
                        _client.requestStatus = 0;
                        int time = DateTime.Now.Second;
                        while (_client.requestStatus == 0)
                        {
                            int delay = time - DateTime.Now.Second;
                            if (delay > 30)
                            {
                                myMessageBox = new MyMessageBox(_client, 3);
                                myMessageBox.ShowDialog();
                                return;
                            }
                        }
                        myMessageBox = new MyMessageBox(_client, _client.requestStatus);
                        myMessageBox.ShowDialog();

                        Grid ParentGrid = (Grid)this.Parent;
                        ParentGrid.Children.Add(new UsersControl(_client));
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
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new UsersControl(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }

        private void TextBoxId_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxId.Text = selectedUser.Id.ToString();
        }
    }
}
