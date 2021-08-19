using MultipleChoiceTest.Object;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Database
{
    class StudentSetup : DatabaseCommunicator
    {
        public int getStudentNumber(string username)
        {
            int studentNumber = 0;    //Defaults the lecturers' number to 0

            cnn.Open(); //Opens connection string

            //Collects information from table UserDetails
            string sqlQuery = "SELECT StudentNumber FROM StudentInfo WHERE Username = @username";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@username", username);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())   //Loops through each row
            {
                studentNumber = Convert.ToInt16(dataReader.GetValue(0).ToString()); //Gets the collected lecturers' number
            }
            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return studentNumber;  //Returns the lecturers number
        }

        //gets the testname and its ID
        public List<string> getUnfinishedTestNames(int studentNumber)
        {
            List<string> tests = new List<string>();
            cnn.Open(); //Opens connection string

            //Collects information from table  TestInfo
            string sqlQuery = "SELECT T.TestID, T.TestName FROM TestInfo T, UserModule U WHERE U.StudentNumber = 1 AND T.ModuleID = U.ModuleID";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@StudentNumber", studentNumber);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the command

            string id;  //Variable for storing the ID
            string testName;    //Variable for storing the testname

            while (dataReader.Read())   //Loops through each row
            {
                id = dataReader.GetValue(0).ToString(); //Sets the ID
                testName = dataReader.GetValue(1).ToString();   //Sets the test name

                tests.Add(id + " -> " + testName);  //Adds them to a list
            }

            //Closes everything
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return tests;   //returns the list
        }

        //gets the testname and its ID
        public List<string> getFinishedTestNames(int studentNumber)
        {
            List<string> tests = new List<string>();
            cnn.Open(); //Opens connection string

            //Collects information from table  TestInfo
            string sqlQuery = "SELECT T.TestID, T.TestName FROM TestInfo T, MarkInfo M WHERE T.TestID = M.TestID AND M.StudentNumber = @StudentNumber";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@StudentNumber", studentNumber);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the command

            string id;  //Variable for storing the ID
            string testName;    //Variable for storing the testname

            while (dataReader.Read())   //Loops through each row
            {
                id = dataReader.GetValue(0).ToString(); //Sets the ID
                testName = dataReader.GetValue(1).ToString();   //Sets the test name

                tests.Add(id + " -> " + testName);  //Adds them to a list
            }

            //Closes everything
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return tests;   //returns the list
        }

        public List<string> loadModules(int studentNumber)
        {
            List<string> modules = new List<string>();

            cnn.Open(); //Opens connection string

            //Collects information from table  TestInfo
            string sqlQuery = "SELECT U.ModuleID, M.ModuleDescription FROM UserModule U, ModuleInfo M WHERE U.ModuleID = M.ModuleID AND U.StudentNumber = @StudentNumber";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@StudentNumber", studentNumber);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the command

            string id;  //Variable for storing the ID
            string moduletName;    //Variable for storing the testname

            while (dataReader.Read())   //Loops through each row
            {
                id = dataReader.GetValue(0).ToString(); //Sets the ID
                moduletName = dataReader.GetValue(1).ToString();   //Sets the test name

                modules.Add(id + " -> " + moduletName);  //Adds them to a list
            }

            //Closes everything
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return modules;
        }

        public List<TestResults> loadMarks(int studentNumber)
        {
            List<TestResults> studentMarks = new List<TestResults>();

            cnn.Open(); //Opens connection string

            //Collects information from table UserModule, ModuleInfo
            string sqlQuery = "SELECT T.ModuleID, T.TestName, M.Mark, T.NumQuestions FROM TestInfo T, MarkInfo M WHERE T.TestID = M.TestID AND M.StudentNumber = @StudentNumber";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@StudentNumber", studentNumber);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the command

            string moduleID;  //Variable for storing the module ID
            string testName;    //Variable for storing the testname
            int mark; //int for storing mark
            int total; //int for storing total marks of test

            while (dataReader.Read())   //Loops through each row
            {
                moduleID = dataReader.GetValue(0).ToString();
                testName = dataReader.GetValue(1).ToString();
                mark = Convert.ToInt16(dataReader.GetValue(2).ToString());
                total = Convert.ToInt16(dataReader.GetValue(3).ToString());

                studentMarks.Add(new TestResults(moduleID, testName, mark, total));
            }

            //Closes everything
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return studentMarks;
        }
    }
}
