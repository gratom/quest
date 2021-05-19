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

#pragma warning restore

        private List<StatisticData> statisticDatas;
        private List<Test> testsData;

        private StatisticData selectedData;

        protected override bool OnInit()
        {
            return true;
        }

        #region public functions

        public void StartStatistic()
        {
            LoadStatisticData();
            LoadTestData();
            OpenUI();
        }

        public void AddFromTest()
        {
            TestsSubPanel.SetActive(true);
            List<IScrollableContainerContent> content = new List<IScrollableContainerContent>();
            for (int i = 0; i < testsData.Count; i++)
            {
                int j = i;
                content.Add(new ButtonScrollableContainerContent() { text = testsData[j].name, onClick = () => { NewStatisticFromTest(j); } });
            }
            testsList.SetContent(content, true);
            //show panel with tests
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
            StatisticData data = new StatisticData()
            {
            };
            statisticDatas.Add(data);
        }

        private void UpdateListPersons()
        {
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