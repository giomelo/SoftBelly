using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Entities.Player;
using _Scripts.Helpers;
using _Scripts.Screen_Flow;
using _Scripts.Singleton;
using _Scripts.Store;
using _Scripts.Systems.Patients;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Systems
{
    public enum DoorLocations
    {
        GARDEN,
        LAB,
        PATIENTS,
        STORE
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
           DoorCall();
        }

        public void DoorCall()
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
                    PlayerInputHandler.EnableInputCall();
                    StoreController.Instance.StorageObject.SetActive(false);
                    break;
                case DoorLocations.PATIENTS:
                    PatientsController.Instance.Initialize();
                    break;
                case DoorLocations.STORE:
                    PlayerInputHandler.DisableInputCall();
                    StoreController.Instance.StorageObject.SetActive(true);
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
