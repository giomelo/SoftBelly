using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;

namespace _Scripts.Systems.Lab.Machines
{
    public class MixPan : BaseMachine
    {
        public override void CreateResult()
        {
            throw new System.NotImplementedException();
        }
        
        protected override void InitMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCameraMix();
        }
        protected override void FinishMachine()
        {
            GameManager.Instance.camSwitcher.ChangeCameraMix();
            OnDisposeMachine();
        }

        private void OnDisposeMachine()
        {
            
        }
    }
}