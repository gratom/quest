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
    }
}