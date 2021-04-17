using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers
{
    using Global.Components;
    using Global.Components.Testing;
    using Global.Managers.Datas;
    using UnityEngine.UI;

    public class TestingManager : BaseManager
    {
        public override Type ManagerType => typeof(TestingManager);

#pragma warning disable

        #region UI blocks

        [SerializeField] private GameObject UIPanel;
        [SerializeField] private GameObject StartPanel;
        [SerializeField] private GameObject ProcessPanel;
        [SerializeField] private GameObject FinishPanel;

        #endregion UI blocks

        [SerializeField] private InputField inputName;
        [SerializeField] private Text nameText;
        [SerializeField] private InputField questionText;
        [SerializeField] private InputField correctAnswerText;
        [SerializeField] private InputField gradeInput;

#pragma warning restore

        private List<Question> tempQuestions;

        private List<Question> questionPool;

        private Test currentTest;

        protected override bool OnInit()
        {
            CloseUI();
            return true;
        }

        #region public functions

        public void StartNewTest()
        {
            PrepareTesting();
            nameText.text = inputName.text;
            inputName.text = "";
            StartPanel.SetActive(false);
            ClearProcessPanel();
            ProcessPanel.SetActive(true);
            FinishPanel.SetActive(false);
        }

        public void StartTesting()
        {
            LoadQuestions();
            OpenUI();
        }

        public void ExitFromStartMenu()
        {
            CloseUI();
            inputName.text = "";
        }

        public void FinishTesting()
        {
            //TODO result
            StartPanel.SetActive(false);
            ProcessPanel.SetActive(false);
            FinishPanel.SetActive(true);
        }

        public void DropQuestion() //перейти к следующему вопросу
        {
        }

        public void LeftQuestion() //убрать вопрос из пула
        {
        }

        public void SaveAnswer() //сохранить ответ
        {
        }

        #endregion public functions

        #region private functions

        private void ClearProcessPanel()
        {
        }

        private void OpenUI()
        {
            UIPanel.SetActive(true);
            StartPanel.SetActive(true);
            ProcessPanel.SetActive(false);
            FinishPanel.SetActive(false);
        }

        private void CloseUI()
        {
            UIPanel.SetActive(false);
            StartPanel.SetActive(false);
            ProcessPanel.SetActive(false);
            FinishPanel.SetActive(false);
        }

        private void PrepareTesting()
        {
            currentTest = new Test
            {
                answers = new List<Answer>(),
                name = inputName.text
            };
        }

        private void PreparePool()
        {
            questionPool = new List<Question>();
            for (int i = 0; i < tempQuestions.Count; i++)
            {
                questionPool.Add(tempQuestions[i].Copy());
            }
        }

        private void LoadQuestions()
        {
            tempQuestions = Services.GetManager<DataManager>().DynamicData.questions;
        }

        #endregion private functions
    }
}