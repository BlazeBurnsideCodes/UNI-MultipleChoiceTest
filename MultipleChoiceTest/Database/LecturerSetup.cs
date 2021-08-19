using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Database
{
    class LecturerSetup : DatabaseCommunicator
    {
        //Finds the lecturer's number based on the inputed lecturers' username.
        public int getLecturerNumber(string username)
        {
            int lecturerNumber = 0;    //Defaults the lecturers' number to 0

            cnn.Open(); //Opens connection string

            //Collects information from table UserDetails
            string sqlQuery = "SELECT LecturerNumber FROM LecturerInfo WHERE Username = @username";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@username", username);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())   //Loops through each row
            {
                lecturerNumber = Convert.ToInt16(dataReader.GetValue(0).ToString()); //Gets the collected lecturers' number
            }
            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return lecturerNumber;  //Returns the lecturers number
        }

        //gets the testname and its ID
        public List<string> getTestNames(int lecturerNumber)
        {
            List<string> tests = new List<string>();
            cnn.Open(); //Opens connection string

            //Collects information from table  TestInfo
            string sqlQuery = "SELECT TestID, TestName FROM TestInfo WHERE LecturerNumber = @LecturerNumber";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@LecturerNumber", lecturerNumber);  //Replaces the parameter with the correct value
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

        public List<string> getModules(int entryNumber, string entryType)
        {
            List<string> modules = new List<string>();


            cnn.Open(); //Opens connection string

            //Collects information from table  TestInfo
            string sqlQuery = "SELECT ModuleID  FROM UserModule WHERE LecturerNumber = @entryNumber";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@entryNumber", entryNumber);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the command

            while (dataReader.Read())   //Loops through each row
            {
                modules.Add(dataReader.GetValue(0).ToString());  //Adds them to a list
            }

            //Closes everything
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return modules;
        }

        public string getTestModule(int testID)
        {
            string moduleCode = "";

            cnn.Open(); //Opens connection string

            //Collects information from table  TestInfo
            string sqlQuery = "SELECT ModuleID FROM TestInfo WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@TestID", testID);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the command

            while (dataReader.Read())   //Loops through each row
            {
                moduleCode = dataReader.GetValue(0).ToString();  //Adds them to a list
            }

            //Closes everything
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return moduleCode;
        }

        public List<string> getStudents(int lecturerNumber)
        {
            List<string> studentNumbers = new List<string>();

            cnn.Open(); //Opens the connection string

            //Declares the query then sets it as a SqlCommand
            string sqlQuery = "SELECT U1.StudentNumber "
                + "FROM UserModule U1 "
                + "LEFT JOIN UserModule U2 ON U1.ModuleID = U2.ModuleID "
                + "WHERE U1.StudentNumber IS NOT NULL AND U2.StudentNumber IS NULL "
                + "AND U2.LecturerNumber = @LecturerNumber "
                + "GROUP BY U1.StudentNumber "
                + "ORDER BY U1.StudentNumber";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //    //The following code was taken from C# Station.
            //    //Author: Joe_Mayo
            command.Parameters.AddWithValue("@LecturerNumber", lecturerNumber);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the datareader

            while (dataReader.Read())   //Loops through each row
            {
                studentNumbers.Add(dataReader.GetValue(0).ToString());
            }

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return studentNumbers;
        }
    }
}
