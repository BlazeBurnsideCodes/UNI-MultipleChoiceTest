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

namespace MultipleChoiceTest.Student
{
    /// <summary>
    /// Interaction logic for TestMemo.xaml
    /// </summary>
    public partial class TestMemo : Window
    {

        /*
         *      Initialize
         */

        public TestMemo(int testIDInput, int studentID, TestViewTake closeTestCreator, HomePageS openHomePage)
        {
            InitializeComponent();

            testID = testIDInput;
            StudentID = studentID;
            ClosePage = closeTestCreator;
            OpenPage = openHomePage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AnswerReading memoGen = new AnswerReading();
            questions = memoGen.loadQuestions(testID);
            List<int> markInfo = memoGen.loadMarks(testID, StudentID);
            answers = memoGen.loadAnswers(markInfo[0]);


            loadList();
            displayMark(markInfo[1], questions.Count());
        }

        /*
         *      Global Variables
         */

        int testID;
        int StudentID;
        List<Questions> questions;
        List<Questions> answers;
        TestViewTake ClosePage;
        HomePageS OpenPage;

        /*
         *      Helper Methods
         */

        private void loadList()
        {
            lstQuestions.Items.Clear(); //Clears the list.
            for (int i = 0; i < questions.Count(); i++)   //Repeats for each item in the list.
            {
                lstQuestions.Items.Add("Question " + (i + 1));  //Prints each of the questions.
            }
        }

        public void displayMark(int mark, int total)
        {
            txtTotalMarks.Text = mark + "/" + total;
        }

        /*
         *      Events
         */

        private void LstQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txtResults.Clear();
                txtResults.Text = "Question: " + questions[lstQuestions.SelectedIndex].Question + "\n \n";

                txtResults.Text += "Answer 1: " + questions[lstQuestions.SelectedIndex].Answer1 + "\n";
                txtResults.Text += "Answer 2: " + questions[lstQuestions.SelectedIndex].Answer2 + "\n";
                txtResults.Text += "Answer 3: " + questions[lstQuestions.SelectedIndex].Answer3 + "\n";
                txtResults.Text += "Answer 4: " + questions[lstQuestions.SelectedIndex].Answer4 + "\n \n";

                switch (questions[lstQuestions.SelectedIndex].CorrectAnswer)
                {
                    case 1:
                        txtResults.Text += "Correct Answer: " + questions[lstQuestions.SelectedIndex].Answer1 + "\n \n";
                        break;
                    case 2:
                        txtResults.Text += "Correct Answer: " + questions[lstQuestions.SelectedIndex].Answer2 + "\n \n";
                        break;
                    case 3:
                        txtResults.Text += "Correct Answer: " + questions[lstQuestions.SelectedIndex].Answer3 + "\n \n";
                        break;
                    case 4:
                        txtResults.Text += "Correct Answer: " + questions[lstQuestions.SelectedIndex].Answer4 + "\n \n";
                        break;
                }

                txtResults.Text += "====================================" + "\n";

                switch (answers[lstQuestions.SelectedIndex].CorrectAnswer)
                {
                    case 1:
                        txtResults.Text += "Your Answer: " + questions[lstQuestions.SelectedIndex].Answer1 + "\n \n";
                        break;
                    case 2:
                        txtResults.Text += "Your Answer: " + questions[lstQuestions.SelectedIndex].Answer2 + "\n \n";
                        break;
                    case 3:
                        txtResults.Text += "Your Answer: " + questions[lstQuestions.SelectedIndex].Answer3 + "\n \n";
                        break;
                    case 4:
                        txtResults.Text += "Your Answer: " + questions[lstQuestions.SelectedIndex].Answer4 + "\n \n";
                        break;
                }
            }
            catch
            {
                txtResults.Clear();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(OpenPage == null)
            {
                ClosePage.Close();
            }
            else
            {
                OpenPage.Show();
            }
        }
    }
}
