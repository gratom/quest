﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Managers
{
    using Global.Components;
    using Global.Managers.Datas;
    using System.Linq;

    public class QuestionManager : BaseManager
    {
        public override Type ManagerType => typeof(QuestionManager);

#pragma warning disable

        [SerializeField] private GameObject UIPanel;
        [SerializeField] private InputField inputID;
        [SerializeField] private InputField inputQuestion;
        [SerializeField] private InputField inputAnswer;
        [SerializeField] private InputField inputTags;
        [SerializeField] private InputField inputDifficult;

#pragma warning restore

        private Question currentQuestion;
        private List<Question> tempQuestions;

        protected override bool OnInit()
        {
            Clear();
            CloseUI();
            return true;
        }

        #region public functions

        public void OpenQuestions()
        {
            LoadQuestions();
            OpenUI();
        }

        public void SaveAndClose()
        {
            Services.GetManager<DataManager>().DynamicData.questions = tempQuestions;
            Clear();
            CloseUI();
        }

        public void OpenOrCreate()
        {
            if (int.TryParse(inputID.text, out int ID))
            {
                SelectQuestion(ID);
            }
            else
            {
                SelectQuestion(0);
            }
        }

        public void AddNewQuestion()
        {
            currentQuestion = new Question(tempQuestions.Max(x => x.ID) + 1);
            Clear();
            LoadInfoFromQuestion();
        }

        public void DeleteCurrentQuestion()
        {
            if (currentQuestion != null)
            {
                tempQuestions.Remove(currentQuestion);
            }
        }

        public void SaveCurrentQuestion()
        {
            currentQuestion.QuestionSubject = inputQuestion.text;
            currentQuestion.CorrectAnswer = inputAnswer.text;
            int.TryParse(inputDifficult.text, out currentQuestion.Difficult);
            currentQuestion.Tags = inputTags.text.Split(',').ToList();
            if (!tempQuestions.Contains(currentQuestion))
            {
                tempQuestions.Add(currentQuestion);
            }
        }

        #endregion public functions

        #region private functions

        private void Clear()
        {
            inputID.text = "";
            inputQuestion.text = "";
            inputAnswer.text = "";
            inputDifficult.text = "";
            inputTags.text = "";
        }

        private void SelectQuestion(int ID)
        {
            currentQuestion = tempQuestions.FirstOrDefault(x => x.ID == ID);
            if (currentQuestion == null)
            {
                AddNewQuestion(ID);
            }
            Clear();
            LoadInfoFromQuestion();
        }

        private void AddNewQuestion(int ID)
        {
            currentQuestion = new Question(ID);
        }

        private void LoadInfoFromQuestion()
        {
            inputID.text = currentQuestion.ID.ToString();
            inputQuestion.text = currentQuestion.QuestionSubject;
            inputAnswer.text = currentQuestion.CorrectAnswer;
            inputDifficult.text = currentQuestion.Difficult.ToString();
            currentQuestion.Tags.ForEach((x) => { inputTags.text += x + ","; });
        }

        private void OpenUI()
        {
            UIPanel.SetActive(true);
        }

        private void CloseUI()
        {
            UIPanel.SetActive(false);
        }

        private void LoadQuestions()
        {
            tempQuestions = Services.GetManager<DataManager>().DynamicData.questions;
        }

        #endregion private functions
    }
}