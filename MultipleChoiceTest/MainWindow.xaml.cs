using MultipleChoiceTest.Database;
using MultipleChoiceTest.Lecturer;
using MultipleChoiceTest.Register;
using MultipleChoiceTest.Student;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultipleChoiceTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /*
         *      Initialize
         */

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            DLogin signInTest = new DLogin(); //Opens a link to the DatabaseLogin class
            string path = signInTest.signInCheck(txtUsername.Text, txtPassword.Text);   //Tests the username and password

            switch (path)   //Finds which path the user will go down
            {
                case "NoUser":  //The no user path will return if the user does not exist. 
                    MessageBox.Show("Login Failed. \n Username or Password is incorrect. Please try again.", "Login:"); //Displays an error message
                    break;

                case "Lecturer":    //The lecturer path will send the user to the lecturer half of the application
                    LecturerSetup getIDL = new LecturerSetup();
                    int lecturerNumber = getIDL.getLecturerNumber(txtUsername.Text); //Gets the lecturer number to keep track of who's logged in.
                    MessageBox.Show("Login Success: Lecturer", "Login:");   //Shows the login confirmation message

                    HomePageL LecturerSignIn = new HomePageL(lecturerNumber, this);   //Creates a link to the lecturers' page.
                    LecturerSignIn.Show();  //Shows the lecturers' page
                    Hide(); //Hides the current page
                    txtUsername.Text = "";  //Sets the login page back to default 
                    txtPassword.Text = "";
                    break;

                case "Student": //The student path will send the user to the users' half of the application
                    StudentSetup getIDS = new StudentSetup();
                    int studentNumber = getIDS.getStudentNumber(txtUsername.Text); //Gets the lecturer number to keep track of who's logged in.
                    MessageBox.Show("Login Success: Student", "Login:");   //Shows the login confirmation message

                    HomePageS StudentSignIn = new HomePageS(studentNumber, this);   //Creates a link to the students' page.
                    StudentSignIn.Show();  //Shows the students' page
                    Hide(); //Hides the current page
                    txtUsername.Text = "";  //Sets the login page back to default 
                    txtPassword.Text = "";
                    break;
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            UserDetails newUser = new UserDetails(this);
            newUser.Show();
        }
    }
}
