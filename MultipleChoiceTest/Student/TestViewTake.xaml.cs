using getCorrect;
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
    /// Interaction logic for TestViewTake.xaml
    /// </summary>
    public partial class TestViewTake : Window
    {

        /*
         *      Initialize
         */

        public TestViewTake(int selectedTestID, int studentNumber, HomePageS BackPage)
        {
            InitializeComponent();

            testID = selectedTestID;    //Sets the testID
            studNum = studentNumber;
            this.BackPage = BackPage;    //Sets a link to the previous page
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TestReading loadTest = new TestReading();   //Creates a link to the test reading class
            test = loadTest.getTest(testID);    //Collects all the questions, answers and correct answer from the TestReading class

            currentQuestion = 1;    //Defaults the current question to 1
            loadList(); //Calls the load list method
            preparePage();  //Calls the prepare page methodprepareStudentAnswers
            prepareStudentAnswers();
            radAnswer1.IsChecked = false;
        }

        /*
         *      Global Variables
         */

        int testID; //Stores the test id
        int studNum;
        HomePageS BackPage; //Stores a link to the previous page
        int currentQuestion;    //Stores the current question
        List<Questions> test; //Stores a list of objects Questions
        List<Questions> answers = new List<Questions>(); //Stores a list of objects Questions
        FindAnswers findCorrect = new FindAnswers();    //Allows use of the getAnswers dll

        /*
         *      Button Methods
         */

        private void BtnSaveAnswer_Click(object sender, RoutedEventArgs e)
        {
            answers[currentQuestion - 1].CorrectAnswer = findCorrect.setAnswer(radAnswer1.IsChecked.Value, radAnswer2.IsChecked.Value, radAnswer3.IsChecked.Value, radAnswer4.IsChecked.Value);    //Adds the students answer to the array
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            answers[currentQuestion - 1].CorrectAnswer = findCorrect.setAnswer(radAnswer1.IsChecked.Value, radAnswer2.IsChecked.Value, radAnswer3.IsChecked.Value, radAnswer4.IsChecked.Value);    //Adds the students answer to the array

            if (currentQuestion < test.Count())  //Tests if it can go higher
            {
                lstQuestions.SelectedIndex = currentQuestion;   //Sets the current question one higher, accounting for the lists starting point of 0
            }
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            answers[currentQuestion - 1].CorrectAnswer = findCorrect.setAnswer(radAnswer1.IsChecked.Value, radAnswer2.IsChecked.Value, radAnswer3.IsChecked.Value, radAnswer4.IsChecked.Value);    //Adds the students answer to the array

            if (currentQuestion > 1) //Tests if it is able to go lower
            {
                lstQuestions.SelectedIndex = currentQuestion - 2;   //Sets the current question one lower, accounting for the lists starting point of 0
            }
        }

        private void BtnSubmitTest_Click(object sender, RoutedEventArgs e)
        {
            answers[currentQuestion - 1].CorrectAnswer = findCorrect.setAnswer(radAnswer1.IsChecked.Value, radAnswer2.IsChecked.Value, radAnswer3.IsChecked.Value, radAnswer4.IsChecked.Value);    //Adds the students answer to the array
            
            int mark = 0;
            for (int i = 0; i < test.Count; i++)
            {
                if(test[i] == answers[i])   //Calculates the students mark
                {
                    mark++;
                }
            }

            TestReading saveMark = new TestReading();
            int markID = saveMark.saveTestResults(mark, studNum, testID);

            foreach(Questions answer in answers)
            {
                saveMark.saveStudentAnswers(markID, answer.QuestionID, answer.CorrectAnswer);
            }

            BackPage.displayTests();

            TestMemo results = new TestMemo(testID, studNum, this, null);  //Creates a link to the student memo
            results.Show(); //Shows the memo
            Hide(); //Hides this
        }

        /*
         *      Methods
         */

        private void loadList()
        {
            lstQuestions.Items.Clear(); //Clears the list.
            for (int i = 0; i < test.Count(); i++)   //Repeats for each item in the list.
            {
                lstQuestions.Items.Add("Question " + (i + 1));  //Prints each of the questions.
            }
        }

        private void preparePage()
        {
            try     //Attempts to find information for the selected question.
            {
                //if (currentQuestion < test.Count())  //If the current question is less than the number of questions in the test
                //{                    
                    txtAnswer1.Text = test[currentQuestion - 1].Answer1;    //Sets the answer 1 textbox to Answer1 from the object stored in the requested index.
                    txtAnswer2.Text = test[currentQuestion - 1].Answer2;    //Sets the answer 2 textbox to Answer2 from the object stored in the requested index.
                    txtAnswer3.Text = test[currentQuestion - 1].Answer3;    //Sets the answer 3 textbox to Answer3 from the object stored in the requested index.
                    txtAnswer4.Text = test[currentQuestion - 1].Answer4;    //Sets the answer 4 textbox to Answer4 from the object stored in the requested index.
                    txtQuestion.Text = test[currentQuestion - 1].Question;  //Sets the question textbox to Question from the object stored in the requested index.
                    questionTitle();    //Updtes the title.

                    switch (answers[currentQuestion - 1].CorrectAnswer)    //Fills in the answer if one has been selected
                    {
                        case 1: //Sets the selected answer to true
                            radAnswer1.IsChecked = true;
                            radAnswer2.IsChecked = false;
                            radAnswer3.IsChecked = false;
                            radAnswer4.IsChecked = false;
                            break;
                        case 2: //Sets the selected answer to true
                            radAnswer1.IsChecked = false;
                            radAnswer2.IsChecked = true;
                            radAnswer3.IsChecked = false;
                            radAnswer4.IsChecked = false;
                            break;
                        case 3: //Sets the selected answer to true
                            radAnswer1.IsChecked = false;
                            radAnswer2.IsChecked = false;
                            radAnswer3.IsChecked = true;
                            radAnswer4.IsChecked = false;
                            break;
                        case 4: //Sets the selected answer to true
                            radAnswer1.IsChecked = false;
                            radAnswer2.IsChecked = false;
                            radAnswer3.IsChecked = false;
                            radAnswer4.IsChecked = true;
                            break;
                        default:    //Sets all to false if none has been selected
                            radAnswer1.IsChecked = false;
                            radAnswer2.IsChecked = false;
                            radAnswer3.IsChecked = false;
                            radAnswer4.IsChecked = false;
                            break;
                    }
                //}
            }
            catch
            {
                //If an error occurs, exit the method.
            }
        }

        //Updates the question title.
        private void questionTitle()
        {
            lblQuestionX.Content = "Question " + currentQuestion;  //Sets lblQuestionX to the current question number.
        }

        private void prepareStudentAnswers()
        {
            int questionID, questionNum;
            foreach(Questions question in test)
            {
                questionID = question.QuestionID;
                questionNum = question.QuestionNum;
                answers.Add(new Questions(questionID, questionNum, 0));
            }
        }

        /*
         *      Events
         */

        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.Show();    //Shows the previous page
        }

        private void LstQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentQuestion = lstQuestions.SelectedIndex + 1;   //Changes the current question
            preparePage();  //Calls the prepare page method
        }
    }
}
