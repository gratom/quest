using System;

namespace Global.Components
{
    [Serializable]
    public class Question
    {
        public string QuestionSubject;
        public string CorrectAnswer;
        public string[] Tags;
        public int Difficult;
    }
}