using System;
using _Scripts.Helpers;
using _Scripts.Singleton;
using _Scripts.Systems.Lab.Machines.Base;
using _Scripts.Systems.Lab.Machines.MachineBehaviour;
using UnityEngine;

namespace _Scripts.Systems.Lab.Machines
{
    public class Pestle : BaseMachine, IMix
    {
        public override void CreateResult()
        {
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            if (LabEvents.CurrentMachine == null || LabEvents.CurrentMachine.MachineId != MachineId) return;
            if (Input.GetMouseButton(0))
            {
                if (DragAndDropBase.SelectedObject == null)
                {
                    RaycastHit hit = DragAndDropBase.CastRay();
                    if (hit.collider == null) return;
                    if (!hit.collider.CompareTag("Drag")) return;
                
                    DragAndDropBase.SelectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
                else
                {
                    //disable rigidybody
                    // DragAndDropBase.SelectedObject =null;
                    // Cursor.visible = true;
                }

                if (DragAndDropBase.SelectedObject == null) return;
                Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    GameManager.Instance.MainCamera
                        .WorldToScreenPoint(DragAndDropBase.SelectedObject.transform.position).z);
                Vector3 worldPos = GameManager.Instance.MainCamera.ScreenToWorldPoint(pos);
                DragAndDropBase.SelectedObject.transform.position = new Vector3(worldPos.x, worldPos.y, worldPos.z);
            }
            else
            {
                DragAndDropBase.SelectedObject =null;
                Cursor.visible = true;
            }

        }
    }
}
