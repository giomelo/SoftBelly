using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;

namespace _Scripts.Systems.Lab.Machines
{
    public class Cauldron : BaseMachine, IBurn
    {
        public void Burn()
        {
            StartMachine();
        }
    }
}