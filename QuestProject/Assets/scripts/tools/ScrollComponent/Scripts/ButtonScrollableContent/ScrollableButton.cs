using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.Components.Universal
{
    [Assert]
    [RequireComponent(typeof(Button))]
    public class ScrollableButton : BaseScrollableContainer
    {
#pragma warning disable

        [HideInInspector, SerializeField] protected Button button;
        [HideInInspector, SerializeField] protected Text buttonText;

#pragma warning restore

        private Action onClickAction;

        #region public functions

        public override void Init(IScrollableContainerContent content)
        {
            buttonText.text = ((ButtonScrollableContainerContent)content).text;
            onClickAction = ((ButtonScrollableContainerContent)content).onClick;
        }

        public override void OnFirstInit()
        {
            button.onClick.AddListener(() => { onClickAction?.Invoke(); });
        }

        #endregion public functions

        #region protected functions

        protected override void Validate()
        {
            if (button == null)
            {
                button = GetComponent<Button>();
            }
            if (buttonText == null)
            {
                buttonText = transform.GetComponentInChildren<Text>();
            }
        }

        #endregion protected functions
    }
}