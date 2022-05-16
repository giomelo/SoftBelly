using System;
using System.Collections;
using System.Collections.Generic;
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
        private Vector3 _mOffset;
        private float _mZCoord;
        private Vector3 _startPosition;
        private Vector3 _startRotation;
        protected bool CanDrag = false;
        [SerializeField] private bool canDragBoth;
        protected Rigidbody rb;

        public virtual void Start()
        {
            var transform1 = transform;
            _startPosition = transform1.position;
            _startRotation = transform1.eulerAngles;
        }
        public virtual void StartDrag()
        {
            CanDrag = true;
        }
        public virtual void StopDrag()
        {
            CanDrag = false;
        }

        private void OnMouseDown()
        {
            if (!CanDrag) return;
            _startPosition = gameObject.transform.position;
            var position = _startPosition;
            _mZCoord = GameManager.Instance.MainCamera.WorldToScreenPoint(
              position).z;
              // Store offset = gameobject world pos - mouse world pos
            _mOffset = position - GetMouseAsWorldPoint();
            Cursor.visible = false;
        }

        public virtual void OnMouseUp()
        {
            if (!CanDrag) return;
            Cursor.visible = true;
            transform.position = _startPosition;
            transform.eulerAngles = _startRotation;
        }


        private Vector3 GetMouseAsWorldPoint()
        {
            // Pixel coordinates of mouse (x,y)
            Vector3 mousePoint = Input.mousePosition;

            // z coordinate of game object on screen
            mousePoint.z = _mZCoord;
            // Convert it to world points

            return GameManager.Instance.MainCamera.ScreenToWorldPoint(mousePoint);
        }
        
        private void OnMouseDrag()
        {
            if (!CanDrag) return;
            if (LabEvents.CurrentMachine == null) return;
            if (!canDragBoth)
            {
                transform.position = new Vector3(_startPosition.x, GetMouseAsWorldPoint().y + _mOffset.y, GetMouseAsWorldPoint().z + _mOffset.z);
            }
            else
            {
                transform.position = new Vector3(GetMouseAsWorldPoint().x, GetMouseAsWorldPoint().y + _mOffset.y, GetMouseAsWorldPoint().z + _mOffset.z);

            }
        }

        public void ResetObj()
        {
            Cursor.visible = true;
            transform.position = _startPosition;
        }
        protected IEnumerator BackRigidbody()
        {
            yield return new WaitForSeconds(0.5f);
            rb.isKinematic = false;
        }
    }
}