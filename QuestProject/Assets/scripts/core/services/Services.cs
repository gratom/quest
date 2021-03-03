using System.Collections.Generic;
using UnityEngine;

namespace Global
{
    using Managers;

    public static class Services
    {
        private static bool isInit = false;

        private static List<BaseManager> managersList = new List<BaseManager>();

        public static void InitAppWith(List<BaseManager> managers)
        {
            if (!isInit)
            {
                managersList = managers;

                for (int i = 0; i < managersList.Count; i++)
                {
                    managersList[i].Init();
                }

                isInit = true;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError("Services already initiated. Your list of managers is not added to services.");
            }
#endif
        }

        public static T GetManager<T>() where T : BaseManager
        {
            return (T)managersList.Find(x => x.ManagerType == typeof(T));
        }
    }
}