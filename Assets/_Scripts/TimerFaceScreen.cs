using System.Collections;
using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts
{
    public class TimerFaceScreen : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Look());
        }

        private IEnumerator Look()
        {
            transform.LookAt(GameManager.Instance.MainCamera.transform.transform.position, Vector3.up);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Look());
        }
    
        private void OnDestroy()
        {
            StopCoroutine(Look());
        }
    }
}
