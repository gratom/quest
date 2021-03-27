using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    using Global.Components;
    using Global.Components.Testing;

    [Serializable]
    public class DynamicData
    {
#pragma warning disable
        [SerializeField] public List<Question> questions = new List<Question>();
        [SerializeField] public List<Test> testsResults = new List<Test>();
#pragma warning restore
    }
}