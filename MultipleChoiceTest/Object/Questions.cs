using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceTest.Object
{
    class Questions
    {
        //Variables for the question object
        private int questionNum;
        private int questionID;
        private string question;
        private string answer1;
        private string answer2;
        private string answer3;
        private string answer4;
        private int correctAnswer;

        //Get and set methods for the variables for the question object
        public int QuestionNum { get => questionNum; set => questionNum = value; }
        public int QuestionID { get => questionID; set => questionID = value; }
        public string Question { get => question; set => question = value; }
        public string Answer1 { get => answer1; set => answer1 = value; }
        public string Answer2 { get => answer2; set => answer2 = value; }
        public string Answer3 { get => answer3; set => answer3 = value; }
        public string Answer4 { get => answer4; set => answer4 = value; }
        public int CorrectAnswer { get => correctAnswer; set => correctAnswer = value; }


        public Questions(string newQuestion, string newAnswer1, string newAnswer2, string newAnswer3, string newAnswer4, int newCorrectAnswer, int questionNum, int questionID)
        {
            Question = newQuestion;
            Answer1 = newAnswer1;
            Answer2 = newAnswer2;
            Answer3 = newAnswer3;
            Answer4 = newAnswer4;
            CorrectAnswer = newCorrectAnswer;
            QuestionNum = questionNum;
            QuestionID = questionID;
        }

        public Questions(int questionID, int questionNum, int studentAnswer)
        {
            QuestionID = questionID;
            QuestionNum = questionNum;
            CorrectAnswer = studentAnswer;
        }

        //A method to set inputed values into the variables
        public Questions(string newQuestion, string newAnswer1, string newAnswer2, string newAnswer3, string newAnswer4, int newCorrectAnswer)
        {
            Question = newQuestion;
            Answer1 = newAnswer1;
            Answer2 = newAnswer2;
            Answer3 = newAnswer3;
            Answer4 = newAnswer4;
            CorrectAnswer = newCorrectAnswer;
        }

        public static bool operator ==(Questions q1, Questions q2)
        {
            return (q1.CorrectAnswer == q2.CorrectAnswer);
        }
        public static bool operator !=(Questions q1, Questions q2)
        {
            return (q1.CorrectAnswer != q2.CorrectAnswer);
        }
    }
}
