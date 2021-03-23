using System;

namespace Global.Managers
{
    public class TestingManager : BaseManager
    {
        public override Type ManagerType => typeof(TestingManager);

        protected override bool OnInit()
        {
            return true;
        }

        public void StartTesting()
        {
            throw new NotImplementedException();
        }
    }
}