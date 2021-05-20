using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers
{
    using Global.Components;
    using Global.Components.Testing;
    using Global.Managers.Datas;
    using System.Linq;
    using Tools;
    using Tools.Components.Universal;
    using UnityEngine.UI;

    [Assert]
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
        [SerializeField] private InputField inputFind;
        [SerializeField] private ScrollableComponent questionsList;

        [SerializeField] private Text resultText;

#pragma warning restore

        private List<Question> tempQuestions;

        private List<Question> questionPool;

        [SerializeField] private Test currentTest;

        private Question currentQuestion;

        private int currentGrade
        {
            get
            {
                int result = 0;
                result = int.TryParse(gradeInput.text, out result) ? result : 0;
                return result;
            }
        }

        public const int maxRate = 10;

        protected override bool OnInit()
        {
            CloseUI();
            return true;
        }

        #region public functions

        public void BeginTestingProcess()
        {
            PrepareTesting();
            nameText.text = inputName.text;
            inputName.text = "";
            ClearProcessPanel();
            PreparePool();
            SetContentInScrollable();
            StartPanel.SetActive(false);
            ProcessPanel.SetActive(true);
            FinishPanel.SetActive(false);
        }

        public void StartTesting()
        {
            //Test test = Services.GetManager<DataManager>().DynamicData.testsResults[2];
            //float dif = 0;
            //for (int i = 0; i < test.answers.Count; i++)
            //{
            //    dif += test.answers[i].question.Difficult;
            //}

            //dif /= test.answers.Count;
            //Debug.Log(dif);

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
            resultText.text = nameText.text + "\n get " + GetScore().ToString();
            StartPanel.SetActive(false);
            ProcessPanel.SetActive(false);
            FinishPanel.SetActive(true);
            Services.GetManager<DataManager>().DynamicData.testsResults.Add(currentTest);
        }

        public void LeftQuestion() //убрать вопрос из пула
        {
            questionPool.Remove(currentQuestion);
            SetContentInScrollable();
            ClearProcessPanel();
        }

        public void SaveAnswer() //сохранить ответ
        {
            currentTest.answers.Add(new Answer(currentQuestion, currentGrade));
            questionPool.Remove(currentQuestion);
            currentQuestion = null;
            SetContentInScrollable();
            ClearProcessPanel();
        }

        public void OnFindEnter()
        {
            SetContentInScrollable(questionPool.Where(x => x.QuestionSubject.ToLower().IndexOf(inputFind.text.ToLower()) >= 0).ToList());
        }

        public void ResetContentScrollable()
        {
            SetContentInScrollable();
            inputFind.text = "";
        }

        #endregion public functions

        #region private functions

        private string GetScore()
        {
            if (currentTest.answers.Count > 0)
            {
                float totalGradeSum = 0;
                float maximumGradeSum = 0;
                float averageDifficult = 0;
                for (int i = 0; i < currentTest.answers.Count; i++)
                {
                    totalGradeSum += currentTest.answers[i].grade * currentTest.answers[i].question.Difficult;
                    maximumGradeSum += maxRate * currentTest.answers[i].question.Difficult;
                    averageDifficult += currentTest.answers[i].question.Difficult;
                }
                averageDifficult /= currentTest.answers.Count;
                totalGradeSum = totalGradeSum / maximumGradeSum * averageDifficult;

                return Mathf.RoundToInt(totalGradeSum * 100).ToString() + "\n difficult = " + averageDifficult.ToString("0.00");
            }
            return "0";
        }

        private void ClearProcessPanel()
        {
            questionText.text = "";
            correctAnswerText.text = "";
            gradeInput.text = "";
        }

        private void SetContentInScrollable()
        {
            SetContentInScrollable(questionPool);
        }

        private void SetContentInScrollable(List<Question> questions)
        {
            List<IScrollableContainerContent> content = new List<IScrollableContainerContent>();
            for (int i = 0; i < questions.Count; i++)
            {
                int iTemp = i;
                content.Add(new ButtonScrollableContainerContent() { text = questions[iTemp].QuestionSubject, onClick = () => { ChooseQuestion(questions[iTemp].ID); } });
            }
            questionsList.SetContent(content);
        }

        private void ChooseQuestion(int index)
        {
            currentQuestion = questionPool.FirstOrDefault(x => x.ID == index);
            questionText.text = currentQuestion.QuestionSubject;
            correctAnswerText.text = currentQuestion.CorrectAnswer;
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