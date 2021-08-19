using MultipleChoiceTest.Object;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Database
{
    class TestReading : DatabaseCommunicator
    {
        //Fetches the selected test for the Student user
        public List<Questions> getTest(int testID)    //Requests the testID
        {
            List<Questions> test = new List<Questions>();   //Creates a list to store object answers for the test

            cnn.Open(); //Opens the connection string

            //Declares the query then sets it as a SqlCommand
            string sqlQuery = "SELECT Question, Answer1, Answer2, Answer3, Answer4, AnswerCorrect, QuestionID  FROM QuestionInfo WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //    //The following code was taken from C# Station.
            //    //Author: Joe_Mayo
            command.Parameters.AddWithValue("@TestID", testID);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the datareader

            int questionNum = 0;

            while (dataReader.Read())   //Loops through each row
            {
                string question = dataReader.GetValue(0).ToString(); //Inserts the question into the lists object
                string Answer1 = dataReader.GetValue(1).ToString();
                string Answer2 = dataReader.GetValue(2).ToString();
                string Answer3 = dataReader.GetValue(3).ToString();
                string Answer4 = dataReader.GetValue(4).ToString();
                int correctAnswer = Convert.ToInt16(dataReader.GetValue(5).ToString());
                questionNum++;
                int questionID = Convert.ToInt16(dataReader.GetValue(6).ToString());
                test.Add(new Questions(question, Answer1, Answer2, Answer3, Answer4, correctAnswer, questionNum, questionID));


            }

            dataReader.Close();
            cnn.Close();

            return test;    //returns the list
        }

        public int saveTestResults(int mark, int studNum, int testID)
        {
            int newMarkID = createNewID("MarkID", "MarkInfo");

            cnn.Open(); //Opens connection string

            //Inserts a new row into the TestInfo table with the collected information.
            string query = "INSERT INTO MarkInfo (MarkID, Mark, StudentNumber, TestID) VALUES(@markID, @mark, @studentNumber, @testID)";
            SqlCommand command = new SqlCommand(query, cnn);    //Sets the command to a string

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@markID", newMarkID);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@mark", mark);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@studentNumber", studNum);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@testID", testID);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the insert command

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return newMarkID;
        }

        public int createNewID(string column, string table)
        {
            int markID = 0; //Defaults markID to 0

            cnn.Open(); //Opens connection string

            //Gets the largest QuestionID from QuestionInfo
            string sqlQuery = "SELECT MAX(" + column + ") FROM " + table + "";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@column", column);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@table", table);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())   //Loops through each row
            {
                try
                {
                    markID = Convert.ToInt16(dataReader.GetValue(0).ToString());    //Gets the testID
                }
                catch
                {
                    //Null was found. Use default value.
                }
            }

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return markID + 1;  //Returns the highest testID increased by one.
        }

        public void saveStudentAnswers(int markID, int questionID, int correctAnswer)
        {
            int newStudentAnswerID = createNewID("StudentAnswerID", "StudentAnswer");

            cnn.Open(); //Opens connection string

            //Inserts a new row into the TestInfo table with the collected information.
            string query = "INSERT INTO StudentAnswer (StudentAnswerID, MarkID, QuestionID, StudentAnswer) VALUES(@studentAnswerID, @markID, @questionID, @studentAnswer)";
            SqlCommand command = new SqlCommand(query, cnn);    //Sets the command to a string

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@studentAnswerID", newStudentAnswerID);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@markID", markID);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@questionID", questionID);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@studentAnswer", correctAnswer);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the insert command

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
    }
}
