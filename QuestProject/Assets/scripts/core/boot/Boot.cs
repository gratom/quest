using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Boot
{
    using Global;
    using Managers;
    using Tools;

    [Assert]
    public class Boot : MonoBehaviour
    {
#pragma warning disable
        [SerializeField] private BootSettings bootSetting;
#pragma warning restore

        #region Unity functions

        private void Awake()
        {
            ManagersCreating();
            StartCoroutine(Loading());
        }

        #endregion Unity functions

        #region private functions

        private void ManagersCreating()
        {
            List<BaseManager> baseManagers = new List<BaseManager>();
            GameObject managerGameObject = new GameObject("Managers");
            DontDestroyOnLoad(managerGameObject);
            for (int i = 0; i < bootSetting.Managers.Count; i++)
            {
                baseManagers.Add(Instantiate(bootSetting.Managers[i], managerGameObject.transform));
            }
            Services.InitAppWith(baseManagers);
        }

        private IEnumerator Loading()
        {
            yield return new WaitForSeconds(bootSetting.BootTime);
            if (bootSetting.NextSceneIndex == 0)
            {
                Debug.Log("Next scene after boot is null, please, check the boot settings.");
                yield break;
            }
            SceneLoader.LoadScene(bootSetting.NextSceneIndex);
        }

        #endregion private functions
    }
}