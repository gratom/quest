using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Components.Statistics
{
    [Assert]
    public class DiagramPart : MonoBehaviour
    {
        public string TextValue { get => text.text; set => text.text = value; }

        public float Height
        {
            get => image.rectTransform.rect.height;
            set
            {
                image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);
                mainValue.text = ((int)value).ToString();
            }
        }

#pragma warning disable

        [SerializeField] private Image image;
        [SerializeField] private Text text;
        [SerializeField] private Text mainValue;

#pragma warning restore
    }
}