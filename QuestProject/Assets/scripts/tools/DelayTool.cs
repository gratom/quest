using System;
using System.Collections;
using UnityEngine;

namespace Tools
{
    public class DelayTool : MonoBehaviour
    {
        public static void NewDelay(float time, Action action)
        {
            DelayTool d = new GameObject().AddComponent<DelayTool>();
            d.StartCoroutine(d.DelayCoroutine(time, action));
        }

        private IEnumerator DelayCoroutine(float time, Action action)
        {
            yield return new WaitForSecondsRealtime(time);
            action?.Invoke();
            Destroy(gameObject);
        }
    }
}