using Global.Managers;
using UnityEngine;

namespace Global.Components.Menu
{
    public class MenuComponent : MonoBehaviour
    {
        #region public functions

        public void StartTestingButton()
        {
            Services.GetManager<TestingManager>().StartTesting();
        }

        public void OpenQuestionsButton()
        {
            Services.GetManager<QuestionManager>().OpenQuestions();
        }

        public void OpenStatistic()
        {
            Services.GetManager<StatisticManager>().StartStatistic();
        }

        #endregion public functions
    }
}