using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace _Scripts.Helpers
{
    /// <summary>
    /// Basic camswitcher control
    /// </summary>
    public class CineMachineSwitcher : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera worldCam;
        [SerializeField]
        private CinemachineVirtualCamera pestleCam;
        private bool _isOnWorld = true;

        public void ChangeCamera()
        {
            if (_isOnWorld)
            {
                pestleCam.Priority = 2;
                worldCam.Priority = 1;
            }
            else
            {
                pestleCam.Priority = 1;
                worldCam.Priority = 2;
            }
            
            _isOnWorld = !_isOnWorld;
        }
    }
}
