using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Entities.Npcs
{
    /// <summary>
    /// class for basic npc behaviour
    /// </summary>
    public abstract class NpcBase : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent agent;
        public void MoveToPosition(Vector3 position)
        {
            agent.destination = position;
        }
    }
}
