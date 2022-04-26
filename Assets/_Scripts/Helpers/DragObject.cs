using System;
using _Scripts.Singleton;
using _Scripts.Systems.Lab;
using _Scripts.Systems.Lab.Machines;
using UnityEngine;

namespace _Scripts.Helpers
{
    /// <summary>
    /// Basic drag object
    /// </summary>
    public class DragObject : MonoBehaviour
    {
        private Vector3 mOffset;
        private float mZCoord;

        private void OnMouseDown()
        {
            var position = gameObject.transform.position;
            mZCoord = GameManager.Instance.MainCamera.WorldToScreenPoint(
                position).z;
            // Store offset = gameobject world pos - mouse world pos
            mOffset = position - GetMouseAsWorldPoint();
            Cursor.visible = false;
        }

        private void OnMouseUp()
        {
            Cursor.visible = true;
        }


        private Vector3 GetMouseAsWorldPoint()
        {
            // Pixel coordinates of mouse (x,y)
            Vector3 mousePoint = Input.mousePosition;

            // z coordinate of game object on screen
            mousePoint.z = mZCoord;
            // Convert it to world points

            return GameManager.Instance.MainCamera.ScreenToWorldPoint(mousePoint);
        }
        
        private void OnMouseDrag()
        {
            if (LabEvents.CurrentMachine == null ) return;
            if (LabEvents.CurrentMachine as Pestle)
            {
                transform.position = GetMouseAsWorldPoint() + mOffset;
            }
        }
    }
}