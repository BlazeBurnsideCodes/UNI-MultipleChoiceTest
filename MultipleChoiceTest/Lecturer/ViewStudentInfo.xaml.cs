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
    /// Interaction logic for ViewStudentInfo.xaml
    /// </summary>
    public partial class ViewStudentInfo : Window
    {

        /*
         *      Initialize
         */

        public ViewStudentInfo(HomePageL BackPage, int lecturerID)
        {
            InitializeComponent();

            this.BackPage = BackPage;
            this.lecturerID = lecturerID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillList();
            loadStudents();
        }

        /*
         *      Global Variables
         */

        HomePageL BackPage;  //Stores the link to the previous page.
        int lecturerID;
        List<string> studentNumbers;
        List<TestResults> studentInfo;
        List<TestResults> currentStudent = new List<TestResults>();

        /*
         *      Helper Methods
         */

        public void fillList()
        {
            LecturerSetup loadStudents = new LecturerSetup();
            studentNumbers = loadStudents.getStudents(lecturerID);

            foreach (string studentNumber in studentNumbers)
            {
                lstStudentNumbers.Items.Add(studentNumber);
            }
        }

        public void loadStudents()
        {
            AnswerReading loadStudentMarks = new AnswerReading();
            studentInfo = loadStudentMarks.loadStudentInfo(studentNumbers);
        }

        public void displayStudentInfo(int studentID)
        {
            try
            {
                getStudentInfo(studentID);

                txtStudentInfo.Text = currentStudent[0].StudentID + ": " + currentStudent[0].StudentName + " " + currentStudent[0].StudentSurname + "\n \n";

                foreach (TestResults student in currentStudent)
                {
                    txtStudentInfo.Text += student.TestName + ": " + student.Mark + "/" + student.TestTotal + "\n";
                }
            }
            catch
            {
                txtStudentInfo.Text = "Student hasn't completed any tests yet.";
            }
        }

        public void getStudentInfo(int studentID)
        {
            currentStudent.Clear();

            foreach (TestResults possibleStudentSelected in studentInfo)
            {
                if(possibleStudentSelected.StudentID == studentID)
                {
                    currentStudent.Add(possibleStudentSelected);
                }
            }
        }        

        /*
         *      Events
         */

        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.Show();
        }

        private void LstStudentNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedStudent = Convert.ToInt16(lstStudentNumbers.SelectedItem);
            displayStudentInfo(selectedStudent);
        }
    }
}
