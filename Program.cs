using System;
using System.Collections.Generic;
using Questions;

namespace Arithmetic_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            int LandingPageLoop = 1;
            int LandingPageLoopInput = 0;
            int MainLoop = 1;
            int MainQuestionSelectionLoopInput = 1;
            int NewExpressionLoop = 1;
            int QuestionNumber = 0;
            string MainLoopInput = "";
            string NewExpressionLoopInput = "";

            QuestionListInitialize QLI = new QuestionListInitialize();
            QLI.PopulateQuestionList();

            while (LandingPageLoop == 1)
            {
                Console.Clear();
                Console.WriteLine("\nSimple Arithmethic Calculator\n");
                Console.WriteLine("1. Calculate existing expressions");
                Console.WriteLine("2. Add new expression");
                Console.WriteLine("3. Exit program\n");
                Console.WriteLine("Choose an option");

                try
                {
                    LandingPageLoopInput = Int32.Parse(Console.ReadLine());

                    switch (LandingPageLoopInput)
                    {
                        case 1:
                            while (MainLoop == 1)
                            {
                                List<string> QuestionList = QLI.GetQuestionList();

                                Console.Clear();
                                Console.WriteLine("Calculate existing expressions\n");
                                Console.WriteLine("Expression List: ");
                                for (int i = 0; i < QuestionList.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + QuestionList[i]);
                                }
                                Console.WriteLine("\nSelect question number: ");
                                
                                while (MainQuestionSelectionLoopInput == 1)
                                {
                                    try
                                    {
                                        QuestionNumber = Int32.Parse(Console.ReadLine());
                                        if (QuestionNumber <= QuestionList.Count && QuestionNumber > 0)
                                        {
                                            MainQuestionSelectionLoopInput = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Input out of range");
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Input must be an integer");
                                    }
                                }

                                List<string> Output = Tokenizer(QuestionList[QuestionNumber - 1]);
                                Console.WriteLine("\nExpression chosen: " + QuestionList[QuestionNumber - 1]); 
                                Console.WriteLine("Expression result: " + Output[0] + "\n");

                                Console.WriteLine("Press Enter to continue");
                                MainLoopInput = Console.ReadLine();

                                MainQuestionSelectionLoopInput = 1;
                                QuestionNumber = 0;
                                break;
                            }
                            break;
                        case 2:
                            while (NewExpressionLoop == 1)
                            {
                                Console.Clear();
                                Console.WriteLine("Add new expression\n");
                                Console.WriteLine("Ensure that numbers, operators and brackets are all seperated by spaces as shown below");
                                Console.WriteLine("12.1 - ( 16 / 2 ) + ( ( 3 + 6 ) * 12 )\n"); 
                                Console.WriteLine("Input new expression");
                                QLI.AddToQuestionList(Console.ReadLine());

                                Console.WriteLine("\nPress Enter to continue");
                                NewExpressionLoopInput = Console.ReadLine();
                                break;
                            }
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Input must be an integer");
                }
            }
        }

        static List<string> Tokenizer(string PassedQuestion)
        {
            List<String> SelectedQuestion = new List<String>();
            string[] SelectedQuestionString = PassedQuestion.Split(" ");
            int Index = 1;
            int MultiplicationCount = 0;
            int DivisionCount = 0;

            foreach (string Element in SelectedQuestionString)
            {
                SelectedQuestion.Add(Element);
            }
            SelectedQuestion.Insert(0, "(");
            SelectedQuestion.Add(")");

            for (int x = 1; x < SelectedQuestion.Count; x++)
            {
                if (double.TryParse(SelectedQuestion[x], out _) && SelectedQuestion[x - 1] == "-")
                {
                    SelectedQuestion[x] = "-" + SelectedQuestion[x];
                    SelectedQuestion[x - 1] = "+";
                }
                if (double.TryParse(SelectedQuestion[x], out _) && SelectedQuestion[x - 1] == "*") 
                {
                    SelectedQuestion.Insert((x + 1), ")");
                    MultiplicationCount++;
                }
                if (double.TryParse(SelectedQuestion[x], out _) && SelectedQuestion[x - 1] == "/")
                {
                    SelectedQuestion.Insert((x + 1), ")");
                    DivisionCount++;
                }
            }

            if (MultiplicationCount > 0)
            {
                for (int x = 0; x <= MultiplicationCount; x++)
                {
                    SelectedQuestion.Insert(0, "(");
                }
            }
            if (DivisionCount > 0)
            {
                for (int x = 0; x <= DivisionCount; x++)
                {
                    SelectedQuestion.Insert(0, "(");
                }
            }

            Console.WriteLine("\nExpression Elements:"); 
            foreach (string x in SelectedQuestion)                      
            {                                          
                Console.Write(x + " ");                    
                Index++;                                                
            }  
            Console.WriteLine("\n");                                               

            return Calculation(SelectedQuestion);
        }

        static List<string> Calculation(List<string> SelectedQuestion)
        {
            List<string> OperandStack = new List<string>();
            List<string> OperatorStack = new List<string>();
            double SelectedQuestionOutput = 0;
            int CalculateMode = 0;
            int Iterations = 0;

            for (int Element = 0; Element < SelectedQuestion.Count; Element++)
            {
                Iterations ++;
                Console.WriteLine("\nIteration: " + Iterations);
                Console.WriteLine("Input: " + SelectedQuestion[Element]);

                if (SelectedQuestion[Element] == "+" || SelectedQuestion[Element] == "-" || SelectedQuestion[Element] == "*" || SelectedQuestion[Element] == "/" || SelectedQuestion[Element] == "(" || SelectedQuestion[Element] == ")" )
                {
                    OperatorStack.Add(SelectedQuestion[Element].ToString());                             
                    Console.WriteLine("Push to OperatorStack: " + SelectedQuestion[Element].ToString());
                    // Condition 2
                    if (SelectedQuestion[Element] == ")" && OperandStack.Count >= 2)                            
                    {
                        CalculateMode = 1;
                        OperatorStack.RemoveAt(OperatorStack.Count - 1);
                        Console.WriteLine("Pop from OperatorStack: " + SelectedQuestion[Element].ToString()); 
                    }
                }
                else
                {
                    OperandStack.Add(SelectedQuestion[Element].ToString());
                    Console.WriteLine("Push to OperandStack: " + SelectedQuestion[Element].ToString());
                }

                Console.WriteLine("OperandStack.Count: " + OperandStack.Count);
                Console.WriteLine("OperatorStack.Count: " + OperatorStack.Count);

                if (CalculateMode == 1)
                {
                    Console.WriteLine("\nCalculate Mode Initiated\n");

                    for (int Index = 1; Index <= OperatorStack.Count; Index++)
                    {
                        switch (OperatorStack[OperatorStack.Count - 1])
                        {
                            case "+":
                                SelectedQuestionOutput = Convert.ToDouble(OperandStack[OperandStack.Count - 2]) + Convert.ToDouble(OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Operation: " + OperandStack[OperandStack.Count - 2] + " " + OperatorStack[OperatorStack.Count - 1] + " " + OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Pop from OperatorStack: " + OperatorStack[OperatorStack.Count - 1]);
                                Console.WriteLine("Pop from OperandStack: " + OperandStack[OperandStack.Count - 2]);
                                Console.WriteLine("Pop from OperandStack: " + OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Operation Output: " + SelectedQuestionOutput);
                                Console.WriteLine("Push to OperandStack: " + SelectedQuestionOutput); 
                                OperandStack.RemoveAt(OperandStack.Count - 1);          
                                OperandStack.RemoveAt(OperandStack.Count - 1);          
                                OperatorStack.RemoveAt(OperatorStack.Count - 1);     
                                OperandStack.Add(SelectedQuestionOutput.ToString());    
                                break;
                            case "-":
                                SelectedQuestionOutput = Convert.ToDouble(OperandStack[OperandStack.Count - 2]) - Convert.ToDouble(OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Operation: " + OperandStack[OperandStack.Count - 2] + " " + OperatorStack[OperatorStack.Count - 1] + " " + OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Pop from OperatorStack: " + OperatorStack[OperatorStack.Count - 1]);
                                Console.WriteLine("Pop from OperandStack: " + OperandStack[OperandStack.Count - 2]);
                                Console.WriteLine("Pop from OperandStack: " + OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Operation Output: " + SelectedQuestionOutput);
                                Console.WriteLine("Push to OperandStack: " + SelectedQuestionOutput); 
                                OperandStack.RemoveAt(OperandStack.Count - 1);          
                                OperandStack.RemoveAt(OperandStack.Count - 1);          
                                OperatorStack.RemoveAt(OperatorStack.Count - 1);     
                                OperandStack.Add(SelectedQuestionOutput.ToString());
                                break;
                            case "/":
                                SelectedQuestionOutput = Convert.ToDouble(OperandStack[OperandStack.Count - 2]) / Convert.ToDouble(OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Operation: " + OperandStack[OperandStack.Count - 2] + " " + OperatorStack[OperatorStack.Count - 1] + " " + OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Pop from OperatorStack: " + OperatorStack[OperatorStack.Count - 1]);
                                Console.WriteLine("Pop from OperandStack: " + OperandStack[OperandStack.Count - 2]);
                                Console.WriteLine("Pop from OperandStack: " + OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Operation Output: " + SelectedQuestionOutput);
                                Console.WriteLine("Push to OperandStack: " + SelectedQuestionOutput); 
                                OperandStack.RemoveAt(OperandStack.Count - 1);          
                                OperandStack.RemoveAt(OperandStack.Count - 1);          
                                OperatorStack.RemoveAt(OperatorStack.Count - 1);     
                                OperandStack.Add(SelectedQuestionOutput.ToString());
                                break;
                            case "*":
                                SelectedQuestionOutput = Convert.ToDouble(OperandStack[OperandStack.Count - 2]) * Convert.ToDouble(OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Operation: " + OperandStack[OperandStack.Count - 2] + " " + OperatorStack[OperatorStack.Count - 1] + " " + OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Pop from OperatorStack: " + OperatorStack[OperatorStack.Count - 1]);
                                Console.WriteLine("Pop from OperandStack: " + OperandStack[OperandStack.Count - 2]);
                                Console.WriteLine("Pop from OperandStack: " + OperandStack[OperandStack.Count - 1]);
                                Console.WriteLine("Operation Output: " + SelectedQuestionOutput);
                                Console.WriteLine("Push to OperandStack: " + SelectedQuestionOutput); 
                                OperandStack.RemoveAt(OperandStack.Count - 1);          
                                OperandStack.RemoveAt(OperandStack.Count - 1);          
                                OperatorStack.RemoveAt(OperatorStack.Count - 1);     
                                OperandStack.Add(SelectedQuestionOutput.ToString());  
                                break;
                        }
                    }
                      
                    // Condition 1
                    if (OperandStack.Count < 2 || OperatorStack.Count == 0 || OperatorStack[OperatorStack.Count - 1] == "(")     
                    {
                        CalculateMode = 0;
                        Console.WriteLine("Pop from OperatorStack: " + OperatorStack[OperatorStack.Count - 1]);
                        OperatorStack.RemoveAt(OperatorStack.Count - 1);
                        Console.WriteLine("OperandStack.Count: " + OperandStack.Count);
                        Console.WriteLine("OperatorStack.Count: " + OperatorStack.Count);
                        Console.WriteLine("\nCalculate Mode Completed");
                    }
                }
            }

            return OperandStack;
        }
    }
}
