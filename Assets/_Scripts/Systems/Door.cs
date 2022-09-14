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
        private DoorLocations doorTransport;
        [SerializeField]
        private bool teleport;
        
        private void OnTriggerEnter(Collider other)
        {
           DoorCall();
        }

        public void DoorCall()
        {
            //ScreenFlow.Instance.LoadScene(scene);
            PlayerInputHandler.DisableInputCall();
            if(teleport)
                GameManager.Instance.Player.position = position;
         
            switch (doorTransport)
            {
                // inicializar alguma coisa no ambiente
                case DoorLocations.LAB:
                    StartCoroutine(BackInput());
                    GameManager.Instance.MainCamera = GameManager.Instance.camSwitcher.mainLabcamera;
                    break;
                case DoorLocations.GARDEN:
                    StartCoroutine(BackInput());
                    
                   
                    break;
                case DoorLocations.PATIENTS:
                    StartCoroutine(BackInput());
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
            yield return new WaitForSeconds(0.1f);
            PlayerInputHandler.EnableInputCall();
            StoreController.Instance.StorageObject.SetActive(false);
          
        }
    }
}
