using UnityEngine;

namespace Global.Managers
{
    public abstract class BaseManager : MonoBehaviour
    {
        public abstract System.Type ManagerType { get; }

        public bool isInit { get; private set; } = false;

        public void Init()
        {
            if (!isInit)
            {
                isInit = OnInit();
                if (!isInit)
                {
                    throw new System.Exception("manager type of " + ManagerType + " is not initialized correctly!");
                }
            }
        }

        protected abstract bool OnInit();
    }
}