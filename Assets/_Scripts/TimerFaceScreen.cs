using UnityEngine;

namespace _Scripts
{
    public class TimerFaceScreen : MonoBehaviour
    {
        void Update()
        {
            transform.LookAt(Camera.main.transform.position, Vector3.up);
        }
    }
}
