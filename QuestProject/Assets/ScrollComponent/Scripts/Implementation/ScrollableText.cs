using UnityEngine;
using UnityEngine.UI;

namespace Scrolling.Content
{
    [RequireComponent(typeof(Text))]
    public class ScrollableText : ScrollableContainer
    {
        #region Inspector variables

#pragma warning disable
        [HideInInspector, SerializeField] private Text text;
#pragma warining restore

        #endregion Inspector variables

        #region Protected functions

        protected override void Validate()
        {
            if (text == null)
            {
                text = GetComponent<Text>();
            }
        }

        #endregion Protected functions

        #region Public functions

        public override void Init(IScrollableContainerContent content)
        {
            text.text = ((A) content).s;
        }

        #endregion Public functions
    }
}