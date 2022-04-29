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
        private Vector3 _startPosition;
        protected bool canDrag = false;

        private void OnMouseDown()
        {
            if (!canDrag) return;
            _startPosition = gameObject.transform.position;
            var position = _startPosition;
            mZCoord = GameManager.Instance.MainCamera.WorldToScreenPoint(
                position).z;
            // Store offset = gameobject world pos - mouse world pos
            mOffset = position - GetMouseAsWorldPoint();
            Cursor.visible = false;
        }

        private void OnMouseUp()
        {
            if (!canDrag) return;
            Cursor.visible = true;
            transform.position = _startPosition;
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
            if (!canDrag) return;
            if (LabEvents.CurrentMachine == null ) return;
            if (LabEvents.CurrentMachine as Pestle)
            {
                transform.position = GetMouseAsWorldPoint() + mOffset;
            }
        }
    }
}