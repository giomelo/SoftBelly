using UnityEngine;

namespace _Scripts.Helpers
{
    public class OnMouseOverScript : MonoBehaviour
    {
        [SerializeField]
        private Outline objectOutline;
        void OnMouseOver()
        {
            //If your mouse hovers over the GameObject with the script attached, output this message
            Debug.Log("Mouse is over GameObject.");
            if(objectOutline)
                objectOutline.enabled = true;
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