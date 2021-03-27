using UnityEngine;

namespace Scrolling
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class ScrollableContainer : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [HideInInspector, SerializeField] private RectTransform rectTransform;
#pragma warining restore

        #endregion Inspector variables

        #region Properties

        public RectTransform RectTransform => rectTransform;

        #endregion Properties

        #region Unity functions

        private void OnValidate()
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }

            Validate();
        }

        #endregion Unity functions

        #region Abstract functions

        public abstract void Init(IScrollableContainerContent content);
        protected abstract void Validate();

        #endregion Abstract functions
    }
}