using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts.Helpers
{
    public static class DragAndDropBase
    {
        public static GameObject SelectedObject;

        public static RaycastHit CastRay()
        {
            Camera mainCam = GameManager.Instance.MainCamera;
            Vector3 mousePosFar = new Vector3
                (Input.mousePosition.x, Input.mousePosition.y, 
                    mainCam.farClipPlane);
            Vector3 mousePosNear = new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, 
                mainCam.nearClipPlane);
            Vector3 worldMousePosFar = mainCam.ScreenToWorldPoint(mousePosFar);
            Vector3 worldMousePosNear = mainCam.ScreenToWorldPoint(mousePosNear);

            RaycastHit hit;
            Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
            return hit;

        }
    }
}