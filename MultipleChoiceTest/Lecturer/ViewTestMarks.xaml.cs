using MultipleChoiceTest.Database;
using MultipleChoiceTest.Object;
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
    /// Interaction logic for ViewTestMarks.xaml
    /// </summary>
    public partial class ViewTestMarks : Window
    {

        /*
         *      Initialize
         */

        public ViewTestMarks(HomePageL BackPage, int testID, string testName)
        {
            InitializeComponent();

            this.BackPage = BackPage;
            this.testID = testID;
            this.testName = testName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AnswerReading loadResults = new AnswerReading();
            studentResults = loadResults.loadStudentResults(testID);

            displayResults();
        }

        /*
         *      Global Variables
         */

        HomePageL BackPage;  //Stores the link to the previous page.
        int testID;
        string testName;
        List<TestResults> studentResults;

        /*
         *      Helper Methods
         */

        public void displayResults()
        {
            txtTestMarks.Text = testName + "\n \n";
            foreach (TestResults student in studentResults)
            {
                txtTestMarks.Text += "Student Number: " + student.StudentID + "\t \t Result: " + student.Mark + "/" + student.TestTotal + "\n";
            }
        }

        /*
         *      Events
         */

        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.Show();
        }
    }
}
