using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Object
{
    class TestResults
    {
        private string moduleID;
        private string testName;
        private int mark;
        private int testTotal;

        public string ModuleID { get => moduleID; set => moduleID = value; }
        public string TestName { get => testName; set => testName = value; }
        public int Mark { get => mark; set => mark = value; }
        public int TestTotal { get => testTotal; set => testTotal = value; }
        public int StudentID { get => studentID; set => studentID = value; }
        public string StudentName { get => studentName; set => studentName = value; }
        public string StudentSurname { get => studentSurname; set => studentSurname = value; }

        public TestResults(string moduleID, string testName, int mark, int testTotal)
        {
            ModuleID = moduleID;
            TestName = testName;
            Mark = mark;
            TestTotal = testTotal;
        }

        private int studentID;

        public TestResults(int studentID, int mark, int testTotal)
        {
            StudentID = studentID;
            Mark = mark;
            TestTotal = testTotal;
        }

        private string studentName;
        private string studentSurname;

        public TestResults(int studentID, string studentName, string studentSurname, string testName, int mark, int testTotal)
        {
            StudentID = studentID;
            StudentName = studentName;
            TestName = testName;
            StudentSurname = studentSurname;
            Mark = mark;
            TestTotal = testTotal;
        }
    }
}
