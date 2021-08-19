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
    /// Interaction logic for ViewMarks.xaml
    /// </summary>
    public partial class ViewMarks : Window
    {

        /*
         *      Initialize
         */

        public ViewMarks(HomePageS BackPage, int studNum)
        {
            InitializeComponent();

            this.BackPage = BackPage;
            studentNumber = studNum;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadList();
            loadMarks();
        }

        /*
         *      Global Variables
         */

        HomePageS BackPage;
        int studentNumber;
        List<TestResults> marks;

        /*
         *      Helper Methods
         */

        public void loadList()
        {
            StudentSetup getModules = new StudentSetup();
            List<string> modules = getModules.loadModules(studentNumber);

            foreach (string module in modules)
            {
                lstModules.Items.Add(module);
            }
        }

        public void loadMarks()
        {
            StudentSetup getMarks = new StudentSetup();
            marks = getMarks.loadMarks(studentNumber);
        }

        public void displayMarks(string moduleID, string moduleName)
        {
            try
            {
                txtMarks.Text = moduleName + "\n"
                + "================================================" + "\n \n";
                foreach (TestResults mark in marks)
                {
                    if (mark.ModuleID.Equals(moduleID))
                    {
                        txtMarks.Text += mark.TestName + "\t \t" + mark.Mark + "/" + mark.TestTotal + "\n";
                    }
                }
            }
            catch (Exception)
            {
                //Catch in the event of an error, no marks yet to display
            }
            
        }

        /*
         *      Events
         */

        private void Window_Closed(object sender, EventArgs e)
        {
            BackPage.Show();
        }

        private void LstModules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string moduleID = lstModules.SelectedItem.ToString();
            string[] moduleSplit = moduleID.Split('-');    //Splits the item into ID and Test
            moduleID = moduleSplit[0].Trim();
            string moduleName = moduleSplit[1];
            displayMarks(moduleID, moduleName);
        }
    }
}
