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

namespace MultipleChoiceTest.Lecturer
{
    /// <summary>
    /// Interaction logic for HomePageL.xaml
    /// </summary>
    public partial class HomePageL : Window
    {

        /*
         *      Initialize
         */

        public HomePageL(int lecturerNumber, MainWindow BackPage)
        {
            InitializeComponent();
            LecturerNumber = lecturerNumber;    //Sets the lecturer number
            this.BackPage = BackPage;    //Sets a link to the previous page.
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            displayTests(); //Calls the display tests method when the form loads.
            fillCombobox();
        }

        /*
         *      Global Variables
         */

        int LecturerNumber; //Stores the current lecturers number.
        MainWindow BackPage;  //Stores the link to the previous page.

        /*
         *      Button Methods
         */

        private void BtnCreateTest_Click(object sender, RoutedEventArgs e)
        {
            string testName = txtTestName.Text; //Sets the inserted testname to a variable 
            string module;
            try
            {
                module = cmbModule.SelectedItem.ToString();
            }
            catch
            {
                module = "";
            }
            

            if (!(testName.Equals(""))) //Makes sure a name has been given to the test
            {
                if(!(module.Equals("")))
                {
                    TestViewCreate View = new TestViewCreate("Create", -1, LecturerNumber, testName, module, this); //Creates a link to the next page
                    View.Show();    //Shows the next page
                    Hide();    //Hides this page
                }
                else
                {
                    MessageBox.Show("Please select a module.", "Error: No Module Found");   //Tells the user that they have not entered a test name
                }
            }
            else
            {
                MessageBox.Show("Please insert a test name in the textbox provided", "Error: No Name Found");   //Tells the user that they have not entered a test name
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            string testName;    //Variable for holding the tests name
            string[] testSplit; //Variable for splitting the ID and testname
            try
            {
                testName = lstTestView.SelectedItem.ToString(); //Gets the selected item from the list
                testSplit = testName.Split('-');    //Splits the item into ID and Test
                int testID = Convert.ToInt16(testSplit[0]);
                LecturerSetup getModule = new LecturerSetup();
                string module = getModule.getTestModule(testID);
                TestViewCreate takeTest = new TestViewCreate("Edit", testID, LecturerNumber, testName, module, this);  //Uses the ID to create a link to the next page
                takeTest.Show();    //Shows the take test page
                Hide();    //Hides the current page
            }
            catch
            {
                MessageBox.Show("Please select a test.", "Selection error:");   //Shows an error message if no test was selected
            }
        }

        private void BtnViewMemo_Click(object sender, RoutedEventArgs e)
        {
            string testName;    //Variable for holding the tests name
            string[] testSplit; //Variable for splitting the ID and testname
            try
            {
                testName = lstTestView.SelectedItem.ToString(); //Gets the selected item from the list
                testSplit = testName.Split('-');    //Splits the item into ID and Test
                int testID = Convert.ToInt16(testSplit[0]);
                testName = testSplit[1].ToString().Substring(2);
                TestMemo memo = new TestMemo(this, testID, testName);
                memo.Show();
                Hide();
            }
            catch
            {
                MessageBox.Show("Please select a test.", "Selection error:");   //Shows an error message if no test was selected
            }
        }

        private void BtnViewTestMarks_Click(object sender, RoutedEventArgs e)
        {
            string testName;    //Variable for holding the tests name
            string[] testSplit; //Variable for splitting the ID and testname
            try
            {
                testName = lstTestView.SelectedItem.ToString(); //Gets the selected item from the list
                testSplit = testName.Split('-');    //Splits the item into ID and Test
                int testID = Convert.ToInt16(testSplit[0]);
                testName = testSplit[1].ToString().Substring(2);
                ViewTestMarks memo = new ViewTestMarks(this, testID, testName);
                memo.Show();
                Hide();
            }
            catch
            {
                MessageBox.Show("Please select a test.", "Selection error:");   //Shows an error message if no test was selected
            }
        }

        private void BtnViewStudentInfo_Click(object sender, RoutedEventArgs e)
        {
            ViewStudentInfo viewStudents = new ViewStudentInfo(this, LecturerNumber);
            viewStudents.Show();
            Hide();
        }

        /*
         *      Helper Methods
         */

        //Fills the lstTestView with the test ID and name
        public void displayTests()
        {
            lstTestView.Items.Clear();  //Clears the list

            LecturerSetup getTests = new LecturerSetup(); //Creates a link to the database communicator
            List<string> tests = getTests.getTestNames(LecturerNumber); //Fills a list with the test ID's and test name

            foreach (string test in tests)  //For each item in the list
            {
                lstTestView.Items.Add(test);    //Adds a line to the lst
            }
        }

        public void fillCombobox()
        {
            LecturerSetup getModules = new LecturerSetup(); //Creates a link to the lecturer setup
            List<string> modules = getModules.getModules(LecturerNumber, "L"); //Fills a list with the test ID's and test name

            foreach (string module in modules)  //For each item in the list
            {
                cmbModule.Items.Add(module);    //Adds a line to the lst
            }
        }

        /*
         *      Events
         */

        //Shows the previous page when this one is closed.
        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.Show();
        }

        private void TxtTestName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (txtTestName.Text.Equals("Insert Test Name"))
            {
                txtTestName.Text = "";
            }
        }
    }
}
