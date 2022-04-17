using System.Collections;
using System.Collections.Generic;
using _Scripts.Singleton;
using UnityEngine;

public class TimerFaceScreen : MonoBehaviour
{
    void Update()
    {
        //transform.LookAt(GameManager.Instance.MainCamera.transform.position, Vector3.up);
    }

    private void Start()
    {
        StartCoroutine(Look());
    }

    public IEnumerator Look()
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
