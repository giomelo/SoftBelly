using _Scripts.Systems.Lab.Machines.MachineBehaviour;

namespace _Scripts.Systems.Lab.Machines
{
    public class Cauldron : BaseMachine, ITimerMachine
    {
        public void Work()
        {
            StartMachine();
        }
    }
}