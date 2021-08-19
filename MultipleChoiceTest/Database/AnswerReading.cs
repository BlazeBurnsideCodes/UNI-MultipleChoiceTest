using MultipleChoiceTest.Object;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Database
{
    class AnswerReading : DatabaseCommunicator
    {
        public List<Questions> loadQuestions(int testID)
        {
            List<Questions> questions = new List<Questions>();   //Creates a list to store object Questions for the memo

            cnn.Open(); //Opens the connection string

            //Declares the query then sets it as a SqlCommand
            string sqlQuery = "SELECT Question, Answer1, Answer2, Answer3, Answer4, AnswerCorrect FROM QuestionInfo WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //    //The following code was taken from C# Station.
            //    //Author: Joe_Mayo
            command.Parameters.AddWithValue("@TestID", testID);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the datareader

            while (dataReader.Read())   //Loops through each row
            {
                string question = dataReader.GetValue(0).ToString(); //Inserts the question into the lists object
                string Answer1 = dataReader.GetValue(1).ToString();
                string Answer2 = dataReader.GetValue(2).ToString();
                string Answer3 = dataReader.GetValue(3).ToString();
                string Answer4 = dataReader.GetValue(4).ToString();
                int correctAnswer = Convert.ToInt16(dataReader.GetValue(5).ToString());
                questions.Add(new Questions(question, Answer1, Answer2, Answer3, Answer4, correctAnswer));
            }

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return questions;
        }



        public List<int> loadMarks(int testID, int studentID)
        {
            List<int> marks = new List<int>();   //Creates a list to store object answers for the test

            cnn.Open(); //Opens the connection string

            //Declares the query then sets it as a SqlCommand
            string sqlQuery = "SELECT MarkID, Mark FROM MarkInfo WHERE TestID = @TestID AND StudentNumber = @StudentNumber";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //    //The following code was taken from C# Station.
            //    //Author: Joe_Mayo
            command.Parameters.AddWithValue("@TestID", testID);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@StudentNumber", studentID);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the datareader

            while (dataReader.Read())   //Loops through each row
            {
                int markID = Convert.ToInt16(dataReader.GetValue(0)); //Inserts the question into the lists object
                int mark = Convert.ToInt16(dataReader.GetValue(1));
                marks.Add(markID);
                marks.Add(mark);
            }

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return marks;
        }

        public List<Questions> loadAnswers(int markID)
        {
            List<Questions> answers = new List<Questions>();   //Creates a list to store object answers for the test

            cnn.Open(); //Opens the connection string

            //Declares the query then sets it as a SqlCommand
            string sqlQuery = "SELECT QuestionID, StudentAnswer FROM StudentAnswer WHERE MarkID = @MarkID";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //    //The following code was taken from C# Station.
            //    //Author: Joe_Mayo
            command.Parameters.AddWithValue("@MarkID", markID);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the datareader

            int questionNum = 0;

            while (dataReader.Read())   //Loops through each row
            {
                questionNum += 1;
                int questionID = Convert.ToInt16(dataReader.GetValue(0)); //Inserts the question into the lists object
                int answer = Convert.ToInt16(dataReader.GetValue(1));
                answers.Add(new Questions(questionID, questionNum, answer));
            }

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return answers;
        }

        public List<TestResults> loadStudentResults(int testID)
        {
            List<TestResults> studentResults = new List<TestResults>();

            cnn.Open(); //Opens the connection string

            //Declares the query then sets it as a SqlCommand
            string sqlQuery = "Select M.StudentNumber, M.Mark, T.NumQuestions FROM MarkInfo M, TestInfo T WHERE T.TestID = M.TestID AND T.TestID = @TestID ORDER BY StudentNumber";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //    //The following code was taken from C# Station.
            //    //Author: Joe_Mayo
            command.Parameters.AddWithValue("@TestID", testID);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the datareader

            int studentNumber;
            int mark;
            int numQuestions;

            while (dataReader.Read())   //Loops through each row
            {
                studentNumber = Convert.ToInt16(dataReader.GetValue(0));
                mark = Convert.ToInt16(dataReader.GetValue(1)); //Inserts the question into the lists object
                numQuestions = Convert.ToInt16(dataReader.GetValue(2));
                studentResults.Add(new TestResults(studentNumber, mark, numQuestions));
            }

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return studentResults;
        }

        public List<TestResults> loadStudentInfo(List<string> studentNumbers)
        {
            List<TestResults> studentInfo = new List<TestResults>();
            foreach (string studentNumber in studentNumbers)
            {
                cnn.Open(); //Opens the connection string

                //Declares the query then sets it as a SqlCommand
                string sqlQuery = "SELECT S.FirstName, S.Surname, T.TestName, M.Mark, T.NumQuestions FROM MarkInfo M, StudentInfo S, TestInfo T WHERE S.StudentNumber = M.StudentNumber AND M.TestID = T.TestID AND S.StudentNumber = @StudentNumber";
                SqlCommand command = new SqlCommand(sqlQuery, cnn);

                int studNum = Convert.ToInt16(studentNumber);

                //________________________Code Attribution________________________
                //    //The following code was taken from C# Station.
                //    //Author: Joe_Mayo
                command.Parameters.AddWithValue("@StudentNumber", studNum);  //Replaces the parameter with the correct value
                //______________________________END______________________________

                SqlDataReader dataReader = command.ExecuteReader(); //Executes the datareader

                string firstName;
                string surname;
                string testName;
                int mark;
                int totalQuestions;

                while (dataReader.Read())   //Loops through each row
                {
                    firstName = dataReader.GetValue(0).ToString();
                    surname = dataReader.GetValue(1).ToString();
                    testName = dataReader.GetValue(2).ToString();
                    mark = Convert.ToInt16(dataReader.GetValue(3));
                    totalQuestions = Convert.ToInt16(dataReader.GetValue(4));

                    studentInfo.Add(new TestResults(studNum, firstName, surname, testName, mark, totalQuestions));
                }

                //Closes communications with the database
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }

            return studentInfo;
        }
    }
}
