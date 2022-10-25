using System.Collections;
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
        
        /// <summary>
        /// Move agent to destination
        /// </summary>
        /// <param name="position"></param>
        public void MoveToPosition(Vector3 position)
        {
            agent.destination = position;
        }
        
        public bool CheckIfIsInDestination()
        {
            // Check if we've reached the destination
            if (agent.pathPending) return false;
            if (!(agent.remainingDistance <= agent.stoppingDistance)) return false;
            return !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
        }
    }
}
