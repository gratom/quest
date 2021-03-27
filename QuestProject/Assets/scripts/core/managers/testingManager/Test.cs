using System;
using System.Collections.Generic;

namespace Global.Components.Testing
{
    [Serializable]
    public class Test
    {
        public string name;
        public List<Answer> answers;
    }

    [Serializable]
    public class Answer
    {
        public Question question;
        public int grade;

        public Answer(Question question, int grade)
        {
            this.question = question;
            this.grade = grade;
        }
    }
}