using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    using Global.Components;

    [Serializable]
    public class DynamicData
    {
#pragma warning disable
        [SerializeField] public List<Question> questions = new List<Question>();
#pragma warning restore
    }
}