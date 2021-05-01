using UnityEngine;
using UnityEngine.UI;

namespace Tools.Components.Universal
{
    [Assert]
    [RequireComponent(typeof(Button))]
    public class ColoredScrollableButton : ScrollableButton
    {
        #region public functions

        public override void Init(IScrollableContainerContent content)
        {
            base.Init(content);
            buttonText.color = ((ColoredButtonScrollableContainerContent)content).colorForText;
        }

        #endregion public functions
    }
}