using UnityEngine;

namespace _Scripts.Systems.Lab.Machines.Base
{
    [RequireComponent(typeof(BaseMachine))]
    public class MachineHolder : MonoBehaviour
    {
        public BaseMachine CurrentMachine;
    }
}