using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Components
{
    [Serializable]
    public class Question
    {
        public int ID => id;
        public string QuestionSubject;
        public string CorrectAnswer;
        public List<string> Tags = new List<string>();
        public int Difficult;

        [SerializeField] private int id;

        public Question(int id)
        {
            this.id = id;
        }

        public Question Copy()
        {
            Question returnedQuestion = new Question(id)
            {
                QuestionSubject = QuestionSubject,
                CorrectAnswer = CorrectAnswer,
                Difficult = Difficult,
                Tags = new List<string>()
            };
            for (int i = 0; i < Tags.Count; i++)
            {
                returnedQuestion.Tags.Add(Tags[i]);
            }
            return returnedQuestion;
        }
    }
}