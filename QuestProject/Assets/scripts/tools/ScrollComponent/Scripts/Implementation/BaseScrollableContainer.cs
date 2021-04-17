using UnityEngine;

namespace Tools.Components.Universal
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class BaseScrollableContainer : MonoBehaviour
    {
        #region Inspector variables

#pragma warning disable
        [HideInInspector, SerializeField] private RectTransform rectTransform;
#pragma warning restore

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

        public abstract void OnFirstInit();

        public abstract void Init(IScrollableContainerContent content);

        protected abstract void Validate();

        #endregion Abstract functions
    }
}