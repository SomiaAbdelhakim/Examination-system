namespace Examination_System
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TrueFalseQuestion tfQuestion = new TrueFalseQuestion("Is the sky blue?", 5, "Question 1", "true");

            ChooseOneQuestion coQuestion = new ChooseOneQuestion("What is the capital of France?", 10, "Question 2", new string[] { "Paris", "London", "Berlin", "Madrid" }, 0);
            ChooseAllQuestion caQuestion = new ChooseAllQuestion("Select all blue shades.", 15, "Question 3", new string[] { "blue", "red", "babyblue", "drak blue" }, new string[] { "1","3","4" });

            // Create exams
            PracticeExam practiceExam = new PracticeExam(60, 3);
            //FinalExam finalExam = new FinalExam(60, 3);

            // Add questions to exams
            practiceExam.AddQuestion(tfQuestion);
            practiceExam.AddQuestion(coQuestion);
            practiceExam.AddQuestion(caQuestion);


            FinalExam finalExam = new FinalExam(60, 3);
            finalExam.AddQuestion(tfQuestion);
            finalExam.AddQuestion(coQuestion);
            finalExam.AddQuestion(caQuestion);

            int user_input;
            bool check;
            do
            {
                Console.WriteLine("1. Practice Exam \n2.Final Exam\n click any number to Exit");
                check = int.TryParse(Console.ReadLine(), out user_input);
            } while (!check);
            //if (!check)
            //    {
            //        Console.WriteLine("Please enter valid numbers! ");
            //    }
            //    else
            //    {
                    //switch (user_input) {
                    //    case 1:

                    //        Console.WriteLine("Practice Exam:");
                    //        practiceExam.ShowExam();
                    //        break;
                    //    case 2:
                    //        Console.WriteLine("Final Exam:");
                    //        finalExam.ShowExam(); 
                    //        break;
                    //    case 3:
                    //        break;
                    //}
                    if (user_input == 1)
                    {
                        Console.WriteLine("Practice Exam:");
                        practiceExam.ShowExam();
                    }
                    else if (user_input == 2)
                    {
                        Console.WriteLine("Final Exam:");
                        finalExam.ShowExam();
                    }

            //    }
            //}while (user_input !=1 || user_input!=2 || user_input ==3);
        }
    }
}
