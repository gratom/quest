using Scrolling;
using System.Collections.Generic;
using UnityEngine;

public class TestSetterContent : MonoBehaviour
{
#pragma warning disable
    [SerializeField] private ScrollableComponent _scrollableComponent;
#pragma warining restore

    private void Start()
    {
        var testList = new List<IScrollableContainerContent>();
        for (int i = 0; i < 10000; i++)
        {
            testList.Add(new A() { s = "text " + i.ToString() });
        }

        _scrollableComponent.SetContent(testList);
    }
}