using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Database
{
    class DLogin : DatabaseCommunicator
    {
        //Checks based on the username and password whether the user exists and whether they are a student or a lecturer
        public string signInCheck(string username, string password)
        {
            string testUsername, testPassword;  //Variables for testing
            Boolean type; //Variable for testing lecturer bit
            string path = "NoUser"; //Defaults the path to be taken to NoUser

            cnn.Open(); //Opens connection string

            //Collects information from table UserDetails
            string sqlQuery = "Select * from UserDetails";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())   //Loops through each row
            {
                testUsername = dataReader.GetValue(0).ToString();   //Sets username to test
                testPassword = dataReader.GetValue(1).ToString();   //Sets password to test
                type = Convert.ToBoolean(dataReader.GetValue(2));   //sets lecturer bit to test
                if (username.Equals(testUsername) && password.Equals(testPassword)) //tests if info correct
                {
                    if (type)
                    {
                        path = "Lecturer";  //Sets the path to lecturer
                    }
                    else
                    {
                        path = "Student";   //sets the path to student
                    }
                }
            }

            //Closes communications with the database
            dataReader.Close();
            command.Dispose();
            cnn.Close();

            return path;    //Returns which location user is to be sent to
        }
    }
}
