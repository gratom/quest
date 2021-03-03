using System.Collections.Generic;
using UnityEngine;

namespace Global.Boot
{
    using Managers;
    using Tools;

    [CreateAssetMenu(fileName = "BootSettings", menuName = "Scriptables/Boot setting", order = 51)]
    [Assert]
    public class BootSettings : ScriptableObject
    {
#pragma warning disable
        [SerializeField] private float bootTime;
        public float BootTime => bootTime;

        [SerializeField] private int nextSceneIndex = 0;
        public int NextSceneIndex => nextSceneIndex;

        [SerializeField] private List<BaseManager> managers;
        public List<BaseManager> Managers => managers;
#pragma warning restore
    }
}