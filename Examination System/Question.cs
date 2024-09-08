using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
    public abstract class Question
    {
        public string Body { get; set; }
        public int Marks { get; set; }

        public string Header { get; set; }

        public Question(string body, int marks, string header)
        {
            Body = body;
            Marks = marks;
            Header = header;
        }

        public abstract void Displayquestion();

    }

    public class TrueFalseQuestion : Question
    {
        public string CorrectAnswer { get; set; }
        public TrueFalseQuestion(string body, int marks, string header, string correctAnswer) : base(body, marks, header)
        {
            CorrectAnswer = correctAnswer;

        }
        public override void Displayquestion()
        {
            Console.WriteLine($"{Header}\n{Body}\n True or False?");
        }
    }

    public class ChooseOneQuestion : Question
    {
        public string[] Options { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public ChooseOneQuestion(string body, int marks, string header, string[] options, int correctAnsindx)
            : base(body, marks, header)
        {
            Options = options;
            CorrectAnswerIndex = correctAnsindx;

        }
        public override void Displayquestion()
        {
            Console.WriteLine($"{Header}\n{Body}");
            for (int i = 0; i < Options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Options[i]}");
            }
        }
    }

    public class ChooseAllQuestion : Question
    {
        public string[] Options { get; set; }
        public string[] CorrectAnswerIndexes { get; set; }


        public ChooseAllQuestion(string body, int marks, string header, string[] options, string [] correctAns) :
            base(body, marks, header)
        {
            Options = options;
            CorrectAnswerIndexes = correctAns;
        }
        public override void Displayquestion() 
        {
            Console.WriteLine($"{Header}\n {Body} ");
            for (int i = 0;i < Options.Length; i++)
            {
                Console.WriteLine($"{i+1}. {Options[i]}");
            }
        }
    }
}

