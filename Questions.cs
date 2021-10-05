using System;
using System.Collections.Generic;

namespace Questions
{
    class QuestionListInitialize
    {
        private List<String> QuestionList = new List<String>();

        public void PopulateQuestionList()
        {
            QuestionList.Add("1 + 1");                          // 1. Output = 2
            QuestionList.Add("2 * 2");                          // 2. Output = 4
            QuestionList.Add("1 + 2 + 3");                      // 3. Output = 6
            QuestionList.Add("6 / 2");                          // 4. Output = 3
            QuestionList.Add("11 + 23");                        // 5. Output = 34
            QuestionList.Add("11.1 + 23");                      // 6. Output = 34.1
            QuestionList.Add("1 + 1 * 3");                      // 7. Output = 4
            QuestionList.Add("( 11.5 + 15.4 ) + 10.1");         // 8. Output = 37
            QuestionList.Add("23 - ( 29.3 - 12.5 )");           // 9. Output = 6.2
            QuestionList.Add("( 1 / 2 ) - 1 + 1");              // 10. Output = 0.5
            QuestionList.Add("10 - ( 2 + 3 * ( 7 - 5 ) )");     // 11. Output = 2
        }

        public List<String> GetQuestionList()
        {
            return QuestionList;
        }

        public void ClearQuestionList()
        {
            QuestionList.Clear();
        }

        public void AddToQuestionList(string NewExpression)
        {
            QuestionList.Add(NewExpression);
        }
    }
}