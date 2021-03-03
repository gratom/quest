using System;

namespace Tools
{
    public static class EnumEnumerator
    {
        public static T Next<T>(this T src) where T : Enum
        {
            T[] array = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf(array, src) + 1;
            return (array.Length == j) ? array[0] : array[j];
        }
    }
}