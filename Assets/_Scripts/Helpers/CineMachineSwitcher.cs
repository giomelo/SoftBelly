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
        private CinemachineVirtualCamera labCam;
        [SerializeField]
        private CinemachineVirtualCamera pestleCam;
        [SerializeField]
        private CinemachineVirtualCamera mixCam;
        [SerializeField]
        private CinemachineVirtualCamera potionCam;
        [SerializeField]
        private CinemachineVirtualCamera gardenCam;

        public Camera mainLabcamera; 
        private bool _isOnWorld = false;
        private bool _isOnGarden = true;

        public void ChangeCameraPestle()
        {
            if (_isOnWorld)
            {
                pestleCam.Priority = 2;
                labCam.Priority = 1;
            }
            else
            {
                pestleCam.Priority = 1;
                labCam.Priority = 2;
            }
            
            _isOnWorld = !_isOnWorld;
        }      
        public void ChangeCameraMix()
        {
            if (_isOnWorld)
            {
                mixCam.Priority = 2;
                labCam.Priority = 1;
            }
            else
            {
                mixCam.Priority = 1;
                labCam.Priority = 2;
            }
            
            _isOnWorld = !_isOnWorld;
        }
        public void ChangeCameraPotion()
        {
            if (_isOnWorld)
            {
                potionCam.Priority = 2;
                labCam.Priority = 1;
            }
            else
            {
                potionCam.Priority = 1;
                    labCam.Priority = 2;
            }
            
            _isOnWorld = !_isOnWorld;
        }
        
        public void ChangeCameraGarden()
        {
            if (_isOnGarden)
            {
                labCam.Priority = 2;
                gardenCam.Priority = 1;
            }
            else
            {
                labCam.Priority = 1;
                gardenCam.Priority = 2;
            }
            
            _isOnWorld = true;
            _isOnGarden = !_isOnGarden;
        }
    }
}
