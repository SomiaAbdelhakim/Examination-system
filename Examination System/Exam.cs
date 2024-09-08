using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examination_System
{
    public abstract class Exam
    {
        public int Time { get; set; }
        public int NumberofQ { get; set; }
        public int TotalMarks { get; set; }
        public int UserMarks { get; set; }
        public List<Question> QuestionList { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime TimeOut { get; set; }



        public Exam(int time, int numberofQ)
        {
            Time = time;
            NumberofQ = numberofQ;
            QuestionList = new List<Question>();
            TotalMarks = 0;
            //UserMarks = 0;
        }

        public void AddQuestion(Question question)
        {
            if (QuestionList.Count < NumberofQ)
            {
                QuestionList.Add(question);
            }
            else
            {
                Console.WriteLine("Cannot Add more Questions!");
            }
        }
        public abstract void ShowExam();

        public void StartExam()
        {
            StartTime = DateTime.Now;
            TimeOut = StartTime.AddSeconds(Time);
            Console.WriteLine($"Exam started at {StartTime}");
        }
        public void EndExam()
        {
            EndTime = StartTime.AddSeconds(Time);
            Console.WriteLine($"Exam ended at {EndTime}\n");
        }
        public void ExamTimeOut()
        {

            if (TimeOut == DateTime.Now)
            {
                Console.WriteLine($"Exam ended at {TimeOut}");
            }
        }
    }

    public class PracticeExam : Exam
    {
        public PracticeExam(int time, int numberofQ) : base(time, numberofQ)
        {

        }
        public override void ShowExam() 
        {
            int score = 0;
            foreach (Question question in QuestionList)
            {
                StartExam();
                EndExam();
                int Choose1_input;
                bool check;
                //bool All_check =true;
                int all_count = 0;
                string[] chooseAll_ans;
                string input;
                string tfAns;
                int correct_count = 0;
                

                question.Displayquestion();
                //int TotalScores = NumberofQ;
                if (question is TrueFalseQuestion tfQuestion)
                {
                    do
                    {
                        Console.WriteLine("Your answer should be true or false: ");
                        tfAns = Console.ReadLine().ToLower();
                    } while (tfAns != "true" && tfAns != "false");
                    if (tfAns == tfQuestion.CorrectAnswer.ToString())
                    {
                        score += 1;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your Answer is correct");
                        Console.ResetColor();


                    }
                    else { 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your Answer is Wrong!"); }
                        Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Correct Answer: {tfQuestion.CorrectAnswer}\n");
                    Console.ResetColor();
                }


                else if (question is ChooseOneQuestion chooseOneQ)
                {
                    do
                    {
                        Console.WriteLine("Choose one from 1 to 4 : ");
                        check = int.TryParse(Console.ReadLine(), out Choose1_input);
                    } while (!check || Choose1_input > chooseOneQ.Options.Length);
                    //Choose1_input = int.Parse(Console.ReadLine());
                    if (Choose1_input == (chooseOneQ.CorrectAnswerIndex + 1))
                    {
                        score += 1;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your Answer is correct");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your Answer is Wrong!");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Correct Answer: {chooseOneQ.CorrectAnswerIndex + 1}\n");
                    Console.ResetColor();
                }

                else if (question is ChooseAllQuestion chooseAllQ)
                {
                    do
                    {
                        all_count = 0;
                        Console.WriteLine("Choose multiple choices from 1 to 4 :");
                        chooseAll_ans = Console.ReadLine().Split(" ");

                        for (int i = 0; i < chooseAll_ans.Length; i++)
                        {
                            if (int.TryParse(chooseAll_ans[i], out int number) && number > 4)
                            {
                                all_count +=1;
                            }
                        }
                    } while (all_count >0);

                    for (int i = 0; i < chooseAll_ans.Length; i++)
                    {
                        if (chooseAll_ans[i] == chooseAllQ.CorrectAnswerIndexes[i])
                        {
                            correct_count++;
                        }
                    }
                    if (correct_count == chooseAllQ.CorrectAnswerIndexes.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your Answer is Correct");
                        Console.ResetColor();
                        score += 1;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your Answer is Wrong!");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Correct Answer: ");
                    foreach (string index in chooseAllQ.CorrectAnswerIndexes)
                    {
                        Console.Write($"{index} ");
                      

                    }
                    Console.ResetColor ();
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------------------");
                }

                
            }

            Console.WriteLine($"Your total Score is {score} out of {NumberofQ}");
            Console.WriteLine();
        }
    }

    public class FinalExam : Exam
    {
        int grade = 0;
        public FinalExam(int time, int numberofQ) : base(time, numberofQ)
        {

        }
        public override void ShowExam()
        {
            foreach (Question question in QuestionList)
            {
                StartExam();
                EndExam();

                int Choose1_input;
                bool check;
                bool All_check = true;
                int all_count = 0;
                string[] chooseAll_ans;
                string input;
                string tfAns;
                
                int Allcorrect_counter = 0;

                question.Displayquestion();
                TotalMarks += question.Marks;

                ExamTimeOut();
                if (question is TrueFalseQuestion tfQuestion)
                {
                    do
                    {
                        Console.WriteLine("Your answer should be true or false: ");
                        tfAns = Console.ReadLine().ToLower();
                    } while (tfAns != "true" && tfAns != "false");
                    if (tfAns == tfQuestion.CorrectAnswer.ToString())
                    {
                        grade += tfQuestion.Marks;
                    }
                }


                else if (question is ChooseOneQuestion chooseOneQ)
                {
                    do
                    {
                        Console.WriteLine("Choose one from 1 to 4 : ");
                        check = int.TryParse(Console.ReadLine(), out Choose1_input);
                    } while (!check || Choose1_input > chooseOneQ.Options.Length);
                    //Choose1_input = int.Parse(Console.ReadLine());
                    if (Choose1_input == (chooseOneQ.CorrectAnswerIndex + 1))
                    {
                        grade += chooseOneQ.Marks;
                    }
                }

                else if (question is ChooseAllQuestion chooseAllQ)
                {
                    do
                    {
                        all_count = 0;
                        Console.WriteLine("Choose multiple choices from 1 to 4 :");
                        chooseAll_ans = Console.ReadLine().Split(" ");

                        for (int i = 0; i < chooseAll_ans.Length; i++)
                        {
                            if (int.TryParse(chooseAll_ans[i], out int number) && number > 4)
                            {
                                all_count += 1;
                            }
                        }
                    } while (all_count > 0);

                    for (int i = 0; i < chooseAll_ans.Length; i++)
                    {
                        if (chooseAll_ans[i] == chooseAllQ.CorrectAnswerIndexes[i])
                        {
                            Allcorrect_counter++;
                        }
                    }
                    if (Allcorrect_counter == chooseAllQ.CorrectAnswerIndexes.Length)
                    {
                        grade += chooseAllQ.Marks;
                    }

                }
                Console.WriteLine("\n---------------------------------------------------------"); 
            }
            Console.WriteLine($"Your total Grade is {grade} out of {TotalMarks}");
        }
    }
}


//string result = "";
//foreach (string ans in caQuestion.CorrectAnswerIndexes)
////for (int i = 0;i<caQuestion.CorrectAnswerIndexes.Length;i++ )
//{ 
//    result += string.Join(", ",ans);
//    //return string.Join(", ", ans]);
//    //Console.Write($"{string.Join(",",ans)}");
//}
//return result+" ,";
////return string.Join(", ", caQuestion.Options.Where((option, index) => caQuestion.CorrectAnswerIndexes[index]));
///

//foreach (string inp in chooseAll_ans) {

//    foreach (string index in chooseAllQ.CorrectAnswerIndexes)
//    {
//        if(inp == index) 
//        {
//            Console.WriteLine("Your answer is correct!");
//        }
//        else{ Console.WriteLine("Wrong Answer"); }
//    }
//}

//while (chooseAll_ans[j] != "1" && chooseAll_ans[j] != "2" && chooseAll_ans[j] != "3" && chooseAll_ans[j] != "4");
//while (All_check>0)
//{
//    All_check = 0;
//    foreach (string ans in chooseAll_ans)
//    {
//        if (ans != "1" && ans != "2" && ans != "3" && ans != "4")
//        {
//            All_check +=1;
//        }

////    }

//    Console.WriteLine("Choose multiple choices from 1 to 4 :");
//    chooseAll_ans = Console.ReadLine().Split(" ");
//}


//private string GetCorrectAnswer(Question question)
//{
//    if (question is TrueFalseQuestion tfQuestion)
//    {
//        return tfQuestion.CorrectAnswer.ToString();
//    }
//    else if (question is ChooseOneQuestion cOneQ)
//    {
//        return (cOneQ.CorrectAnswerIndex+1).ToString();
//        //return cOneQ.CorrectAnswerIndex[];

//    }
//    else if (question is ChooseAllQuestion caQuestion)
//    {
//        foreach (var index in caQuestion.CorrectAnswerIndexes)
//        {

//            Console.WriteLine(index+1);
//        }
//    }
//    return string.Empty;
//}