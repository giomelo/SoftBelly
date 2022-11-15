using System;
using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts.Helpers
{
    public class OnMouseOverScript : MonoBehaviour
    {
        [SerializeField]
        private Outline objectOutline;
        [SerializeField]
        private LayerMask layyer;

        private bool isOver = true;
        void OnMouseOver()
        {
            //If your mouse hovers over the GameObject with the script attached, output this message
            Debug.Log("Mouse is over GameObject.");
            if(objectOutline)
                objectOutline.enabled = true;
        }

        private void FixedUpdate()
        {
            Ray direction = GameManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(GameManager.Instance.MainCamera.transform.position, direction.direction, out hit, 5000,
                    layyer))
            {
                if (!isOver)
                {
                    if (hit.transform.TryGetComponent(out objectOutline))
                    {
                        objectOutline.enabled = false;
                        isOver = true;
                    }
                }
                return;
            }

            if (isOver)
            {
                if (hit.transform.TryGetComponent(out objectOutline))
                {
                    objectOutline.enabled = true;
                    isOver = false;
                }
            }
       
            Debug.Log("oi");
        }

        public void Teste()
        {
            Debug.Log("Mouse is over GameObject.");
            if(objectOutline)
                objectOutline.enabled = true;
        }

        void OnMouseExit()
        {
            //The mouse is no longer hovering over the GameObject so output this message each frame
            Debug.Log("Mouse is no longer on GameObject.");
            if(objectOutline)
                objectOutline.enabled = false;
        }
    }
}