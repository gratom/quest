using System.Collections.Generic;
using UnityEngine;

namespace Global.Components.Statistics
{
    public class Diagram : MonoBehaviour
    {
#pragma warning disable
        [SerializeField] private DiagramPart prefab;
        [SerializeField] private Transform diagramPartParent;
#pragma warning restore

        public float MaxHeight
        {
            get => maxHeight;
            set
            {
                maxHeight = value;
                NormilizedValue = GetComponent<RectTransform>().rect.height / maxHeight;
            }
        }

        public float NormilizedValue { get; private set; }

        private float maxHeight;

        private List<DiagramPart> diagramParts = new List<DiagramPart>();

        #region public functions

        public void SetDiagram(List<DiagramData> diagramDatas)
        {
            if (diagramDatas != null)
            {
                DisableAll();
                //Color color = new Color()
                foreach (DiagramData item in diagramDatas)
                {
                    DiagramPart part = GetNext();
                    part.TextValue = item.fieldName;
                    part.Height = item.fieldValue * NormilizedValue;
                }
            }
        }

        private DiagramPart GetNext()
        {
            foreach (DiagramPart item in diagramParts)
            {
                if (!item.gameObject.activeSelf)
                {
                    item.gameObject.SetActive(true);
                    return item;
                }
            }
            diagramParts.Add(Instantiate(prefab, diagramPartParent));
            return diagramParts[diagramParts.Count - 1];
        }

        private void DisableAll()
        {
            foreach (DiagramPart item in diagramParts)
            {
                item.gameObject.SetActive(false);
            }
        }

        #endregion public functions
    }

    public class DiagramData
    {
        public string fieldName;
        public float fieldValue;
    }
}