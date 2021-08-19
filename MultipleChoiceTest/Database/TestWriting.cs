using MultipleChoiceTest.Object;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Database
{
    class TestWriting : DatabaseCommunicator
    {
        //Adds all the collected information to the database
        public void createNewTest(string testName, List<Questions> newTest, int lecturerNumber, string module)
        {
            cnn.Open(); //Opens connection string

            int testID = createNewTestID(); //Calls the createNewTestID method to get a new test id
            string query;   //Creates a string for the query

            //Inserts a new row into the TestInfo table with the collected information.
            query = "INSERT INTO TestInfo (TestID, TestName, NumQuestions, LecturerNumber, ModuleID) VALUES(@testID, @testName, @numQuestion, @lecturerNumber, @module)";
            SqlCommand command = new SqlCommand(query, cnn);    //Sets the command to a string

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@testID", testID);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@testName", testName); //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@numQuestion", newTest.Count); //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@lecturerNumber", lecturerNumber); //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@module", module); //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the insert command

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            addQuestions(testID, newTest);
        }

        //Add questions
        public void addQuestions(int testID, List<Questions> newTest)
        {
            int questionID; //Creates an integer for the question ID
            string query;   //Creates a string for the query
            SqlCommand command;
            SqlDataReader dataReader;

            cnn.Open();

            foreach (Questions question in newTest) //Loops through for each question in the newTest list
            {
                questionID = createNewQuestionID(); //Calls a method to get a new question ID

                query = "INSERT INTO QuestionInfo (QuestionID, Question, TestID, Answer1, Answer2, Answer3, Answer4, AnswerCorrect) VALUES(@questionID, @question, @testID, @Answer1, @Answer2, @Answer3, @Answer4, @CorrectAnswer); ";   //Inserts a new question
                command = new SqlCommand(query, cnn);   //sets command to the string query

                //________________________Code Attribution________________________
                //The following code was taken from C# Station.
                //Author: Joe_Mayo
                command.Parameters.AddWithValue("@questionID", questionID); //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@question", question.Question);    //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@testID", testID);  //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@Answer1", question.Answer1);  //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@Answer2", question.Answer2);  //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@Answer3", question.Answer3);  //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@Answer4", question.Answer4);  //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@CorrectAnswer", question.CorrectAnswer);  //Replaces the parameter with the correct value
                //______________________________END______________________________question.CorrectAnswer

                dataReader = command.ExecuteReader();   //Executes the insert command
                dataReader.Close(); //closes the data reader
            }

            cnn.Close();
        }

        //Create a new TestID
        public int createNewTestID()
        {
            int testID = 0; //Defaults testID to 0

            //Gets the largest QuestionID from QuestionInfo
            string sqlQuery = "SELECT MAX(TestID) FROM TestInfo";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())   //Loops through each row
            {
                try
                {
                    testID = Convert.ToInt16(dataReader.GetValue(0).ToString());    //Gets the testID
                }
                catch
                {
                    //Null was found. Use default value.
                }
            }

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();

            return testID + 1;  //Returns the highest testID increased by one.
        }

        //Creates a new questionID
        public int createNewQuestionID()
        {
            int questionID = 0; //Defaults the questionID to 0.

            //Gets the largest QuestionID from QuestionInfo
            string sqlQuery = "SELECT MAX(QuestionID) FROM QuestionInfo";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())   //Loops through each row
            {
                try
                {
                    questionID = Convert.ToInt16(dataReader.GetValue(0).ToString());    //Gets the questionID
                }
                catch
                {
                    //Null was found. Use default value.
                }
            }

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();

            return questionID + 1;  //Returns the highest questionID increased by one.
        }

        public void updateTest(int testID, int newNumQuestion)
        {
            cnn.Open(); //Opens connection string

            string query;   //Creates a string for the query

            //Inserts a new row into the TestInfo table with the collected information.
            query = "UPDATE TestInfo SET NumQuestions = @NewNumQuestion WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, cnn);    //Sets the command to a string

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@TestID", testID);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@NewNumQuestion", newNumQuestion); //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the insert command

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        public void removeOldQuestions(int testID)
        {
            cnn.Open(); //Opens connection string

            string query;   //Creates a string for the query

            //Inserts a new row into the TestInfo table with the collected information.
            query = "DELETE FROM QuestionInfo WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, cnn);    //Sets the command to a string

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@TestID", testID);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the insert command

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
    }
}
