using pl.edu.wat.wcy.pz.restaurant_client_application.Controllers;
using pl.edu.wat.wcy.pz.restaurant_client_application.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Xaml.Admin
{
    /// <summary>
    /// Interaction logic for UserNew.xaml
    /// </summary>
    public partial class UserNew : UserControl
    {
        TcpClient _client;
        public UserNew(TcpClient client)
        {
            InitializeComponent();
            _client = client;
            foreach (UsersTypesId type in StorageData.UsersTypesIdList)
            {
                ComboBoxType.Items.Add(type);
                ComboBoxType.DisplayMemberPath = "Name";
            }
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextBoxCity.Text != "" && TextBoxEmail.Text != "" && TextBoxFirstName.Text != "" && TextBoxHouse.Text != "" && TextBoxLogin.Text != "" && TextBoxName.Text != "" &&
                      PasswordBoxPassword.Password != "" && TextBoxPhone.Text != "" && TextBoxAddressCode.Text != "" && TextBoxStreet.Text != "" && ComboBoxType.SelectedIndex > -1)
                {


                    _client.requestStatus = 0;
                    MyMessageBox myMessageBox = new MyMessageBox(_client, 0);
                    myMessageBox.ShowDialog();

                    if (_client.requestStatus == 1)
                    {
                        _client.SendMessage("0011<EOP>" + TextBoxLogin.Text + "<EOP>" + PasswordHash.GenerateSHA512String(PasswordBoxPassword.Password) + "<EOP>" + TextBoxEmail.Text + "<EOP>" + TextBoxFirstName.Text + "<EOP>" + TextBoxName.Text +
                             "<EOP>" + TextBoxCity.Text + "<EOP>" + TextBoxAddressCode.Text + "<EOP>" + TextBoxStreet.Text + "<EOP>" + TextBoxHouse.Text + "<EOP>" + TextBoxPhone.Text +
                              "<EOP>" + (ComboBoxType.SelectedIndex + 1).ToString() + "<EOP>" + TextBoxSalary.Text + "<EOP>");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Grid ParentGrid = (Grid)this.Parent;
            ParentGrid.Children.Add(new UsersControl(_client));
            ParentGrid.Children.RemoveAt(ParentGrid.Children.Count - 2);
        }
    }
}
