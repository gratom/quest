using System;

namespace Tools.Components.Universal
{
    public class ButtonScrollableContainerContent : IScrollableContainerContent
    {
        public string text;
        public Action onClick;
    }
}