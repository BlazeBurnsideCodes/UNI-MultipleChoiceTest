using MultipleChoiceTest.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MultipleChoiceTest.Register
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window
    {

        /*
         *      Initialize
         */

        public UserDetails(MainWindow BackPage)
        {
            InitializeComponent();

            this.BackPage = BackPage;
        }

        /*
         *      Global Variables
         */

        MainWindow BackPage;

        /*
         *      Button Methods
         */

        private void BtnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if(!(username.Trim().Equals("") || password.Trim().Equals("")))
            {
                if (chkLecturer.IsChecked.Value)
                {
                    string adminUsername = txtAdminUsername.Text;
                    string adminPassword = txtAdminPassword.Text;

                    if(adminUsername.Equals("Admin") && adminPassword.Equals("Admin"))
                    {
                        UserInformation lecturerCreate = new UserInformation(this, username, password, 1);
                        lecturerCreate.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Admin login failed.", "Error: Values Incorrect");
                    }
                }
                else
                {
                    AddNewUser checkUsername = new AddNewUser();
                    
                    if (checkUsername.checkUsername(username))
                    {
                        UserInformation studentCreate = new UserInformation(this, username, password, 0);
                        studentCreate.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username already exists.", "Error: Username invalid");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please make sure all fields contain valid values.", "Error: Values Missing");
            }
        }

        /*
         *      Events
         */

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            txtAdminUsername.IsEnabled = true;
            txtAdminPassword.IsEnabled = true;
        }

        private void ChkLecturer_Unchecked(object sender, RoutedEventArgs e)
        {
            txtAdminUsername.IsEnabled = false;
            txtAdminPassword.IsEnabled = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.Show();
        }
    }
}
