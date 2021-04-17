using UnityEngine;
using UnityEngine.UI;

namespace Tools.Components.Universal
{
    [RequireComponent(typeof(Text))]
    public class ScrollableText : BaseScrollableContainer
    {
        #region Inspector variables

#pragma warning disable

        [HideInInspector, SerializeField] private Text text;

#pragma warning restore

        #endregion Inspector variables

        #region Public functions

        public override void Init(IScrollableContainerContent content)
        {
            text.text = ((TextScrollableContainerContent)content).text;
        }

        public override void OnFirstInit()
        {
            //do nothing
        }

        #endregion Public functions

        #region Protected functions

        protected override void Validate()
        {
            if (text == null)
            {
                text = GetComponent<Text>();
            }
        }

        #endregion Protected functions
    }
}