using System;
using _Scripts.Screen_Flow;
using _Scripts.Singleton;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Systems
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private string scene;
        [SerializeField]
        private Vector3 position;
        [SerializeField]
        private UnityEvent cameraChanger;
        private void OnTriggerEnter(Collider other)
        {
            //ScreenFlow.Instance.LoadScene(scene);
           
            GameManager.Instance.Player.position = position;
            cameraChanger.Invoke();
            
        }
    }
}
