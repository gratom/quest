using System;
using UnityEngine;

namespace Global.Managers.Datas
{
    public sealed class DataManager : BaseManager
    {
#pragma warning disable

        [SerializeField] private StaticData staticData;
        [SerializeField] private DynamicData dynamicData;

#pragma warning restore

        public override Type ManagerType => typeof(DataManager);

        /// <summary>
        /// Data for stable setting, like default settings, URLs, non-changable enemy damage,
        /// constants, something, that you not change after build
        /// </summary>
        public StaticData StaticData => staticData;

        /// <summary>
        /// Data for changable values, like money, score, count of boosts, power ups, etc
        /// </summary>
        public DynamicData DynamicData => dynamicData;

        #region Uniny functions

        private void OnApplicationQuit()
        {
            SaveDynamicData();
        }

        private void OnApplicationPause(bool pause)
        {
            SaveDynamicData();
        }

        #endregion Uniny functions

        protected override bool OnInit()
        {
            LoadOrCreateData(ref dynamicData, staticData.DynamicDataLocation);
            return true;
        }

        #region private functions

        private void SaveDynamicData()
        {
            SaveData(dynamicData, staticData.DynamicDataLocation);
            PlayerPrefs.Save();
        }

        private static void LoadOrCreateData<T>(ref T value, string location) where T : new()
        {
            string data = PlayerPrefs.GetString(location, "");
            if (data != "")
            {
                value = JsonUtility.FromJson<T>(data);
            }
            else
            {
                value = new T();
            }
        }

        private static void SaveData<T>(T value, string location)
        {
            PlayerPrefs.SetString(location, JsonUtility.ToJson(value));
        }

        #endregion private functions
    }
}