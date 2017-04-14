using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Data.SqlClient;

namespace ShvnFbrk
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }
        void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
        }
        void button2_Click(object sender, RoutedEventArgs e)
        {
            switch (Pass_box.Password == ConfPass_Box.Password)
            {
                case (true):
                    string NewUserCkeck;
                    ConClass ConCheck = new ConClass();
                    RegistryKey DataBase_Connection = Registry.CurrentConfig;
                    RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                    ConCheck.Connection_Options(Encrpt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                                                Encrpt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                                                Encrpt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                                                Encrpt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                    SqlConnection connectionNewUser = new SqlConnection(ConCheck.ConnectString);
                    SqlCommand Select_USID = new SqlCommand("select [dbo].[Login].[login]" +
                    " from [dbo].[Login] inner join[dbo].[roli] on " +
                    "[dbo].[Login].[roli_id] =[dbo].[roli].[ID_roli]" +
                    "where login='" + Login_text.Text + "' and alkogolik_pass='" + Pass_box.Password + "'", connectionNewUser);
                    try
                    {
                        connectionNewUser.Open();
                        NewUserCkeck = Select_USID.ExecuteScalar().ToString();
                        connectionNewUser.Close();
                        MessageBox.Show("Пользователь с именем " + NameClient.Text + ", уже есть!");
                    }
                    catch
                    {
                        string GuestRole;
                        int Tel_Value;
                        SqlConnection connectionNewUserInsert = new SqlConnection(ConCheck.ConnectString);
                        SqlCommand SelectGuestRole = new SqlCommand("select ID_roli from [dbo].[roli] where Role_Name = 'Гость'"
                            , connectionNewUserInsert);
                        connectionNewUserInsert.Open();
                        SqlCommand CreateNewUser = new SqlCommand("insert into [dbo].[Login]" +
                        "([FAM],[IM],[OTCH],[TEL],[Roli_id],[login],[pass])" +
                        "values ('" + NameClient.Text + "','" + FamKlient.Text + "','" + OtchKlient.Text
                        + "','" + PhoneKlient.Text + "',"
                        + "'1'" + ",'" + Login_text.Text + "','" + Pass_box.Password + "')"
                        , connectionNewUserInsert);
                        CreateNewUser.ExecuteNonQuery();
                        connectionNewUserInsert.Close();
                        MessageBox.Show("Вы прошли регистрацию!");
                    }
                    break;
                case (false):
                    MessageBox.Show("Пароли не совпадают, повторите попытку");
                    break;
            }
        }
    }
}
