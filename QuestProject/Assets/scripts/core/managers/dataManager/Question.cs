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

        public HashSet<string> HashTags => hashTags ?? ReinitHashTags();
        [SerializeField] private int id;

        private HashSet<string> hashTags;

        public Question(int id)
        {
            this.id = id;
        }

        #region public functions

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

        #endregion public functions

        #region private functions

        private HashSet<string> ReinitHashTags()
        {
            HashSet<string> returnValue = new HashSet<string>();
            foreach (string item in Tags)
            {
                returnValue.Add(item);
            }
            hashTags = returnValue;
            return returnValue;
        }

        #endregion private functions
    }
}