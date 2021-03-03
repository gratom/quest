using UnityEngine;

namespace Tools
{
    public abstract class AbstractAverage<T> where T : new()
    {
        protected T[] all;
        private int counter = 0;

        public int count => all.Length;

        public abstract T average { get; }

        public int Counter
        {
            get => counter;
            private set
            {
                counter = value;
                if (counter >= all.Length)
                {
                    counter = 0;
                }
            }
        }

        public AbstractAverage(int count = 5)
        {
            all = new T[count];
        }

        public void Clear()
        {
            for (int i = 0; i < all.Length; i++)
            {
                all[i] = new T();
            }
        }

        public void AddNext(T value)
        {
            all[Counter++] = value;
        }
    }

    public class AverageFloat : AbstractAverage<float>
    {
        public AverageFloat(int count = 5) : base(count)
        {
        }

        public override float average
        {
            get
            {
                float sum = 0;
                int indexs = 0;
                for (int i = 0; i < all.Length; i++)
                {
                    if (all[i] != 0)
                    {
                        sum += all[i];
                        indexs++;
                    }
                }
                return indexs != 0 ? sum / indexs : 0;
            }
        }
    }

    public class AverageInt : AbstractAverage<int>
    {
        public AverageInt(int count = 5) : base(count)
        {
        }

        public override int average
        {
            get
            {
                int sum = 0;
                int indexs = 0;
                for (int i = 0; i < all.Length; i++)
                {
                    if (all[i] != 0)
                    {
                        sum += all[i];
                        indexs++;
                    }
                }
                return indexs != 0 ? sum / indexs : 0;
            }
        }
    }

    public class AverageVector2 : AbstractAverage<Vector2>
    {
        public AverageVector2(int count = 5) : base(count)
        {
        }

        public override Vector2 average
        {
            get
            {
                Vector2 sum = Vector2.zero;
                int indexs = 0;
                for (int i = 0; i < all.Length; i++)
                {
                    if (all[i] != Vector2.zero)
                    {
                        sum += all[i];
                        indexs++;
                    }
                }
                return indexs != 0 ? sum / indexs : Vector2.zero;
            }
        }
    }

    public class AverageVector3 : AbstractAverage<Vector3>
    {
        public AverageVector3(int count = 5) : base(count)
        {
        }

        public override Vector3 average
        {
            get
            {
                Vector3 sum = Vector3.zero;
                int indexs = 0;
                for (int i = 0; i < all.Length; i++)
                {
                    if (all[i] != Vector3.zero)
                    {
                        sum += all[i];
                        indexs++;
                    }
                }
                return indexs != 0 ? sum / indexs : Vector3.zero;
            }
        }
    }
}