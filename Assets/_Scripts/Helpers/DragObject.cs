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
    public abstract class DragObject : MonoBehaviour
    {
        private Vector3 mOffset;
        private float mZCoord;
        private Vector3 _startPosition;
        protected bool canDrag = false;
        [SerializeField] private bool _canDragBoth;

        private void Start()
        {
            _startPosition = gameObject.transform.position;
        }
        public virtual void StartDrag()
        {
            canDrag = true;
        }
        public virtual void StopDrag()
        {
            canDrag = false;
        }

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
            if (LabEvents.CurrentMachine == null) return;
            if (!_canDragBoth)
            {
                transform.position = new Vector3(_startPosition.x, GetMouseAsWorldPoint().y + mOffset.y, GetMouseAsWorldPoint().z + mOffset.z);
            }
            else
            {
                transform.position = new Vector3(GetMouseAsWorldPoint().x, GetMouseAsWorldPoint().y + mOffset.y, GetMouseAsWorldPoint().z + mOffset.z);

            }
        }

        public void ResetObj()
        {
            Cursor.visible = true;
            transform.position = _startPosition;
        }
    }
}