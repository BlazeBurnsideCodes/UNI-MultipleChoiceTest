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

namespace MultipleChoiceTest.Lecturer
{
    /// <summary>
    /// Interaction logic for TestViewCreate.xaml
    /// </summary>
    public partial class TestViewCreate : Window
    {

        /*
         *      Initialize
         */

        public TestViewCreate(string mode, int testID, int lecturerNumber, string testName, string module, HomePageL BackPage)
        {
            InitializeComponent();

            this.mode = mode;
            this.testID = testID;
            LecturerNumber = lecturerNumber;
            this.testName = testName;
            this.module = module;
            this.BackPage = BackPage;    // Stores a "this" value from the previous page for an easy return to that page.
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            questionNum = 1;    //Defaults the current question number to 1.
            lblQuestionX.Content = "Question " + questionNum;  //Sets the default question number label to 1.
            saveFlag = false;   //Sets saveFlag to false, telling the program that the question hasn't been changed since last save.

            if(mode.Equals("Edit"))
            {
                TestReading getQuestions = new TestReading();
                questions = getQuestions.getTest(testID);
                loadList();
                lstQuestions.SelectedIndex = 0;
            }
        }

        /*
         *      Global Variables
         */

        string mode;
        int testID;
        int LecturerNumber;
        string testName;
        string module;
        private List<Questions> questions = new List<Questions>();    //Stores a list of object Questions.
        int questionNum;    //Keeps track of the current question being worked with.
        Boolean saveFlag;   //Keeps track of questions, changing to true when the question has been edited and false when it has been saved.
        FindAnswers findCorrect = new FindAnswers();    //Allows use of the library "getCorrect".
        HomePageL BackPage;  //Creates a variable to store "this" from the previous page.
        
        /*
         *      Button Methods
         */

        //Saves the current question.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string question = txtQuestion.Text;     //Stores the inputed question.
            string answer1 = txtAnswer1.Text;       //Stores the inputed answer one.
            string answer2 = txtAnswer2.Text;       //Stores the inputed answer two.
            string answer3 = txtAnswer3.Text;       //Stores the inputed answer three.
            string answer4 = txtAnswer4.Text;       //Stores the inputed answer four.
            int correct = findCorrect.setAnswer(radAnswer1.IsChecked.Value, radAnswer2.IsChecked.Value, radAnswer3.IsChecked.Value, radAnswer4.IsChecked.Value);    //Uses the getCorrect library to find the selected radioButtons number, before storing it as an int.

            if ((question.Equals("Please Insert your Question Here.")) || (question.Equals("")))    //Checks that a question has been inserted.
            {
                MessageBox.Show("Please Insert a Question.", "Invalid Input:"); //"Invalid Input" error message.
            }
            else if ((answer1.Equals("Please Insert your Answer Here.")) || (answer1.Equals("")))   //Checks that a question has been inserted.
            {
                MessageBox.Show("Please Insert an Answer for Option 1.", "Invalid Input:"); //"Invalid Input" error message.
            }
            else if ((answer2.Equals("Please Insert your Answer Here.")) || (answer2.Equals("")))   //Checks that a question has been inserted.
            {
                MessageBox.Show("Please Insert an Answer for Option 2.", "Invalid Input:"); //"Invalid Input" error message.
            }
            else if ((answer3.Equals("Please Insert your Answer Here.")) || (answer3.Equals("")))   //Checks that a question has been inserted.
            {
                MessageBox.Show("Please Insert an Answer for Option 3.", "Invalid Input:"); //"Invalid Input" error message.
            }
            else if ((answer4.Equals("Please Insert your Answer Here.")) || (answer4.Equals("")))   //Checks that a question has been inserted.
            {
                MessageBox.Show("Please Insert an Answer for Option 4.", "Invalid Input:"); //"Invalid Input" error message.
            }
            else if (correct == 0)   //Checks that a question has been inserted.
            {
                MessageBox.Show("Please Select a Correct Answer.", "Invalid Input:");   //"Invalid Input" error message.
            }
            else
            {
                try     //Attempts to overwrite an existing instance of the question.
                {
                    questions[questionNum - 1] = new Questions(question, answer1, answer2, answer3, answer4, correct);    //Overwrites the preveous question of this number with a new object.
                }
                catch   //Creates a new Question object if no question number x exists.
                {
                    questions.Add(new Questions(question, answer1, answer2, answer3, answer4, correct));  //Creates a new object at index x.
                }
                loadList(); //Calls method loadlist to refresh the listbox. 
                saveFlag = false;   //Sets saveFlag to false, telling the program that the question hasn't been changed since last save.
            }
        }

        //Creates a new blank template for the user.
        private void BtnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            Boolean flag = false;
            if (saveFlag)   //Checks if the question has been edited before last save.
            {                
                if (MessageBox.Show("Any unsaved changes will be removed, are you sure you would like to create a new question?", "New Question:", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)  //Triggers if the user responds yes.
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }

            if(flag)
            {
                questionNum = questions.Count() + 1;  //Increases the current question by one.
                questionTitle();    //Calls the questionTitle method.
                txtAnswer1.Text = "Please Insert your Answer Here.";    //Sets the answer 1 textbox back to the default.
                txtAnswer2.Text = "Please Insert your Answer Here.";    //Sets the answer 2 textbox back to the default.
                txtAnswer3.Text = "Please Insert your Answer Here.";    //Sets the answer 3 textbox back to the default.
                txtAnswer4.Text = "Please Insert your Answer Here.";    //Sets the answer 4 textbox back to the default.
                txtQuestion.Text = "Please Insert your Question Here."; //Sets the question textbox back to the default.
                radAnswer1.IsChecked = false;   //Sets the answer 1 radiobutton to false.
                radAnswer2.IsChecked = false;   //Sets the answer 1 radiobutton to false.
                radAnswer3.IsChecked = false;   //Sets the answer 1 radiobutton to false.
                radAnswer4.IsChecked = false;   //Sets the answer 1 radiobutton to false.
                saveFlag = false;   //Sets the saveFlag back to false, indicating no changes have been made.
            }
        }

        //Removes the current question.
        private void BtnRemoveQuestion_Click(object sender, RoutedEventArgs e)
        {
            try     //Attempts to remove the selected question.
            {
                questions.RemoveAt(questionNum - 1);  //Removes the selected question.
                loadList(); //Reloads the test
                preparePage(questionNum - 2);   //Loads the question the came before the deleted question.
            }
            catch   //Throws an error if no question is selected.
            {
                MessageBox.Show("Please select a question to remove", "Removal Error"); //"Removal Error" error message.
            }
        }

        //Adds the test to the database
        private void BtnSubmitTest_Click(object sender, RoutedEventArgs e)
        {
            if(questions.Count > 0)
            {
                //Checks if the user is ready t submit their test
                if (MessageBox.Show("Are you sure you are ready to submit this test? \n This will save and close the test creator and take you back to the previous page.", "Submit Test:", MessageBoxButton.YesNo) == MessageBoxResult.Yes)    //Validates response 
                {
                    TestWriting addTestToDatabase = new TestWriting();  //Creates a link to testWriting
                    if (mode.Equals("Create"))
                    {
                        addTestToDatabase.createNewTest(testName, questions, LecturerNumber, module);   //Sends the method the information required to make the test
                        MessageBox.Show("Your test has been successfully added.", "Success");   //Confirms completion
                        Close();    //Closes the screen
                    }
                    else if (mode.Equals("Edit"))
                    {
                        addTestToDatabase.updateTest(testID, questions.Count());
                        addTestToDatabase.removeOldQuestions(testID);
                        addTestToDatabase.addQuestions(testID, questions);
                        MessageBox.Show("Your test has been successfully updated.", "Success");   //Confirms completion
                        Close();    //Closes the screen
                    }
                }
            }
            else
            {
                MessageBox.Show("Test requires a minimum of one question.", "Error: No questions");   //Confirms completion
            }            
        }

        /*
         *      Helper Methods
         */

        //Updates the question title.
        private void questionTitle()
        {
            lblQuestionX.Content = "Question " + questionNum;  //Sets lblQuestionX to the current question number.
        }

        //Fills the listbox with links to the created questions.
        private void loadList()
        {
            lstQuestions.Items.Clear(); //Clears the list.
            for (int i = 0; i < questions.Count(); i++)   //Repeats for each item in the list.
            {
                lstQuestions.Items.Add("Question " + (i + 1));  //Prints each of the questions.
            }
        }

        //Fills the page with information from the selected question.
        private void preparePage(int question)
        {
            try     //Attempts to find information for the selected question.
            {
                txtAnswer1.Text = questions[question].Answer1;    //Sets the answer 1 textbox to Answer1 from the object stored in the requested index.
                txtAnswer2.Text = questions[question].Answer2;    //Sets the answer 2 textbox to Answer2 from the object stored in the requested index.
                txtAnswer3.Text = questions[question].Answer3;    //Sets the answer 3 textbox to Answer3 from the object stored in the requested index.
                txtAnswer4.Text = questions[question].Answer4;    //Sets the answer 4 textbox to Answer4 from the object stored in the requested index.
                txtQuestion.Text = questions[question].Question;  //Sets the question textbox to Question from the object stored in the requested index.
                int selected = questions[question].CorrectAnswer; //Collects the integer from CorrectAnswer in the object stored in the requested index.
                switch (selected)    //Finds the case of the CorrectAnswer integer.
                {
                    case 1:
                        radAnswer1.IsChecked = true;  //Sets the first answer to true.
                        break;
                    case 2:
                        radAnswer2.IsChecked = true;  //Sets the second answer to true.
                        break;
                    case 3:
                        radAnswer3.IsChecked = true;  //Sets the third answer to true.
                        break;
                    case 4:
                        radAnswer4.IsChecked = true;  //Sets the forth answer to true.
                        break;
                }
                questionNum = question + 1; //Sets the question number to the new question number.
                questionTitle();    //Updtes the title.
            }
            catch
            {
                //If an error occurs, exit the method.
            }
        }

        /*
         *      Events
         */

        private void TxtQuestion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox answer = sender as TextBox; //Finds which textbox has been clicked in.

            if (answer.Equals(txtAnswer1))  //Checks if this is the textbox that was clicked.
            {
                if (txtAnswer1.Text.Equals("Please Insert your Answer Here."))  //Tests to make sure this is the first time the textbox has been clicked.
                {
                    txtAnswer1.Text = "";   //Empties the textbox.
                }
            }
            else if (answer.Equals(txtAnswer2))  //Checks if this is the textbox that was clicked.
            {
                if (txtAnswer2.Text.Equals("Please Insert your Answer Here."))  //Tests to make sure this is the first time the textbox has been clicked.
                {
                    txtAnswer2.Text = "";   //Empties the textbox.
                }
            }
            else if (answer.Equals(txtAnswer3))  //Checks if this is the textbox that was clicked.
            {
                if (txtAnswer3.Text.Equals("Please Insert your Answer Here."))  //Tests to make sure this is the first time the textbox has been clicked.
                {
                    txtAnswer3.Text = "";   //Empties the textbox.
                }
            }
            else if (answer.Equals(txtAnswer4))  //Checks if this is the textbox that was clicked.
            {
                if (txtAnswer4.Text.Equals("Please Insert your Answer Here."))  //Tests to make sure this is the first time the textbox has been clicked.
                {
                    txtAnswer4.Text = "";   //Empties the textbox.
                }
            }
            else if (answer.Equals(txtQuestion))  //Checks if this is the textbox that was clicked.
            {
                if (txtQuestion.Text.Equals("Please Insert your Question Here."))   //Tests to make sure this is the first time the textbox has been clicked.
                {
                    txtQuestion.Text = "";  //Empties the textbox.
                }
            }
        }

        private void LstQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            preparePage(lstQuestions.SelectedIndex);    //Calls the preparePage method, sending it the selectedIndex
        }

        private void textChanged(object sender, TextChangedEventArgs e)
        {
            saveFlag = true;    //Sets saveFlag to true, indicating a change has occured since last save.
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.displayTests();    //Refreshes the tests
            BackPage.Show();    //Shows the previous page.
        }
    }
}
