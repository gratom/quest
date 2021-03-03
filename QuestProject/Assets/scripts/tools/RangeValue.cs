using System;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public abstract class AbstractRange<T> where T : IComparable, IFormattable, IConvertible
    {
        public T Max
        {
            get => max;
            set => max = value.CompareTo(min) <= 0 ? Inc(min) : value;
        }

        [SerializeField] private T max;

        public T Current
        {
            get => current;
            set => current = value.CompareTo(max) > 0 ? max : value.CompareTo(min) < 0 ? min : value;
        }

        [SerializeField] private T current;

        public T Min
        {
            get => min;
            set => min = value.CompareTo(max) >= 0 ? Dec(max) : value;
        }

        [SerializeField] private T min;

        public abstract float Percent { get; }

        protected abstract T Inc(T value);

        protected abstract T Dec(T value);
    }

    [Serializable]
    public class RangeFloat : AbstractRange<float>
    {
        public override float Percent => Current / Max;

        protected override float Inc(float value)
        {
            return value++;
        }

        protected override float Dec(float value)
        {
            return value--;
        }
    }

    [Serializable]
    public class RangeInt : AbstractRange<int>
    {
        public override float Percent => (float)Current / Max;

        protected override int Dec(int value)
        {
            return value++;
        }

        protected override int Inc(int value)
        {
            return value--;
        }
    }
}