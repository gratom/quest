using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers
{
    using Global.Components.Statistics;
    using Global.Components.Testing;
    using Global.Managers.Datas;
    using Tools;

    using Tools.Components.Universal;
    using UnityEngine.UI;

    [Assert]
    public class StatisticManager : BaseManager
    {
        public override Type ManagerType => typeof(StatisticManager);

#pragma warning disable

        [SerializeField] private GameObject UIPanel;
        [SerializeField] private GameObject PersonData;
        [SerializeField] private GameObject ComparerData;
        [SerializeField] private GameObject TestsSubPanel;

        [SerializeField] private ScrollableComponent personsList;
        [SerializeField] private ScrollableComponent testsList;

        #region person

        [SerializeField, Header("person panel data")] private InputField salaryInput;
        [SerializeField] private Text nameText;
        [SerializeField] private Text averageKnowledgeText;
        [SerializeField] private Text knowledgeCostText;
        [SerializeField] private Text totalGradeSumText;
        [SerializeField] private Text averageDifficultText;
        [SerializeField] private Diagram diagram;

        #endregion person

#pragma warning restore

        private List<StatisticData> statisticDatas;
        private List<Test> testsData;

        private StatisticData selectedData;

        protected override bool OnInit()
        {
            CloseUI();
            return true;
        }

        #region public functions

        public void StartStatistic()
        {
            LoadStatisticData();
            LoadTestData();
            UpdateListPersons();
            OpenUI();
        }

        public void OnSalaryInput(string s)
        {
            if (s != "")
            {
                int.TryParse(s, out int salary);
                if (selectedData != null)
                {
                    selectedData.salary = salary;
                }
            }
            UpdateSelectedStatistic();
        }

        public void AddFromTest()
        {
            TestsSubPanel.SetActive(true);
            List<IScrollableContainerContent> content = new List<IScrollableContainerContent>();
            for (int i = 0; i < testsData.Count; i++)
            {
                int j = i;
                content.Add(new ButtonScrollableContainerContent()
                {
                    text = testsData[j].name,
                    onClick = () =>
                    {
                        NewStatisticFromTest(j);
                        TestsSubPanel.SetActive(false);
                        UpdateListPersons();
                    }
                });
            }
            testsList.SetContent(content, true);
            TestsSubPanel.SetActive(true);
        }

        public void DeleteSelected()
        {
            statisticDatas.Remove(selectedData);
            UpdateListPersons();
        }

        public void CloseStatistic()
        {
            Save();
            CloseUI();
        }

        public void OpenPersonData()
        {
            TestsSubPanel.SetActive(false);
            PersonData.SetActive(true);
            ComparerData.SetActive(false);
        }

        public void OpenComparerData()
        {
            TestsSubPanel.SetActive(false);
            PersonData.SetActive(false);
            ComparerData.SetActive(true);
        }

        public void OpenMain()
        {
            TestsSubPanel.SetActive(false);
            PersonData.SetActive(false);
            ComparerData.SetActive(false);
        }

        #endregion public functions

        #region private functions

        private void NewStatisticFromTest(int index)
        {
            StatisticData sData = new StatisticData() { name = testsData[index].name };
            sData.averageKnowledge = new KnowledgeData() { tag = "all" };
            sData.knowledgesByTags = new List<KnowledgeData>();

            HashSet<string> allTags = new HashSet<string>();

            #region add all tags

            foreach (Answer item in testsData[index].answers)
            {
                foreach (string tag in item.question.Tags)
                {
                    if (!allTags.Contains(tag))
                    {
                        allTags.Add(tag);
                    }
                }
            }

            #endregion add all tags

            #region create knowlegdes by tags

            foreach (string tagItem in allTags)
            {
                sData.knowledgesByTags.Add(new KnowledgeData() { tag = tagItem });
            }

            #endregion create knowlegdes by tags

            #region solve values for every knowledge

            foreach (KnowledgeData knowledge in sData.knowledgesByTags)
            {
                int counter = 0;
                foreach (Answer answer in testsData[index].answers)
                {
                    if (answer.question.HashTags.Contains(knowledge.tag))
                    {
                        counter++;
                        knowledge.averageDifficult += answer.question.Difficult;
                        knowledge.maximumGradeSum += answer.question.Difficult * TestingManager.maxRate;
                        knowledge.totalGradeSum += answer.grade * answer.question.Difficult;
                    }
                }
                knowledge.averageDifficult /= counter;
            }

            #endregion solve values for every knowledge

            #region solve values for total knowledge

            {
                int counter = 0;
                foreach (Answer answer in testsData[index].answers)
                {
                    counter++;
                    sData.averageKnowledge.averageDifficult += answer.question.Difficult;
                    sData.averageKnowledge.maximumGradeSum += answer.question.Difficult * TestingManager.maxRate;
                    sData.averageKnowledge.totalGradeSum += answer.grade * answer.question.Difficult;
                }
                sData.averageKnowledge.averageDifficult /= counter;
            }

            #endregion solve values for total knowledge

            statisticDatas.Add(sData);
        }

        private void UpdateListPersons()
        {
            List<IScrollableContainerContent> content = new List<IScrollableContainerContent>();
            for (int i = 0; i < statisticDatas.Count; i++)
            {
                int j = i;
                content.Add(new ButtonScrollableContainerContent()
                {
                    text = statisticDatas[j].name,
                    onClick = () =>
                    {
                        SelectStatistic(j);
                    }
                });
            }
            personsList.SetContent(content);
        }

        private void SelectStatistic(int index)
        {
            selectedData = statisticDatas[index];
            UpdateSelectedStatistic();
        }

        private void UpdateSelectedStatistic()
        {
            if (selectedData != null)
            {
                salaryInput.text = selectedData.salary.ToString();
                nameText.text = selectedData.name;
                averageKnowledgeText.text = selectedData.averageKnowledge.points.ToString();
                knowledgeCostText.text = selectedData.knowledgeCost.ToString("0.00") + "$";
                totalGradeSumText.text = selectedData.averageKnowledge.totalGradeSum.ToString() + " / " + selectedData.averageKnowledge.maximumGradeSum.ToString();
                averageDifficultText.text = selectedData.averageKnowledge.averageDifficult.ToString("0.00");

                diagram.MaxHeight = 600;
                List<DiagramData> diagramDatas = new List<DiagramData>();
                foreach (string item in Services.GetManager<DataManager>().DynamicData.AllTags)
                {
                    if (selectedData.KnowledgeDictionary.ContainsKey(item))
                    {
                        diagramDatas.Add(new DiagramData() { fieldValue = selectedData.KnowledgeDictionary[item].points, fieldName = item });
                    }
                    else
                    {
                        diagramDatas.Add(new DiagramData() { fieldValue = 0, fieldName = item });
                    }
                }
                diagram.SetDiagram(diagramDatas);
            }
        }

        private void Save()
        {
            Services.GetManager<DataManager>().DynamicData.statistics = statisticDatas;
        }

        private void LoadTestData()
        {
            testsData = Services.GetManager<DataManager>().DynamicData.testsResults;
        }

        private void LoadStatisticData()
        {
            statisticDatas = Services.GetManager<DataManager>().DynamicData.statistics;
        }

        private void OpenUI()
        {
            TestsSubPanel.SetActive(false);
            UIPanel.SetActive(true);
            PersonData.SetActive(false);
            ComparerData.SetActive(false);
        }

        private void CloseUI()
        {
            TestsSubPanel.SetActive(false);
            UIPanel.SetActive(false);
            PersonData.SetActive(false);
            ComparerData.SetActive(false);
        }

        #endregion private functions
    }
}