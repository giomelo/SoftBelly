using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Entities.Player;
using _Scripts.Helpers;
using _Scripts.Screen_Flow;
using _Scripts.Singleton;
using _Scripts.Systems.Patients;
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
            StartCoroutine(BackInput());
            switch (doorTransport)
            {
                // inicializar alguma coisa no ambiente
                case DoorLocations.LAB:
                    GameManager.Instance.MainCamera = GameManager.Instance.camSwitcher.mainLabcamera;
                    break;
                case DoorLocations.GARDEN:
                    
                    break;
                case DoorLocations.PATIENTS:
                    PatientsController.Instance.Initialize();
                    break;
                
            }
            //cameraChanger.Invoke();
            
        }

        public IEnumerator BackInput()
        {
            yield return new WaitForSeconds(1);
            PlayerInputHandler.EnableInputCall();
            
        }
    }
}
