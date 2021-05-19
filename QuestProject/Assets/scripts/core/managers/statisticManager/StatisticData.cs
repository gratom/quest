using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Components.Statistics
{
    [Serializable]
    public class StatisticData
    {
        public string name;
        public int salary;
        public KnowledgeData total;
        public List<KnowledgeData> knowledgesByTags;

        public Dictionary<string, KnowledgeData> KnowledgeDictionary => knowledgeDictionary ?? InitKnowledgeDictionary();

        private Dictionary<string, KnowledgeData> knowledgeDictionary;

        private Dictionary<string, KnowledgeData> InitKnowledgeDictionary()
        {
            if (knowledgesByTags != null)
            {
                knowledgeDictionary = new Dictionary<string, KnowledgeData>();
                foreach (KnowledgeData item in knowledgesByTags)
                {
                    knowledgeDictionary.Add(item.tag, item);
                }
            }
            return knowledgeDictionary;
        }
    }

    [Serializable]
    public class KnowledgeData
    {
        public string tag;
        public int totalGradeSum;
        public int maximumGradeSum;
        public float averageDifficult;
        public int points => Mathf.RoundToInt(totalGradeSum / maximumGradeSum * averageDifficult * 100);
    }
}