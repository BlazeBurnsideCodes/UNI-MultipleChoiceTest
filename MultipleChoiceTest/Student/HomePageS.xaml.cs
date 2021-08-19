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

namespace MultipleChoiceTest.Student
{
    /// <summary>
    /// Interaction logic for HomePageS.xaml
    /// </summary>
    public partial class HomePageS : Window
    {

        /*
         *      Initialize
         */

        public HomePageS(int studentNumberInput, MainWindow BackPage)
        {
            InitializeComponent();

            studentNumber = studentNumberInput; //Sets the student number
            this.BackPage = BackPage;    //Sets a link to the previous page
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            displayTests(); //Calls the display test method
        }

        /*
         *      Global Variables
         */

        MainWindow BackPage; //Stores a link to the previous page
        int studentNumber;  //Stores the current students number

        /*
         *      Button Methods
         */

        //Sends the user to the selected test
        private void BtnStartTest_Click(object sender, RoutedEventArgs e)
        {
            string testName;    //Variable for holding the tests name
            int testMemo;
            string[] testSplit; //Variable for splitting the ID and testname
            try
            {
                testName = lstTestView.SelectedItem.ToString(); //Gets the selected item from the list
                testSplit = testName.Split('-');    //Splits the item into ID and Test
                testMemo = Convert.ToInt16(testSplit[0]);
                TestViewTake takeTest = new TestViewTake(testMemo, studentNumber,this);  //Uses the ID to create a link to the next page
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
            int testMemo;
            string[] testSplit; //Variable for splitting the ID and testname
            try
            {
                testName = lstTestTakenView.SelectedItem.ToString(); //Gets the selected item from the list
                testSplit = testName.Split('-');    //Splits the item into ID and Test
                testMemo = Convert.ToInt16(testSplit[0]);
                TestMemo showMemo = new TestMemo(testMemo, studentNumber, null, this);
                showMemo.Show();
                Hide();
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Please select a test from the test taken list.", "Selection error:");   //Shows an error message if no test was selected
            }
        }

        private void BtnViewMarks_Click(object sender, RoutedEventArgs e)
        {
            ViewMarks viewMarks = new ViewMarks(this, studentNumber);
            viewMarks.Show();
            Hide();
        }

        /*
         *      Helper Methods
         */

        //Fills the list with tests
        public void displayTests()
        {
            lstTestView.Items.Clear();  //Empties the list

            StudentSetup getTests = new StudentSetup(); //Creates a link to the database communicator

            List<string> takenTests = getTests.getFinishedTestNames(studentNumber);  //Gets a list of tests

            foreach (string test in takenTests)  //Loops for each item in the list
            {
                lstTestTakenView.Items.Add(test);    //Adds a line to the list
            }

            List<string> tests = getTests.getUnfinishedTestNames(studentNumber);  //Gets a list of tests

            foreach (string test in tests)  //Loops for each item in the list
            {
                if(!takenTests.Contains(test))
                    lstTestView.Items.Add(test);    //Adds a line to the list
            }
        }

        /*
         *      Events
         */

        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.Show();    //Shows the previous page when this one closes
        }
    }
}
