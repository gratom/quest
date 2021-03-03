using UnityEngine;

namespace Tools
{
    public static class Asserter
    {
        public static void Assert<T>(T objectAssert, MonoBehaviour objectMono)
        {
            Debug.Assert(objectAssert != null, "The field " + typeof(T).Name + " is null\nin: " + objectMono?.name + ";    type of: " + objectMono?.GetType().ToString());
        }
    }
}