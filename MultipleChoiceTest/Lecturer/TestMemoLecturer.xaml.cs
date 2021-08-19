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
    /// Interaction logic for TestMemo.xaml
    /// </summary>
    public partial class TestMemo : Window
    {

        /*
         *      Initialize
         */

        public TestMemo(HomePageL BackPage, int testID, string testName)
        {
            InitializeComponent();

            lblTitle.Content += testName;
            this.BackPage = BackPage;
            this.testID = testID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AnswerReading memoGen = new AnswerReading();
            questions = memoGen.loadQuestions(testID);

            loadList();
            lstQuestions.SelectedIndex = 0;
        }

        /*
         *      Global Variables
         */

        HomePageL BackPage;
        int testID;
        List<Questions> questions;

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

        /*
         *      Events
         */

        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.Show();
        }

        private void LstQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txtQuestions.Clear();
                txtQuestions.Text = "Question: " + questions[lstQuestions.SelectedIndex].Question + "\n \n";

                txtQuestions.Text += "Answer 1: " + questions[lstQuestions.SelectedIndex].Answer1 + "\n";
                txtQuestions.Text += "Answer 2: " + questions[lstQuestions.SelectedIndex].Answer2 + "\n";
                txtQuestions.Text += "Answer 3: " + questions[lstQuestions.SelectedIndex].Answer3 + "\n";
                txtQuestions.Text += "Answer 4: " + questions[lstQuestions.SelectedIndex].Answer4 + "\n \n";

                switch (questions[lstQuestions.SelectedIndex].CorrectAnswer)
                {
                    case 1:
                        txtCorrectAnswer.Text = "Correct Answer: " + questions[lstQuestions.SelectedIndex].Answer1 + "\n \n";
                        break;
                    case 2:
                        txtCorrectAnswer.Text = "Correct Answer: " + questions[lstQuestions.SelectedIndex].Answer2 + "\n \n";
                        break;
                    case 3:
                        txtCorrectAnswer.Text = "Correct Answer: " + questions[lstQuestions.SelectedIndex].Answer3 + "\n \n";
                        break;
                    case 4:
                        txtCorrectAnswer.Text = "Correct Answer: " + questions[lstQuestions.SelectedIndex].Answer4 + "\n \n";
                        break;
                }
            }
            catch (Exception)
            {
                txtQuestions.Text = "";
                txtCorrectAnswer.Text = "";
            }
        }
    }
}
