using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Database
{
    class AddNewUser : DatabaseCommunicator
    {
        public Boolean checkUsername(string username)
        {
            Boolean usernameFlag;

            cnn.Open(); //Opens the connection string

            //Declares the query then sets it as a SqlCommand
            string sqlQuery = "SELECT Username from UserDetails where username = @Username";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            //________________________Code Attribution________________________
            //    //The following code was taken from C# Station.
            //    //Author: Joe_Mayo
            command.Parameters.AddWithValue("@Username", username);  //Replaces the parameter with the correct value
            //______________________________END______________________________

            SqlDataReader dataReader = command.ExecuteReader(); //Executes the datareader

            if (dataReader.HasRows)
            {
                usernameFlag = false;
            }
            else
            {
                usernameFlag = true;
            }

            dataReader.Close();
            cnn.Close();

            return usernameFlag;
        }

        public void createUser(string username, string password, int positionBit)
        {
            cnn.Open();

            string query = "INSERT INTO UserDetails (Username, [Password], Lecturer) VALUES(@Username, @Password, @PositionBit);";   //Inserts a new User
            SqlCommand command = new SqlCommand(query, cnn);   //sets command to the string query

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@Username", username); //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@Password", password);    //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@PositionBit", positionBit);  //Replaces the parameter with the correct value
            //______________________________END______________________________question.CorrectAnswer

            SqlDataReader dataReader = command.ExecuteReader();   //Executes the insert command
            dataReader.Close(); //closes the data reader
            
            cnn.Close();
        }

        public int createStudent(string username, string firstName, string surname)
        {
            int studentNumber = createNewID("StudentNumber", "StudentInfo");

            cnn.Open();

            string query = "INSERT INTO StudentInfo (StudentNumber, Username, FirstName, Surname) VALUES(@StudentNumber, @Username, @FirstName, @Surname);";   //Inserts a new User
            SqlCommand command = new SqlCommand(query, cnn);   //sets command to the string query

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@StudentNumber", studentNumber); //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@Username", username); //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@FirstName", firstName);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@Surname", surname);  //Replaces the parameter with the correct value
            //______________________________END______________________________question.CorrectAnswer

            SqlDataReader dataReader = command.ExecuteReader();   //Executes the insert command
            dataReader.Close(); //closes the data reader

            cnn.Close();

            return studentNumber;
        }

        public int createLecturer(string username, string firstName, string surname)
        {
            int lecturerNumber = createNewID("LecturerNumber", "LecturerInfo");

            cnn.Open();

            string query = "INSERT INTO LecturerInfo (LecturerNumber, Username, FirstName, Surname) VALUES(@LecturerNumber, @Username, @FirstName, @Surname);";   //Inserts a new User
            SqlCommand command = new SqlCommand(query, cnn);   //sets command to the string query

            //________________________Code Attribution________________________
            //The following code was taken from C# Station.
            //Author: Joe_Mayo
            command.Parameters.AddWithValue("@LecturerNumber", lecturerNumber); //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@Username", username); //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@FirstName", firstName);  //Replaces the parameter with the correct value
            command.Parameters.AddWithValue("@Surname", surname);  //Replaces the parameter with the correct value
            //______________________________END______________________________question.CorrectAnswer

            SqlDataReader dataReader = command.ExecuteReader();   //Executes the insert command
            dataReader.Close(); //closes the data reader

            cnn.Close();

            return lecturerNumber;
        }

        public void linkUserModuleStudent(int studentNumber, List<string> modules)
        {
            int userModuleID;

            

            foreach (string module in modules)
            {
                userModuleID = createNewID("UserModuleID", "UserModule");

                cnn.Open();

                string query = "INSERT INTO UserModule (UserModuleID, StudentNumber, ModuleID) VALUES(@UserModuleID, @StudentNumber, @ModuleID);";   //Inserts a new User
                SqlCommand command = new SqlCommand(query, cnn);   //sets command to the string query

                //________________________Code Attribution________________________
                //The following code was taken from C# Station.
                //Author: Joe_Mayo
                command.Parameters.AddWithValue("@UserModuleID", userModuleID); //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@StudentNumber", studentNumber); //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@ModuleID", module);  //Replaces the parameter with the correct value
                                                                          //______________________________END______________________________question.CorrectAnswer

                SqlDataReader dataReader = command.ExecuteReader();   //Executes the insert command
                dataReader.Close(); //closes the data reader

                cnn.Close();
            }

            
        }

        public void linkUserModuleLecturer(int lecturerNumber, List<string> modules)
        {
            int userModuleID;

            foreach (string module in modules)
            {
                userModuleID = createNewID("UserModuleID", "UserModule");

                cnn.Open();

                string query = "INSERT INTO UserModule (UserModuleID, LecturerNumber, ModuleID) VALUES(@UserModuleID, @LecturerNumber, @ModuleID);";   //Inserts a new User
                SqlCommand command = new SqlCommand(query, cnn);   //sets command to the string query

                //________________________Code Attribution________________________
                //The following code was taken from C# Station.
                //Author: Joe_Mayo
                command.Parameters.AddWithValue("@UserModuleID", userModuleID); //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@LecturerNumber", lecturerNumber); //Replaces the parameter with the correct value
                command.Parameters.AddWithValue("@ModuleID", module);  //Replaces the parameter with the correct value
                                                                       //______________________________END______________________________question.CorrectAnswer

                SqlDataReader dataReader = command.ExecuteReader();   //Executes the insert command
                dataReader.Close(); //closes the data reader

                cnn.Close();
            }
        }

        public int createNewID(string ID, string table)
        {
            int newID = 0; //Defaults the questionID to 0.

            cnn.Open();

            //Gets the largest ID from the table
            string sqlQuery = "SELECT MAX(" + ID + ") FROM " + table;
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())   //Loops through each row
            {
                try
                {
                    newID = Convert.ToInt16(dataReader.GetValue(0).ToString());    //Gets the questionID
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

            return newID + 1;  //Returns the highest questionID increased by one.
        }

        public List<string> getModules()
        {
            List<string> modules = new List<string>();


            cnn.Open(); //Opens connection string

            //Collects information from table  TestInfo
            string sqlQuery = "Select ModuleID FROM ModuleInfo";
            SqlCommand command = new SqlCommand(sqlQuery, cnn);

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
    }
}
