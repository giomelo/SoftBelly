using System;
using _Scripts.Entities.Player;
using _Scripts.Screen_Flow;
using _Scripts.Singleton;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Systems
{
    public enum DoorLocations
    {
        GARDEN,
        LAB,
        PATIENTS
    }
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private string scene;
        [SerializeField]
        private Vector3 position;
        [SerializeField]
        private UnityEvent cameraChanger;
        [SerializeField]
        private DoorLocations doorTransport;
        private void OnTriggerEnter(Collider other)
        {
            //ScreenFlow.Instance.LoadScene(scene);
            PlayerInputHandler.DisableInputCall();
            GameManager.Instance.Player.position = position;
            PlayerInputHandler.EnableInputCall();
            switch (doorTransport)
            {
                // inicializar alguma coisa no ambiente
                case DoorLocations.LAB:
                    break;
                case DoorLocations.GARDEN:
                    break;
                case DoorLocations.PATIENTS:
                    break;
                
            }
            cameraChanger.Invoke();
            
        }
    }
}
