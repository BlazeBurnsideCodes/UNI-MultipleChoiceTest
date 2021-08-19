using MultipleChoiceTest.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UserInformation.xaml
    /// </summary>
    public partial class UserInformation : Window
    {
        public UserInformation(UserDetails BackPage, string username, string password, int userType)
        {
            InitializeComponent();
            DataContext = this;

            IncludedModules = new ObservableCollection<string>();
            ExcludedModules = new ObservableCollection<string>();

            this.BackPage = BackPage;
            this.username = username;
            this.password = password;
            this.userType = userType;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillCombobox();
        }

        /*
         *      Binding objects
         */

        public ObservableCollection<string> ExcludedModules { get; set; }
        public ObservableCollection<string> IncludedModules { get; set; }

        /*
         *      Global Variables
         */

        UserDetails BackPage;
        string username;
        string password;
        int userType;

        /*
         *      Button Methods
         */

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string surname = txtSurname.Text;

            if (!(firstName.Trim().Equals("") || surname.Trim().Equals("")))
            {
                if(IncludedModules.Count() > 0)
                {
                    AddNewUser createUser = new AddNewUser();
                    createUser.createUser(username, password, userType);

                    if(userType == 0)
                    {
                        int studentNumber = createUser.createStudent(username, firstName, surname);
                        List<string> modules = new List<string>(IncludedModules);
                        createUser.linkUserModuleStudent(studentNumber, modules);

                        MessageBox.Show("New student added!", "Success!");
                        Close();
                    }
                    else
                    {
                        int lecturerNumber = createUser.createLecturer(username, firstName, surname);
                        List<string> modules = new List<string>(IncludedModules);
                        createUser.linkUserModuleLecturer(lecturerNumber, modules);

                        MessageBox.Show("New lecturer added!", "Success!");
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please select at least one module.", "Error: Modules Missing");
                }
            }
            else
            {
                MessageBox.Show("Please make sure all fields contain valid values.", "Error: Values Missing");
            }
        }

        private void BtnAddModule_Click(object sender, RoutedEventArgs e)
        {
            string module = cmbExcludedModule.SelectedItem.ToString();
            ExcludedModules.Remove(module);
            IncludedModules.Add(module);
        }

        private void BtnRemoveModule_Click(object sender, RoutedEventArgs e)
        {
            string module = cmbIncludedModule.SelectedItem.ToString();
            IncludedModules.Remove(module);
            ExcludedModules.Add(module);
        }

        /*
         *      Helper Methods
         */

        public void fillCombobox()
        {
            AddNewUser getModules = new AddNewUser(); //Creates a link to the add new user class
            List<string> modules = getModules.getModules(); //Fills a list with the test ID's

            foreach (string module in modules)  //For each item in the list
            {
                ExcludedModules.Add(module);   //Adds a line to the lst
            }
        }

        /*
         *      Events
         */

        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.Close();
        }
    }
}
